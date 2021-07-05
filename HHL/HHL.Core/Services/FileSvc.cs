using HHL.Core.DataAccess.Entities;
using HHL.Core.Handlers;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.PostreSQL.Core.Services;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class FileSvc : HHLDataAccessSvc
    {

        public InstantDatahandler InstantDatahandler { get; set; }
        public FileSvc(IHHLQueryExecutionSvc queryExecutionSvc, InstantDatahandler _InstantDatahandler) : base(queryExecutionSvc)
        {
            InstantDatahandler = _InstantDatahandler;
        }

        public async Task<Guid?> AddFile_ReturnFileId(AddFileModel model)
        {
            var fileTypeId = InstantDatahandler.All_FileTypes.First(q => q.Extension == Path.GetExtension(model.Name)).Id;
            var fileEntity = new e_File()
            {
                Name = model.Name,
                FieldNameId = model.FieldNameId,
                FileTypeId = fileTypeId,
                Size = model.Stream.Length,
                EntityId = model.EntityId
                
            };

            var response = await  _HHLQueryExecutionSvc.INSERTAsync(fileEntity);
            if (!response.Success) return null;


            var fileId = response.Results.First().Id;
            UploadLargeObject(model.Stream, fileId);
            return fileId;
        }


        public void UploadLargeObject(byte[] byteArray, Guid fileId)
        {

            using (Stream stream = new MemoryStream(byteArray))
            {
                using (var connection = new NpgsqlConnection(HHLConfigHdr.ConnStr))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var manager = new NpgsqlLargeObjectManager(connection);
                        var oid = manager.Create();



                        using (var tStream = manager.OpenReadWrite(oid))
                        {
                            var buffer = new byte[262144];
                            while (tStream.Position < stream.Length)
                            {
                                var count = stream.Read(buffer, 0, buffer.Length);
                                tStream.Write(buffer, 0, count);
                            }
                        }

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "update \"public\".\"Files\" set \"Data\" = @oid where \"Id\" = @Id";
                            command.Parameters.AddWithValue("@oid", NpgsqlDbType.Oid, oid);
                            command.Parameters.AddWithValue("@Id", fileId);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }
            }
        }
    }
}

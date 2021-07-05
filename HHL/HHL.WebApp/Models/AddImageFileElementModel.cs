using HHL.FileReader;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHL.WebApp.Models
{
    public class AddImageFileElementModel
    {
        public string ElementId { get; set; }
        public ElementRef Element { get; set; }
        public string FileName { get; set; }
        public bool IsFileUploaded { get {

                return !string.IsNullOrWhiteSpace(FileUploadImageData);
            } }
        public string FileUploadImageData { get; set; }
        public string Base64 { get; set; }
        public byte[] Data { get; set; }        
        public IFileReference FileReference { get; set; }
        public long? LimitSize { get; set; }
        public bool IsFileSizeValid { get; set; }
        public bool IsExtensionValid { get; set; }
        public string[] AllowableExtensions { get; set; } = new string[] { "jpg", "jpeg", "png"};


        public AddImageFileElementModel()
        {

        }
      

        public async Task ValidateFile()
        {
            var fileInfo = await FileReference.ReadFileInfoAsync();
            IsFileSizeValid = LimitSize >= fileInfo.Size;
            IsExtensionValid = AllowableExtensions.Contains(fileInfo.Extension);
            FileName = fileInfo.Name;
        }

   

        public async Task SetFile(IFileReference _fileReference)
        {
            FileReference = _fileReference;
            await ValidateFile();

            if (IsFileSizeValid && IsExtensionValid)
            {
                using (var memoryStream = await FileReference.CreateMemoryStreamAsync())
                {
                    Data = memoryStream.ToArray();
                    Base64 = Convert.ToBase64String(Data);
                    FileUploadImageData = $"data:image/jpg;base64,{Base64}";

                }

            }

        }

        public AddImageFileElementModel(string elementId)
        {
            LimitSize = 10485760;
            ElementId = elementId;
        }
    }
}

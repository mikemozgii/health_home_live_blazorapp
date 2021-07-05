using AutoMapper;
using HHL.Auth.Core.Models;
using HHL.Common;
using HHL.Core.DataAccess.Entities;
using HHL.Core.DataAccess.Views;
using HHL.Core.Interfaces;
using HHL.Core.Models;
using HHL.Core.Models.Address;
using HHL.PostreSQL.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HHL.Core.Services
{
    public class AddressSvc : HHLDataAccessSvc
    {

        public AddressSvc(IHHLQueryExecutionSvc queryExecutionSvc) : base(queryExecutionSvc)
        {
            
        }

        public async Task<QueryResponseGeneric<e_Address>> Upsert(AddEditAddressFormModel model)
        {
            return model.Id == null ? await Insert(model) : await Update(model);
        }

        public async Task<QueryResponseGeneric<e_Address>> Insert(AddEditAddressFormModel model)
        {
            var e = new e_Address() { FieldNameId = model.FieldNameId, AreaKm = model.AreaKm, EntityId = model.EntityId, TypeId = model.TypeId, Line1 = model.Line1, Line2 = model.Line2, Name = model.Name, PostalCode = model.PostalCode, CityId = model.CityId, CountryId= model.CountryId, RegionId = model.RegionId, City = model.City  };
            return await _HHLQueryExecutionSvc.INSERTAsync(e);
        }

        public async Task<QueryResponseGeneric<e_Address>> Update(AddEditAddressFormModel model)
        {
            var e = new e_Address() { Id = model.Id.Value, AreaKm = model.AreaKm, FieldNameId = model.FieldNameId, EntityId = model.EntityId, TypeId= model.TypeId, Line1 = model.Line1, Line2 = model.Line2, Name = model.Name, PostalCode = model.PostalCode, CityId = model.CityId, CountryId = model.CountryId, RegionId = model.RegionId, City = model.City };
            return await _HHLQueryExecutionSvc.UPDATEAsync(e);
        }

        public async Task<QueryResponseGeneric<e_Address>> Upsert(AddEditContractorLocationFormModel model)
        {
            return model.Id == null ? await Insert(model) : await Update(model);
        }

        public async Task<QueryResponseGeneric<e_Address>> Insert(AddEditContractorLocationFormModel model)
        {
            var e = new e_Address() { FieldNameId = model.FieldNameId, AreaKm = model.AreaKm, EntityId = model.EntityId, TypeId = model.TypeId, Name = model.Name, CityId = model.CityId, CountryId = model.CountryId, RegionId = model.RegionId };
            return await _HHLQueryExecutionSvc.INSERTAsync(e);
        }

        public async Task<QueryResponseGeneric<e_Address>> Update(AddEditContractorLocationFormModel model)
        {
            var e = new e_Address() { Id = model.Id.Value, AreaKm = model.AreaKm, FieldNameId = model.FieldNameId, EntityId = model.EntityId, TypeId = model.TypeId, Name = model.Name, CityId = model.CityId, CountryId = model.CountryId, RegionId = model.RegionId, };
            return await _HHLQueryExecutionSvc.UPDATEAsync(e);
        }

        public async Task<e_Address> SelectById(Guid addressId)
        {
            return (await _HHLQueryExecutionSvc.SELECTbyIdAsync<e_Address>(addressId)).FirstOrDefault;
        }

        public async Task<Guid> CopyAddressForTask(Guid addressId)
        {
            var address = await SelectById(addressId);
            address.EntityId = null;
            address.FieldNameId = 5;
            address.TypeId = 0;
            var resp = await _HHLQueryExecutionSvc.INSERTAsync(address);          
            return resp.FirstOrDefault.Id;
        }

        public async Task<QueryResponseGeneric<e_Address>> Delete(Guid addressId)
        {
            return await _HHLQueryExecutionSvc.DELETEAsync<e_Address>(addressId);
        }

        public async Task<QueryResponseGeneric<v_Address>> SelectViewByEntityId(Guid entityId)
        {
            return await _HHLQueryExecutionSvc.SELECTbyColumnValueAsync<v_Address>(nameof(v_Address.EntityId).Pair(entityId));
        }

        public async Task<bool> UpdateType(Guid id, int? typeId)
        {
            var resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Address>(new QueryFilter(QueryFilter.Equal(nameof(e_Address.Id), id)), nameof(e_Address.TypeId).Pair(typeId));
            return resp.Success;
        }

        public async Task<bool> UpdateTypeByEntityId(Guid entityId, int? typeId)
        {
            var resp = await _HHLQueryExecutionSvc.UPDATEAsync<e_Address>(new QueryFilter(QueryFilter.Equal(nameof(e_Address.EntityId), entityId)), nameof(e_Address.TypeId).Pair(typeId));
            return resp.Success;
        }

    }
}

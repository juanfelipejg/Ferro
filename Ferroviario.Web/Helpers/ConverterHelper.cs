using Ferroviario.Web.Data;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<RequestEntity> ToRequestEntityAsync(RequestViewModel model, bool isNew)
        {
            return new RequestEntity
            {
                Id = isNew ? 0 : model.Id,
                Type = await _context.RequestTypes.FindAsync(model.Type),
                InitialDate = model.InitialDate,
                FinishDate = model.FinishDate,
                Description = model.Description,
                State = model.State,
                Comment = model.Comment
            };
        }

        public RequestViewModel ToRequestViewModel(RequestEntity requestEntity)
        {
            return new RequestViewModel
            {
                Id = requestEntity.Id,
                Type = requestEntity.Type.Id,
                Types = _combosHelper.GetComboTypes(),
                InitialDate = requestEntity.InitialDate,
                FinishDate = requestEntity.FinishDate,
                Description = requestEntity.Description,
                State = requestEntity.State,
                Comment = requestEntity.Comment
            };
        }

        public async Task<ServiceDetailEntity> ToServiceDetailEntityAsync(ServiceDetailViewModel model, bool isNew)
        {
            return new ServiceDetailEntity
            {
                Id = isNew ? 0 : model.Id,
                Description = model.Description,
                Service = await _context.Services.FindAsync(model.ServiceId)
            };
        }

        public ServiceDetailViewModel ToServiceDetailViewModel(ServiceDetailEntity serviceDetailEntity)
        {
            return new ServiceDetailViewModel
            {
                Id = serviceDetailEntity.Id,
                Description = serviceDetailEntity.Description,
                Service = serviceDetailEntity.Service,
                ServiceId = serviceDetailEntity.Id
            };
        }
    }
}

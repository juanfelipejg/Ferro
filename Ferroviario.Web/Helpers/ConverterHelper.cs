using Ferroviario.Common.Models;
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
                InitialDate = model.InitialDate.ToUniversalTime(),
                FinishDate = model.FinishDate.ToUniversalTime(),
                Description = model.Description,
                State = "Pending",
                Comment = model.Comment,
                User = await _context.Users.FindAsync(model.UserId),
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

        public ChangeViewModel ToChangeViewModel(ChangeEntity changeEntity)
        {
            return new ChangeViewModel
            {
                Id = changeEntity.Id,                
                FirstDriver = changeEntity.FirstDriver,
                FirstDriverId = changeEntity.FirstDriver.Id,
                FirstDriverService = changeEntity.FirstDriverService,
                FirstDriverServiceId = changeEntity.FirstDriverService.Id,
                SecondDriver = changeEntity.SecondDriver,
                SecondDriverId = changeEntity.SecondDriver.Id,
                SecondDriverService = changeEntity.SecondDriverService,
                SecondDriverServiceId= changeEntity.SecondDriverService.Id,
            };

        }

        public async Task<ShiftEntity> ToShiftEntityAsync(ShiftViewModel model, bool isNew)
        {
            return new ShiftEntity
            {
                Id = isNew ? 0 : model.Id,
                User = await _context.Users.FindAsync(model.User),
                Service = await _context.Services.FindAsync(model.Service),
                Date = model.Date.ToUniversalTime()
            };

        }



        public ShiftViewModel ToShiftViewModel(ShiftEntity shiftEntity)
        {
            return new ShiftViewModel
            {
                Id = shiftEntity.Id,
                User = shiftEntity.User.Id,
                Service = shiftEntity.Service.Id,
                Date = shiftEntity.Date,
                Drivers = _combosHelper.GetComboDrivers(),
                Services = _combosHelper.GetComboServices()
            };
        }

        public List<ShiftResponse> ToShiftResponse(List<ShiftEntity> shiftEntities)
        {
            List<ShiftResponse> list = new List<ShiftResponse>();
            foreach (ShiftEntity shiftEntity in shiftEntities)
            {
                list.Add(ToShiftResponse(shiftEntity));
            }

            return list;

        }

        public ShiftResponse ToShiftResponse(ShiftEntity shiftEntity)
        {
            return new ShiftResponse
            {
                Id = shiftEntity.Id,
                User = ToUserResponse(shiftEntity.User),
                Service = ToServiceResponse(shiftEntity.Service),
                Date = shiftEntity.Date
            };
        }
        

        public RequestResponse ToRequestResponse(RequestEntity requestEntity)
        {
            return new RequestResponse
            {
                Id = requestEntity.Id,
                Type = ToRequestTypeResponse(requestEntity.Type),
                InitialDate = requestEntity.InitialDate,
                FinishDate = requestEntity.FinishDate,
                Description = requestEntity.Description,
                State = requestEntity.State,
                Comment = requestEntity.Comment,
                User = ToUserResponse(requestEntity.User)
            };
        }

        public List<RequestResponse> ToRequestResponse(List<RequestEntity> requestEntities)
        {
            List<RequestResponse> list = new List<RequestResponse>();
            foreach (RequestEntity requestEntity in requestEntities)
            {
                list.Add(ToRequestResponse(requestEntity));
            }

            return list;

        }

        public ServiceResponse ToServiceResponse(ServiceEntity serviceEntity)
        {
            return new ServiceResponse
            {
                Id = serviceEntity.Id,
                Name = serviceEntity.Name,
                InitialHour = serviceEntity.InitialHour,
                InitialStation = serviceEntity.InitialStation,
                FinalHour = serviceEntity.FinalHour,
                FinalStation = serviceEntity.FinalStation,
                ServiceDetail = ToServiceDetailResponse(serviceEntity.ServiceDetail)
            };
        }

        public List<ServiceResponse> ToServiceResponse(List<ServiceEntity> serviceEntities)
        {
            List<ServiceResponse> list = new List<ServiceResponse>();
            foreach (ServiceEntity serviceEntity in serviceEntities)
            {
                list.Add(ToServiceResponse(serviceEntity));
            }

            return list;
        }

        private RequestTypeResponse ToRequestTypeResponse(RequestTypeEntity requestTypeEntity)
        {
            if (requestTypeEntity == null)
            {
                return null;
            }

            return new RequestTypeResponse
            {
                Id = requestTypeEntity.Id,
                Type = requestTypeEntity.Type,
            };
        }

        private ServiceDetailResponse ToServiceDetailResponse(ServiceDetailEntity serviceDetailEntity)
        {
            if (serviceDetailEntity == null)
            {
                return null;
            }

            return new ServiceDetailResponse
            {
                Id = serviceDetailEntity.Id,
                Description = serviceDetailEntity.Description,
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

        public UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Address = user.Address,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                UserType = user.UserType
            };
        }

        public async Task<ChangeEntity> ToChangeEntityAsync(ChangeViewModel model, bool isNew)
        {

            return new ChangeEntity
            {
                Id = model.Id,
                FirstDriver = await _context.Users.FindAsync(model.FirstDriverId),
                FirstDriverService = await _context.Shifts.FindAsync(model.FirstDriverServiceId),
                SecondDriver = await _context.Users.FindAsync(model.SecondDriverId),
                SecondDriverService = await _context.Shifts.FindAsync(model.SecondDriverServiceId),
                State = "Pending"              
            };

        }

        public async Task<ShiftEntity> ToShiftEntityAsync(ShiftModel model, bool isNew)
        {
            return new ShiftEntity
            {
                Id = model.Id,
                User = await _context.Users.FindAsync(model.UserId),
                Service = await _context.Services.FindAsync(model.ServiceId),
                Date = model.Date
            };
        }

        public ChangeResponse ToChangeResponse(ChangeEntity changeEntity)
        {
            return new ChangeResponse
            {
                Id = changeEntity.Id,
                FirstDriver = ToUserResponse(changeEntity.FirstDriver),
                FirstDriverService = ToShiftResponse(changeEntity.FirstDriverService),
                SecondDriver = ToUserResponse(changeEntity.SecondDriver),
                SecondDriverService = ToShiftResponse(changeEntity.SecondDriverService),
                State = changeEntity.State
            };
        }

    }
}

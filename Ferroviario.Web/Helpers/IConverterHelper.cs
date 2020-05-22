using Ferroviario.Common.Models;
using Ferroviario.Web.Data.Entities;
using Ferroviario.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferroviario.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<RequestEntity> ToRequestEntityAsync(RequestViewModel model, bool isNew);

        RequestViewModel ToRequestViewModel(RequestEntity requestEntity);

        Task<ServiceDetailEntity> ToServiceDetailEntityAsync(ServiceDetailViewModel model, bool isNew);

        ServiceDetailViewModel ToServiceDetailViewModel(ServiceDetailEntity serviceDetailEntity);

        Task<ShiftEntity> ToShiftEntityAsync(ShiftViewModel model, bool isNew);

        Task<ShiftEntity> ToShiftEntityAsync(ShiftModel model, bool isNew);

        ShiftViewModel ToShiftViewModel(ShiftEntity shiftEntity);

        List<ShiftResponse> ToShiftResponse(List<ShiftEntity> shiftEntities);

        ShiftResponse ToShiftResponse(ShiftEntity shiftEntity);

        RequestResponse ToRequestResponse(RequestEntity requestEntity);

        List<RequestResponse> ToRequestResponse(List<RequestEntity> requestEntities);

        ServiceResponse ToServiceResponse(ServiceEntity serviceEntity);

        List<ServiceResponse> ToServiceResponse(List<ServiceEntity> serviceEntities);

        UserResponse ToUserResponse(UserEntity user);

        Task<ChangeEntity> ToChangeEntityAsync(ChangeViewModel model, bool isNew);

        ChangeViewModel ToChangeViewModel(ChangeEntity changeEntity);



    }
}

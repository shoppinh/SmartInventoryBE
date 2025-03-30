using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeSpace.Core.ProjectAggregate.ViewModels.Response;

namespace Smart_Inventory_BE.Controllers.Base
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        // protected readonly IUserRepository UserRepository;

        // public ApiControllerBase(IUserRepository userRepository)
        // {
        //     UserRepository = userRepository;
        // }

        // protected WorkContext? GetCurrentUserContext(bool isLoginRequired = true)
        // {
        //     var currentUser = HttpContext.Items[nameof(WorkContext)] as WorkContext;
        //     if (isLoginRequired && (currentUser is null || currentUser!.CurrentUser is null))
        //     {
        //         throw new UnauthorizedAccessException(message: ApiResponseMessageConstant.AuthControllerBase_AccessIsDenied);
        //     }

        //     return currentUser;
        // }

        protected ApiResponse<T> CreateResponse<T>(bool isSuccess, T data, string messageCode, string message)
        {
            return new ApiResponse<T>
            {
                IsSuccess = isSuccess,
                Data = data,
                Message = message,
                MessageCode = messageCode
            };
        }

        protected ApiResponse<T> CreateSuccessResponse<T>(T data, string messageCode, string message)
        {
            return CreateResponse(true, data, messageCode, message);
        }

        protected ApiResponse CreateSuccessResponse(string messageCode, string message)
        {
            return new ApiResponse
            {
                IsSuccess = true,
                Message = message,
                MessageCode = messageCode
            };
        }

        protected ApiResponse CreateSuccessResponse()
        {
            return new ApiResponse
            {
                IsSuccess = true,
            };
        }

        protected ApiResponse<T> CreateSuccessResponse<T>(T data)
        {
            return new ApiResponse<T>
            {
                IsSuccess = true,
                Data = data
            };
        }
    }
}
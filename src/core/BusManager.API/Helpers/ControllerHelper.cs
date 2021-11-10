using BusManager.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusManager.API.Helpers
{
    public static class ControllerHelper
    {
        public static int? GetUserId(ControllerBase controllerBase)
        {
            var claimsUserId = controllerBase.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claimsUserId == null)
            {
                return null;
            }

            if (!Int32.TryParse(claimsUserId.Value, out int userId))
            {
                throw new InvalidOperationException("Invalid user id");
            }
            return userId;
        }

        //public static UserInfo GetUserInfo(ControllerBase controllerBase)
        //{
        //    var user = controllerBase.User;

        //    return new UserInfo()
        //    {
        //        IsAuthenticated = user.Identity.IsAuthenticated
        //    };
        //}
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using walkinthepark.Data;
using walkinthepark.Services.Interfaces;

namespace walkinthepark.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserService(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // Find logged in hiker's Application ID
        public string FindRegisteredUserId() => _signInManager.Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}

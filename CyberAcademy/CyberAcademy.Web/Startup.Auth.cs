﻿using CyberAcademy.Web.DataAccess;
using CyberAcademy.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyberAcademy.Web
{
    public partial class Startup
    {
        public static Func<UserManager<AppUser,Guid>> UserManagerFactory { get; private set; } = Create;
        public static Func<RoleManager<AppRole,Guid>> RoleManagerFactory { get; private set; } = RoleCreate;
        public static void ConfigureAuth(IAppBuilder app)
        {
            var option = new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                CookieName = "CyberAcademy",
                LoginPath = new PathString("/Auth/Login")
            };
            app.UseCookieAuthentication(option);
        }

        public static UserManager<AppUser,Guid> Create()
        {
            var dbContext = new AcademyDbContext();
            var store = new UserStore<AppUser, AppRole, Guid, AppUserLogin, AppUserRole, AppUserClaim>(dbContext);
            var usermanager = new UserManager<AppUser, Guid> (store);
            // allow alphanumeric characters in username
            usermanager.UserValidator = new UserValidator<AppUser, Guid>(usermanager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false,
            };

            usermanager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 4,
                RequireDigit = false,
                RequireUppercase = false
            };

            return usermanager;
        }

        public static RoleManager<AppRole,Guid> RoleCreate()
        {
            var dbContext = new AcademyDbContext();
            var store = new RoleStore<AppRole,Guid,AppUserRole>(dbContext);
            var rolemanager = new RoleManager<AppRole, Guid>(store);
            return rolemanager;
        }
    }
}
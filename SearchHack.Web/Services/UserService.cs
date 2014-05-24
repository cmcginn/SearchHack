using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SearchHack.Web.Models;
using System.Security.Principal;
namespace SearchHack.Web.Services
{
    public class UserService
    {
        private UserManager<ApplicationUser> um =
      new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public ApplicationUser GetApplicationUser(IPrincipal user)
        {
            return um.FindByName<ApplicationUser>(user.Identity.Name);
        }
    }
}
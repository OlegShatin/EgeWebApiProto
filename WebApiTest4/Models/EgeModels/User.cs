using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApiTest4.Models.EgeModels
{
    public class User : IdentityUser<int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public DateTime? CreatedAt { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        [DefaultValue(0)]
        public int Points { get; set; }
        [DefaultValue(0)]
        public int UsePoints { get; set; }

        public virtual List<Train> Trains { get; set; }
        public virtual List<EgeTrain> EgeTrains{ get; set; }
        public virtual List<FreeTrain> FreeTrains { get; set; }
        public virtual List<Badge> Badges { get; set; }
    }
}
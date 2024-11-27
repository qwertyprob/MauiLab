using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Domain.Entities.UserData;

namespace Telemedicine.BusinessLogic.DataBaseModel
{
    class UserContext : DbContext
    {
        public UserContext() : base("name=Telemedicine")
        {
        }

        public virtual DbSet<UserProfile> Users { get; set; }
    }
}

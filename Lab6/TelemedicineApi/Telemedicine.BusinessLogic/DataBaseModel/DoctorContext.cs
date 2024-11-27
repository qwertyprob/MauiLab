using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Domain.Entities.DocData;
using Telemedicine.Domain.Entities.UserData;

namespace Telemedicine.BusinessLogic.DataBaseModel
{
    public class DoctorContext : DbContext
    {
        public DoctorContext() : base("name=Telemedicine")
        {
        }

        public virtual DbSet<DocProfile> Docs { get; set; }
    }
}

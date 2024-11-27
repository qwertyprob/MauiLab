using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Domain.Entities.DocData;

namespace Telemedicine.BusinessLogic.DataBaseModel
{
    class ConsultationContext : DbContext
    {
        public ConsultationContext() : base("name=Telemedicine")
        {
        }

        public virtual DbSet<DocConsult> Cons { get; set; }
    }
}

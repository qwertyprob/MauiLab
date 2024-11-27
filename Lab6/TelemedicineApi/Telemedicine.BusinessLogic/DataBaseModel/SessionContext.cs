using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Domain.Entities.Session;

namespace Telemedicine.BusinessLogic.DataBaseModel
{
    class SessionContext : DbContext
    {
        public SessionContext() : base("name=Telemedicine")
        {
        }

        public virtual DbSet<Token> Tokens { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.BusinessLogic.BusinessLogic;
using Telemedicine.BusinessLogic.Interfaces;

namespace Telemedicine.BusinessLogic
{
   public class GlobalBusinessLogic
    {
        public IMainApp GetMainAppBL()
        {
            return new MainAppBL();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Domain.Entities.DocData;
using Telemedicine.Domain.Entities.Responses;
using Telemedicine.Domain.Entities.UserData;

namespace Telemedicine.BusinessLogic.Interfaces
{
    public interface IMainApp
    {
        bool ValidateSession(string token);


        ResponseMessage RegNewUser(UserProfile profile);
        ResponseMessage UserLogIn(UserLogin data);

        UserMinimalData GetUserProfile(UserOnAction action);

        IEnumerable<DocMinimalProfile> GetDocsList(string path);

        DocMinimalProfile GetDoc(string path, int id);

        DocConsultMinimal AddConsultation(DocConsult consultation);
    }
}

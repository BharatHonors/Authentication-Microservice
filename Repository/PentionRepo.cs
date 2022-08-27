using AuthenticationMicroservice.Model;
using System.Collections.Generic;
namespace AuthenticationMicroservice.Repository
{
    public class PentionRepo: IPensionRepo
    {
        List<PentionCredentials> listOfCredentials = new List<PentionCredentials>()
        {
            new PentionCredentials(){Username = "user01", Password = "123"},
            new PentionCredentials(){Username = "user02", Password = "123"},
            new PentionCredentials(){Username = "user03", Password = "123"},
            new PentionCredentials(){Username = "user04", Password = "123"}
        };
        public PentionCredentials GetPentionCredentials(PentionCredentials cred)
        {
            PentionCredentials res = listOfCredentials.Find(x => x.Username == cred.Username && x.Password == cred.Password);
            return res;
        }

        public List<PentionCredentials> GetAll()
        {
            return listOfCredentials;
        }

    }   
}

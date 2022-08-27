using AuthenticationMicroservice.Model;
namespace AuthenticationMicroservice.Repository
{
    public interface IPensionRepo
    {
        public PentionCredentials GetPentionCredentials(PentionCredentials cred);

    }
}
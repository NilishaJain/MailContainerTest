using MailContainerTest.Models;

namespace MailContainerTest.Services.IServices
{
    public interface IMailDataStoreContainerService
    {
        public MailContainer GetMailContainer(string sourceMailContainerNumber);
        public void UpdateContainerCapacity(MailContainer mailContainer, int noOfMailItems);
    }
}

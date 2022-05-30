using MailContainerTest.Models;

namespace MailContainerTest.Data.IRepository
{
    public interface IMailDataStoreRepository
    {
        public MailContainer GetMailContainer(string mailContainerNumber);

        public void UpdateMailContainer(MailContainer mailContainer);
    }
}

using MailContainerTest.Data.IRepository;
using MailContainerTest.Models;

namespace MailContainerTest.Data.Repository
{
    public class BackupMailContainerDataStoreRepository : IMailDataStoreRepository
    {
        public MailContainer GetMailContainer(string mailContainerNumber)
        {
            // Access the database and return the retrieved mail container. Implementation not required for this exercise.
            return new MailContainer();
        }

        public void UpdateMailContainer(MailContainer mailContainer)
        {
            // Update mail container in the database. Implementation not required for this exercise.
        }
    }
}

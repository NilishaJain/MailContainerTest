using MailContainerTest.Data.IRepository;
using MailContainerTest.Data.Repository;
using MailContainerTest.Models;
using MailContainerTest.Services.IServices;

namespace MailContainerTest.Services.Services
{
    public class MailDataStoreContainerService : IMailDataStoreContainerService
    {
        private  IMailDataStoreRepository _mailDataStoreRepository;

        public MailDataStoreContainerService(IMailDataStoreRepository mailDataStoreRepository)
        {
            this._mailDataStoreRepository = mailDataStoreRepository;
        }

        public MailContainer GetMailContainer(string sourceMailContainerNumber)
        {
            return _mailDataStoreRepository.GetMailContainer(sourceMailContainerNumber);
        }

        public void UpdateContainerCapacity(MailContainer mailContainer, int noOfMailItems)
        {
            mailContainer.Capacity -= noOfMailItems;
            _mailDataStoreRepository.UpdateMailContainer(mailContainer);
        }
    }
}

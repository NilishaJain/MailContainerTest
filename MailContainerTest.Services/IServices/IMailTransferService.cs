using MailContainerTest.Models;

namespace MailContainerTest.Services.IServices
{
    public interface IMailTransferService
    {
        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request);

       

    }
}

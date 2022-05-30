using MailContainerTest.Models;
using MailContainerTest.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using static MailContainerTest.Models.Enums;

namespace MailContainerTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailTransferController : ControllerBase
    {
       
        private Func<MailType, IMailTransferService> _mailTransferDelegate;
      
        public MailTransferController(ILogger<MailTransferController> logger,
            Func<MailType, IMailTransferService> mailTransferDelegate)
        {           
            _mailTransferDelegate = mailTransferDelegate;
        }

        
        [HttpPost]

        public bool MakeMailTransfer([FromBody] MakeMailTransferRequest makeMailTransferRequest)
        {
            IMailTransferService mailTransferService = _mailTransferDelegate(makeMailTransferRequest.MailType);

            MakeMailTransferResult result = mailTransferService.MakeMailTransfer(makeMailTransferRequest);
            return result.Success;
        }
    }
}

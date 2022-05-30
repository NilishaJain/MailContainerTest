using MailContainerTest.Models;
using MailContainerTest.Services.IServices;
using MailContainerTest.Services.MailContainerValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailContainerTest.Services.Services
{
    public class SmallParcelMailTransferService : IMailTransferService
    {
        private IMailDataStoreContainerService _mailDataStoreContainerService;
        private readonly List<IValidation<SmallParcelValidation>> _validations;

        public SmallParcelMailTransferService(IMailDataStoreContainerService mailDataStoreContainerService)
        {
            this._mailDataStoreContainerService = mailDataStoreContainerService;
            this._validations = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                  .Where(type => type.GetInterfaces().Contains(typeof(IValidation<SmallParcelValidation>)))
                  .Select(e => Activator.CreateInstance(e)).Cast<IValidation<SmallParcelValidation>>().ToList();

        }
        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {

            MailContainer mailContainer = _mailDataStoreContainerService.GetMailContainer(request.SourceMailContainerNumber);

            MakeMailTransferResult result = ValidateMailTransfer(mailContainer);

            if (result.Success)
            {
                _mailDataStoreContainerService.UpdateContainerCapacity(mailContainer, request.NumberOfMailItems);
            }

            return result;
        }

        private MakeMailTransferResult ValidateMailTransfer(MailContainer mailContainer)
        {
            var result = new MakeMailTransferResult();

            result.Success = true;

            SmallParcelValidation smallParcelValidation = new SmallParcelValidation()
            {
                mailContainer = mailContainer
            };

            List<bool> validationsResult = new List<bool>();
            _validations.ForEach(x => validationsResult.Add(x.Validate(smallParcelValidation)));

            if (validationsResult.Any(x => !x))
            {
                result.Success = false;
            }

            return result;
        }
    }
}

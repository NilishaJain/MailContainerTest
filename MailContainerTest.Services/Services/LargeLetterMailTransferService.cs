using MailContainerTest.Models;
using MailContainerTest.Services.IServices;
using MailContainerTest.Services.MailContainerValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailContainerTest.Services.Services
{
    public class LargeLetterMailTransferService : IMailTransferService
    {

        private readonly IMailDataStoreContainerService _mailDataStoreContainerService;
        private readonly List<IValidation<LargeLetterValidation>> _validations;
        public LargeLetterMailTransferService(IMailDataStoreContainerService mailDataStoreContainerService)
        {
            this._mailDataStoreContainerService = mailDataStoreContainerService;

            this._validations = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                  .Where(type => type.GetInterfaces().Contains(typeof(IValidation<LargeLetterValidation>)))
                  .Select(e => Activator.CreateInstance(e)).Cast<IValidation<LargeLetterValidation>>().ToList();
        }
        public MakeMailTransferResult MakeMailTransfer(MakeMailTransferRequest request)
        {

            MailContainer mailContainer = _mailDataStoreContainerService.GetMailContainer(request.SourceMailContainerNumber);

            MakeMailTransferResult result = ValidateMailTransfer(mailContainer, request);

            if (result.Success)
            {
                _mailDataStoreContainerService.UpdateContainerCapacity(mailContainer, request.NumberOfMailItems);
            }
            return result;
        }

        private MakeMailTransferResult ValidateMailTransfer(MailContainer mailContainer, MakeMailTransferRequest request)
        {

            MakeMailTransferResult result = new MakeMailTransferResult();

            result.Success = true;

            LargeLetterValidation largeLetterValidation = new LargeLetterValidation()
            {
                mailContainer = mailContainer,
                transferRequest = request
            };

            List<bool> validationsResult = new List<bool>();
            _validations.ForEach(x => validationsResult.Add(x.Validate(largeLetterValidation)));

            if (validationsResult.Any(x => !x))
            {
                result.Success = false;
            }

            return result;
        }
    }
}

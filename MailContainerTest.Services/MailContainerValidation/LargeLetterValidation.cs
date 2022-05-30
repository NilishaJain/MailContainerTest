using MailContainerTest.Models;
using static MailContainerTest.Models.Enums;

namespace MailContainerTest.Services.MailContainerValidation
{
    public class LargeLetterValidation
    {
        public MailContainer mailContainer { get; set; }

        public MakeMailTransferRequest transferRequest { get; set; }
    }

    public class MailContainerValidation : IValidation<LargeLetterValidation>
    {
        public bool Validate(LargeLetterValidation container)
        {
            if (container.mailContainer == null)
                return false;

            return true;
        }
    }

    public class LargeLetterMailTypeValidation : IValidation<LargeLetterValidation>
    {
        public bool Validate(LargeLetterValidation container)
        {

            if (container.mailContainer != null && !container.mailContainer.AllowedMailType.HasFlag(AllowedMailType.LargeLetter))
                return false;


            return true;
        }
    }

    public class MailContainerCapacityValidation : IValidation<LargeLetterValidation>
    {
        public bool Validate(LargeLetterValidation container)
        {

            if (container.mailContainer != null && container.transferRequest != null && container.mailContainer.Capacity < container.transferRequest.NumberOfMailItems)
                return false;

            return true;
        }
    }


}

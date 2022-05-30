using MailContainerTest.Models;
using static MailContainerTest.Models.Enums;

namespace MailContainerTest.Services.MailContainerValidation
{
    public class StandardLetterValidation
    {
        public MailContainer mailContainer { get; set; }

        public class MailContainerValidation : IValidation<StandardLetterValidation>
        {
            public bool Validate(StandardLetterValidation container)
            {
                if (container.mailContainer == null)
                    return false;

                return true;
            }
        }

        public class SmallParcelMailTypeValidation : IValidation<StandardLetterValidation>
        {
            public bool Validate(StandardLetterValidation container)
            {

                if (container.mailContainer != null && !container.mailContainer.AllowedMailType.HasFlag(AllowedMailType.StandardLetter))
                    return false;

                return true;
            }
        }

    }
}

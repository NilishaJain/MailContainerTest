using MailContainerTest.Models;
using static MailContainerTest.Models.Enums;

namespace MailContainerTest.Services.MailContainerValidation
{
    public class SmallParcelValidation
    {
        public MailContainer mailContainer { get; set; }

        public class MailContainerValidation : IValidation<SmallParcelValidation>
        {
            public bool Validate(SmallParcelValidation container)
            {
                if (container.mailContainer == null)
                    return false;

                return true;
            }
        }

        public class SmallParcelMailTypeValidation : IValidation<SmallParcelValidation>
        {
            public bool Validate(SmallParcelValidation container)
            {

                if (container.mailContainer != null && !container.mailContainer.AllowedMailType.HasFlag(AllowedMailType.SmallParcel))
                    return false;

                return true;
            }
        }

        public class MailContainerStatusValidation : IValidation<SmallParcelValidation>
        {
            public bool Validate(SmallParcelValidation container)
            {

                if (container.mailContainer != null && container.mailContainer.Status != MailContainerStatus.Operational)
                    return false;

                return true;
            }
        }



    }
}

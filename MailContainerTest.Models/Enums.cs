namespace MailContainerTest.Models
{
    public class Enums
    {
        public enum MailType
        {
            StandardLetter = 1,
            LargeLetter = 2,
            SmallParcel = 3
        }

        public enum AllowedMailType
        {
            StandardLetter = 1,
            LargeLetter = 2,
            SmallParcel = 3
        }

        public enum MailContainerStatus
        {
            Operational,
            OutOfService,
            NoTransfersIn
        }

        public enum DataStore
        {
            Backup = 1            
        }
    }
}

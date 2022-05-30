using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MailContainerTest.Models.Enums;

namespace MailContainerTest.Models
{
    public class MailContainer
    {
        public string MailContainerNumber { get; set; }
        public int Capacity { get; set; }
        public MailContainerStatus Status { get; set; }
        public AllowedMailType AllowedMailType { get; set; }
    }
}

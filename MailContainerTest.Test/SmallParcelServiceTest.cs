using MailContainerTest.Models;
using MailContainerTest.Services.IServices;
using MailContainerTest.Services.Services;
using Moq;
using NUnit.Framework;

namespace MailContainerTest.Test
{
    public class SmallParcelServiceTest
    {
        private IMailTransferService smallParcelMailTransferService;
        private Mock<IMailDataStoreContainerService> mockMailDataStoreContainerService;
        private MakeMailTransferRequest makeMailTransferRequest;

        [SetUp]
        public void Setup()
        {
            this.mockMailDataStoreContainerService = new Mock<IMailDataStoreContainerService>(MockBehavior.Strict);

            this.smallParcelMailTransferService = new SmallParcelMailTransferService(mockMailDataStoreContainerService.Object);

            this.makeMailTransferRequest = new MakeMailTransferRequest()
            {
                DestinationMailContainerNumber = "3",
                MailType = Models.Enums.MailType.SmallParcel,
                NumberOfMailItems = 2,
                SourceMailContainerNumber = "2",
                TransferDate = System.DateTime.Now
            };
        }

        [Test]
        public void Should_Return_True_When_ValidationPasses()
        {

            var mailContainerObject = mockMailDataStoreContainerService.Setup(r => r.GetMailContainer(makeMailTransferRequest.SourceMailContainerNumber)).Returns(new Models.MailContainer()
            {
                AllowedMailType = Models.Enums.AllowedMailType.SmallParcel,
                Capacity = 100,
                MailContainerNumber = "2",
                Status = Models.Enums.MailContainerStatus.Operational
            });

            mockMailDataStoreContainerService.Setup(r => r.UpdateContainerCapacity(mockMailDataStoreContainerService.Object.GetMailContainer(makeMailTransferRequest.SourceMailContainerNumber), makeMailTransferRequest.NumberOfMailItems));

            var actual = smallParcelMailTransferService.MakeMailTransfer(makeMailTransferRequest);

            Assert.AreEqual(true, actual.Success);
        }

        [Test]
        public void Should_Return_False_When_MailContainerIsNull()
        {
            this.makeMailTransferRequest = new MakeMailTransferRequest()
            {
                DestinationMailContainerNumber = "3",
                MailType = Models.Enums.MailType.SmallParcel,
                NumberOfMailItems = 2,
                SourceMailContainerNumber = "2",
                TransferDate = System.DateTime.Now
            };

            var mailContainerObject = mockMailDataStoreContainerService.Setup(r => r.GetMailContainer(makeMailTransferRequest.SourceMailContainerNumber)).Returns((MailContainer)null);

            mockMailDataStoreContainerService.Setup(r => r.UpdateContainerCapacity(mockMailDataStoreContainerService.Object.GetMailContainer(makeMailTransferRequest.SourceMailContainerNumber), makeMailTransferRequest.NumberOfMailItems));

            var actual = smallParcelMailTransferService.MakeMailTransfer(makeMailTransferRequest);

            Assert.AreEqual(false, actual.Success);
        }

        [Test]
        public void Should_Return_False_When_MailTypeIsNotSmallLetter()
        {
            this.makeMailTransferRequest = new MakeMailTransferRequest()
            {
                DestinationMailContainerNumber = "3",
                MailType = Models.Enums.MailType.StandardLetter,
                NumberOfMailItems = 2,
                SourceMailContainerNumber = "2",
                TransferDate = System.DateTime.Now
            };

            var mailContainerObject = mockMailDataStoreContainerService.Setup(r => r.GetMailContainer(makeMailTransferRequest.SourceMailContainerNumber)).Returns(new Models.MailContainer()
            {
                AllowedMailType = Models.Enums.AllowedMailType.StandardLetter,
                Capacity = 100,
                MailContainerNumber = "2",
                Status = Models.Enums.MailContainerStatus.Operational
            });

            mockMailDataStoreContainerService.Setup(r => r.UpdateContainerCapacity(mockMailDataStoreContainerService.Object.GetMailContainer(makeMailTransferRequest.SourceMailContainerNumber), makeMailTransferRequest.NumberOfMailItems));

            var actual = smallParcelMailTransferService.MakeMailTransfer(makeMailTransferRequest);

            Assert.AreEqual(false, actual.Success);
        }
        
        [Test]
        public void Should_Return_False_When_Status_IsNot_Operational()
        {
            this.makeMailTransferRequest = new MakeMailTransferRequest()
            {
                DestinationMailContainerNumber = "3",
                MailType = Models.Enums.MailType.SmallParcel,
                NumberOfMailItems = 2,
                SourceMailContainerNumber = "2",
                TransferDate = System.DateTime.Now
            };

            var mailContainerObject = mockMailDataStoreContainerService.Setup(r => r.GetMailContainer(makeMailTransferRequest.SourceMailContainerNumber)).Returns(new Models.MailContainer()
            {
                AllowedMailType = Models.Enums.AllowedMailType.SmallParcel,
                Capacity = 100,
                MailContainerNumber = "2",
                Status = Models.Enums.MailContainerStatus.OutOfService
            });

            mockMailDataStoreContainerService.Setup(r => r.UpdateContainerCapacity(mockMailDataStoreContainerService.Object.GetMailContainer(makeMailTransferRequest.SourceMailContainerNumber), makeMailTransferRequest.NumberOfMailItems));

            var actual = smallParcelMailTransferService.MakeMailTransfer(makeMailTransferRequest);

            Assert.AreEqual(false, actual.Success);
        }


    }
}

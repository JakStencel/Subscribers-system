using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Data;
using System;

namespace SubscribersSystem.Business.Tests
{
    [TestClass]
    public class PhoneServiceTests
    {
        PhoneBl validPhone = new PhoneBl
        {
            Offer = new OfferBl
            {
                PricePerMinute = 0.60M
            },
            CostOfConnectionsOutsideBundle = 0,
            SecondsLeftInBundle = 35
        };
 
        [TestMethod]
        public void GetPriceOfTheConnection_ValidPhoneAndTimeOfConnection_CorrectValue()
        {
            //Arrange
            const int TIME_OF_CONNECTION = 50;
            const decimal TOTAL_COST = 50 * 0.60M / 60;

            var dataObjectMapperMock = new Mock<IDataObjectMapper>();
            var subscribersSystemDbContextMock = new Mock<ISubscribersSystemDbContext>();

            var phoneService = new PhoneService(() => subscribersSystemDbContextMock.Object, dataObjectMapperMock.Object);

            //Act
            var result = phoneService.GetPriceOfTheConnection(validPhone, TIME_OF_CONNECTION);

            //Assert
            Assert.AreEqual(TOTAL_COST, result);
        }

        [TestMethod]
        public void CheckIfBundleOfMinutesExceeded_SecondsLeftInBundle_CorrectBooleanValue()
        {
            //Arrange

            var dataObjectMapperMock = new Mock<IDataObjectMapper>();
            var subscribersSystemDbContextMock = new Mock<ISubscribersSystemDbContext>();

            var phoneService = new PhoneService(() => subscribersSystemDbContextMock.Object, dataObjectMapperMock.Object);

            //Act
            var result = phoneService.CheckIfBundleOfMinutesExceeded(validPhone.SecondsLeftInBundle);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DecreaseBundleOfMinutes_ValidPhoneAndConnection_DecreasedValue()
        {
            //Arrange
            ConnectionBl validConnection = new ConnectionBl
            {
                TimeOfConnectionInSeconds = 20
            };

            int secondsLeft = validPhone.SecondsLeftInBundle - validConnection.TimeOfConnectionInSeconds;

            var dataObjectMapperMock = new Mock<IDataObjectMapper>();
            var subscribersSystemDbContextMock = new Mock<ISubscribersSystemDbContext>();

            var phoneService = new PhoneService(() => subscribersSystemDbContextMock.Object, dataObjectMapperMock.Object);

            //Act
            var result = phoneService.DecreaseBundleOfMinutes(validPhone, validConnection);

            //Assert
            Assert.AreEqual(secondsLeft, result);
        }
    }
}

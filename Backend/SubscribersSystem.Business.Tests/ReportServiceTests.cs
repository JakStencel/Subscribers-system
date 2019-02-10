using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SubscribersSystem.Business.Models;
using SubscribersSystem.Business.Services;
using SubscribersSystem.Data;

namespace SubscribersSystem.Business.Tests
{
    [TestClass]
    public class ReportServiceTests
    {
        SubscriberBl validSubscriber = new SubscriberBl
        {
            Phones =
            {
                new PhoneBl
                {
                    CostOfConnectionsOutsideBundle = 25,
                    CostOfMessagesOutsideBundle = 3.45M,
                    Offer = new OfferBl{ PriceOfTheOffer = 15.99M }
                },
                new PhoneBl
                {
                    CostOfConnectionsOutsideBundle = 24,
                    CostOfMessagesOutsideBundle = 0.45M,
                    Offer = new OfferBl{ PriceOfTheOffer = 25M }
                },
                new PhoneBl
                {
                    CostOfConnectionsOutsideBundle = 15,
                    CostOfMessagesOutsideBundle = 0.90M,
                    Offer = new OfferBl{ PriceOfTheOffer = 20M }
                }
            }
        };

        [TestMethod]
        public void GetTotalCostOfConnections_ValidSubscriber_CorrectValue()
        {
            //Arrange
            var reportService = new ReportService();

            //Act
            var result = reportService.GetTotalCostOfConnections(validSubscriber);

            //Assert
            Assert.AreEqual(64, result);
        }

        [TestMethod]
        public void GetTotalCostBasedOnOffers_ValidSubscriber_CorrectValue()
        {
            //Arrange
            const decimal TOTAL_OFFERS_COST = 25 + 20 + 15.99M;

            var reportService = new ReportService();

            //Act
            var result = reportService.GetTotalCostBasedOnOffers(validSubscriber);

            //Assert
            Assert.AreEqual(TOTAL_OFFERS_COST, result);
        }

        [TestMethod]
        public void GetTotalCostToBePaid_ValidSubscriber_CorrectValue()
        {
            //Arrange
            const decimal TOTAL_COST = 25 + 20 + 15.99M + 3.45M + 0.45M + 0.90M + 25 + 24 + 15;

            var reportService = new ReportService();

            //Act
            var result = reportService.GetTotalCostToBePaid(validSubscriber);

            //Assert
            Assert.AreEqual(TOTAL_COST, result);
        }
    }
}

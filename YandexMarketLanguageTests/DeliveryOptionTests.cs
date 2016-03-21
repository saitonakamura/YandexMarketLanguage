using System;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;

// ReSharper disable RedundantArgumentNameForLiteralExpression
// ReSharper disable RedundantArgumentName

namespace YandexMarketLanguageTests
{
    [TestFixture]
    public class DeliveryOptionTests : BasicTest
    {
        [Test]
        public void DeliveryOption_GivenCostAndDays_HaveValuesInXml()
        {
            var deliveryOption = new delivery_option(300, 1);

            var xDeliveryOption = new YmlSerializer().ToXDocument(deliveryOption).Root;

            xDeliveryOption.Should().NotBeNull();
            xDeliveryOption.Should().HaveAttribute("cost", "300");
            xDeliveryOption.Should().HaveAttribute("days", "1");
        }

        [Test]
        public void DeliveryOption_GivenCostAndDaysPeriodAndOrderBefore_HaveValuesInXml()
        {
            var deliveryOption = new delivery_option(cost: 0, workDaysFrom: 5, workDaysTo: 7, orderBefore: 14);

            var xDeliveryOption = new YmlSerializer().ToXDocument(deliveryOption).Root;

            xDeliveryOption.Should().NotBeNull();
            xDeliveryOption.Should().HaveAttribute("cost", "0");
            xDeliveryOption.Should().HaveAttribute("days", "5-7");
            xDeliveryOption.Should().HaveAttribute("order-before", "14");
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeWorkDaysTo_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 5, workDaysTo: -7)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeCostWithWorkDaysCount_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: -100, workDays: 1)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeWorkDays_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: 100, workDays: -5)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeCostWithWorkDaysPeriod_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: -100, workDaysFrom: 5, workDaysTo: 7)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeWorkDaysFrom_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: -5, workDaysTo: 7)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenWorkDaysFromGreaterThanWorkDaysFrom_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 7, workDaysTo: 5)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenWorkDaysPeriodGreaterThan3_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 7, workDaysTo: 50)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeOrderBeforeWithWorkDays_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: 100, workDays: 5, orderBefore: -1)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenOrderBeforeBiggerThan24WithWorkDays_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: 100, workDays: 5, orderBefore: 25)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeOrderBeforeWithWorkDaysPeriod_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 5, workDaysTo: 7, orderBefore: -1)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeBiggerThan24WithWorkDaysPeriod_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 5, workDaysTo: 7, orderBefore: 25)).ShouldThrow<ArgumentException>();
        }
    }
}

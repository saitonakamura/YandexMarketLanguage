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
    public class DeliveryOptionTests
    {
        [Test]
        public void DeliveryOptionContructor_GivenNegativeWorkDaysTo_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: 100, _workDaysFrom: 5, _workDaysTo: -7)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeCostWithWorkDaysCount_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: -100, _workDays: 1)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeWorkDays_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: 100, _workDays: -5)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeCostWithWorkDaysPeriod_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: -100, _workDaysFrom: 5, _workDaysTo: 7)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeWorkDaysFrom_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: 100, _workDaysFrom: -5, _workDaysTo: 7)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenWorkDaysFromGreaterThanWorkDaysFrom_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: 100, _workDaysFrom: 7, _workDaysTo: 5)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenWorkDaysPeriodGreaterThan3_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: 100, _workDaysFrom: 7, _workDaysTo: 50)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeOrderBeforeWithWorkDays_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: 100, _workDays: 5, _orderBefore: -1)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenOrderBeforeBiggerThan24WithWorkDays_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: 100, _workDays: 5, _orderBefore: 25)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeOrderBeforeWithWorkDaysPeriod_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: 100, _workDaysFrom: 5, _workDaysTo: 7, _orderBefore: -1)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void DeliveryOptionContructor_GivenNegativeBiggerThan24WithWorkDaysPeriod_ThrowsArgumentException()
        {
            Constructor(() => new delivery_option(_cost: 100, _workDaysFrom: 5, _workDaysTo: 7, _orderBefore: 25)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void TestDeliveryOptionWithDays()
        {
            var deliveryOption = new delivery_option(300, 1);

            var xDeliveryOption = new YmlSerializer().ToXDocument(deliveryOption).Root;

            xDeliveryOption.Should().NotBeNull();
            xDeliveryOption.Should().HaveAttribute("cost", "300");
            xDeliveryOption.Should().HaveAttribute("days", "1");
        }

        [Test]
        public void TestDeliveryOptionWithDaysPeriodAndOrderBefore()
        {
            var deliveryOption = new delivery_option(_cost: 0, _workDaysFrom: 5, _workDaysTo: 7, _orderBefore: 14);

            var xDeliveryOption = new YmlSerializer().ToXDocument(deliveryOption).Root;

            xDeliveryOption.Should().NotBeNull();
            xDeliveryOption.Should().HaveAttribute("cost", "0");
            xDeliveryOption.Should().HaveAttribute("days", "5-7");
            xDeliveryOption.Should().HaveAttribute("order-before", "14");
        }

        public static Action Constructor<T>(Func<T> func)
        {
            return () => func();
        }
    }
}

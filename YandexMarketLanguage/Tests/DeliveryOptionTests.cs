using System;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage.ObjectMapping;
// ReSharper disable RedundantArgumentNameForLiteralExpression
// ReSharper disable RedundantArgumentName

namespace YandexMarketLanguage.Tests
{
    [TestFixture]
    public class DeliveryOptionTests
    {
        [Test]
        public void TestDeliveryOptionConstructor()
        {
            Constructor(() => new delivery_option(cost: -100, workDays: 1)).ShouldThrow<ArgumentException>();
            Constructor(() => new delivery_option(cost: 100, workDays: -5)).ShouldThrow<ArgumentException>();

            Constructor(() => new delivery_option(cost: -100, workDaysFrom: 5, workDaysTo: 7)).ShouldThrow<ArgumentException>();
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: -5, workDaysTo: 7)).ShouldThrow<ArgumentException>();
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 5, workDaysTo: -7)).ShouldThrow<ArgumentException>();
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 7, workDaysTo: 5)).ShouldThrow<ArgumentException>();
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 7, workDaysTo: 50)).ShouldThrow<ArgumentException>();

            Constructor(() => new delivery_option(cost: 100, workDays: 5, order_before: -1)).ShouldThrow<ArgumentException>();
            Constructor(() => new delivery_option(cost: 100, workDays: 5, order_before: -25)).ShouldThrow<ArgumentException>();

            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 5, workDaysTo: 7, order_before: -1)).ShouldThrow<ArgumentException>();
            Constructor(() => new delivery_option(cost: 100, workDaysFrom: 5, workDaysTo: 7, order_before: -25)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void TestDeliveryOptionWithDays()
        {
            var deliveryOption = new delivery_option(300, 1);

            var xDeliveryOption = new YmlSerializer().Serialize(deliveryOption).Root;

            xDeliveryOption.Should().NotBeNull();
            xDeliveryOption.Should().HaveAttribute("cost", "300");
            xDeliveryOption.Should().HaveAttribute("days", "1");
        }

        [Test]
        public void TestDeliveryOptionWithDaysPeriodAndOrderBefore()
        {
            var deliveryOption = new delivery_option(cost: 0, workDaysFrom: 5, workDaysTo: 7, order_before: 14);

            var xDeliveryOption = new YmlSerializer().Serialize(deliveryOption).Root;

            xDeliveryOption.Should().NotBeNull();
            xDeliveryOption.Should().HaveAttribute("cost", "0");
            xDeliveryOption.Should().HaveAttribute("days", "5-7");
            xDeliveryOption.Should().HaveAttribute("order-before", "14");
        }

        static Action Constructor<T>(Func<T> func)
        {
            return () => func();
        }
    }
}

using System;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguageTests
{
    [TestFixture]
    public class AgeTests : BasicTests
    {
        [Test]
        public void Age_GivenUnit_HaveValuesInXml()
        {
            var age = new age(AgeUnit.month);

            var xAge = new YmlSerializer().ToXDocument(age).Root;

            xAge.Should().NotBeNull();
            xAge.Should().HaveAttribute("unit", AgeUnit.month.ToString());
        }

        [Test]
        public void Age_GivenMonthUnitAndValidValue_HaveValuesInXml()
        {
            var age = new age(AgeUnit.month, 7);

            var xAge = new YmlSerializer().ToXDocument(age).Root;

            xAge.Should().NotBeNull();
            xAge.Should().HaveAttribute("unit", AgeUnit.month.ToString());
            xAge.Should().HaveValue(7.ToString());
        }

        [Test]
        public void Age_GivenYearUnitAndValidValue_HaveValuesInXml()
        {
            var age = new age(AgeUnit.year, 12);

            var xAge = new YmlSerializer().ToXDocument(age).Root;

            xAge.Should().NotBeNull();
            xAge.Should().HaveAttribute("unit", AgeUnit.year.ToString());
            xAge.Should().HaveValue(12.ToString());
        }

        [Test]
        public void AgeConstructor_GivenMonthValueGreaterThan12_ThrowsArgumentException()
        {
            Constructor(() => new age(AgeUnit.month, 13)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void AgeConstructor_GivenMonthValueLowerThan0_ThrowsArgumentException()
        {
            Constructor(() => new age(AgeUnit.month, -1)).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void AgeConstructor_GivenInvalidYearValue_ThrowsArgumentException()
        {
            Constructor(() => new age(AgeUnit.year, -1)).ShouldThrow<ArgumentException>();
        }
    }
}

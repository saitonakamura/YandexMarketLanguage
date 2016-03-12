using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguage.Tests
{
    [TestFixture]
    public class OfferTests
    {
        [Test]
        public void TestOffer1()
        {
            var _offer = new offer("12346", 600, CurrencyEnum.USD, 1, "Наручные часы Casio A1234567B");

            var xOffer = new YmlSerializer().Serialize(_offer).Root;

            xOffer.Should().NotBeNull();
            xOffer.Should().HaveAttribute("id", "12346");
            xOffer.Should().HaveElement("price").Which.Should().HaveValue("600");
            xOffer.Should().HaveElement("currencyId").Which.Should().HaveValue(CurrencyEnum.USD.ToString());
            xOffer.Should().HaveElement("categoryId").Which.Should().HaveValue(1.ToString());
            xOffer.Should().HaveElement("name").Which.Should().HaveValue("Наручные часы Casio A1234567B");
        }

        [Test]
        public void TestOffer2()
        {
            var offer = new offer("12341", 16800, CurrencyEnum.RUR, 2, "Принтер НP Deskjet D2663");

            var xOffer = new YmlSerializer().Serialize(offer).Root;

            xOffer.Should().NotBeNull();
            xOffer.Should().HaveAttribute("id", "12341");
            xOffer.Should().HaveElement("price").Which.Should().HaveValue("16800");
            xOffer.Should().HaveElement("currencyId").Which.Should().HaveValue(CurrencyEnum.RUR.ToString());
            xOffer.Should().HaveElement("categoryId").Which.Should().HaveValue(2.ToString());
            xOffer.Should().HaveElement("name").Which.Should().HaveValue("Принтер НP Deskjet D2663");
        }
    }
}

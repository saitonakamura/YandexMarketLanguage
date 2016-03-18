using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguageTests
{
    [TestFixture]
    public class OfferTests
    {
        [Test]
        public void TestOffer1()
        {
            var offer = new offer(_id: "12346", _price: 600, _currencyId: CurrencyEnum.EUR, _categoryId: 1, _name: "Наручные часы Casio A1234567B")
            {
                barcode = new[] { "423424", "43423423" },
            }; 

            var xOffer = new YmlSerializer().ToXDocument(offer).Root;

            xOffer.Should().NotBeNull();
            xOffer.Should().HaveAttribute("id", "12346");
            xOffer.Should().HaveElement("price").Which.Should().HaveValue("600");
            xOffer.Should().HaveElement("currencyId").Which.Should().HaveValue(CurrencyEnum.EUR.ToString());
            xOffer.Should().HaveElement("categoryId").Which.Should().HaveValue(1.ToString());
            xOffer.Should().HaveElement("name").Which.Should().HaveValue("Наручные часы Casio A1234567B");
        }

        [Test]
        public void TestOffer2()
        {
            var offer = new offer(_id: "12341", _price: 16800, _currencyId: CurrencyEnum.RUR, _categoryId: 2, _typePrefix: "Принтер", _vendor: "HP", _model: "Deskjet D2663");

            var xOffer = new YmlSerializer().ToXDocument(offer).Root;

            xOffer.Should().NotBeNull();
            xOffer.Should().HaveAttribute("id", "12341");
            xOffer.Should().HaveAttribute("type", "vendor.model");
            xOffer.Should().HaveElement("price").Which.Should().HaveValue("16800");
            xOffer.Should().HaveElement("currencyId").Which.Should().HaveValue(CurrencyEnum.RUR.ToString());
            xOffer.Should().HaveElement("categoryId").Which.Should().HaveValue(2.ToString());
            xOffer.Should().HaveElement("typePrefix").Which.Should().HaveValue("Принтер");
            xOffer.Should().HaveElement("vendor").Which.Should().HaveValue("HP");
            xOffer.Should().HaveElement("model").Which.Should().HaveValue("Deskjet D2663");
        }
    }
}

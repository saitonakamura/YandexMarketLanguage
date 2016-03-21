using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguageTests
{
    [TestFixture]
    public class OfferTests : BasicTest
    {
        [Test]
        public void OfferSimple_GivenRequiredParameters_ShouldHaveRightProperties()
        {
            var offer = new offer(id: "12346", price: 600, currencyId: CurrencyEnum.EUR, categoryId: 1, name: "Наручные часы Casio A1234567B"); 

            var xOffer = new YmlSerializer().ToXDocument(offer).Root;

            xOffer.Should().NotBeNull();
            xOffer.Should().HaveAttribute("id", "12346");
            xOffer.Should().HaveElement("price").Which.Should().HaveValue("600");
            xOffer.Should().HaveElement("currencyId").Which.Should().HaveValue(CurrencyEnum.EUR.ToString());
            xOffer.Should().HaveElement("categoryId").Which.Should().HaveValue(1.ToString());
            xOffer.Should().HaveElement("name").Which.Should().HaveValue("Наручные часы Casio A1234567B");
        }

        [Test]
        public void OfferVendor_GivenRequiredParameters_ShouldHaveRightProperties()
        {
            var offer = new offer(id: "12341", price: 16800, currencyId: CurrencyEnum.RUR, categoryId: 2, typePrefix: "Принтер", vendor: "HP", model: "Deskjet D2663");

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

        [Test]
        public void OfferSimple_GivenBarcodes_ShouldHaveRightBarcodes()
        {
            var offer = new offer(id: "12346", price: 600, currencyId: CurrencyEnum.EUR, categoryId: 1, name: "Наручные часы Casio A1234567B")
            {
                barcode = new[] { "423424", "43423423", "5353523" },
            };

            var xOffer = new YmlSerializer().ToXDocument(offer).Root;

            xOffer.Should().HaveElement("barcode");
            xOffer.Descendants("barcode").Count().Should().Be(3);
        }

        [Test]
        public void OfferSimple_GivenTypePrefix_ThrowsArgumentException()
        {
            Constructor(() => new offer(id: "12346", price: 600, currencyId: CurrencyEnum.EUR, categoryId: 1, name: "Наручные часы Casio A1234567B")
            {
                typePrefix = "Наручные часы",
            }).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void OfferVendor_GivenName_ThrowsArgumentException()
        {
            Constructor(() => new offer(id: "12341", price: 16800, currencyId: CurrencyEnum.RUR, categoryId: 2, typePrefix: "Принтер", vendor: "HP", model: "Deskjet D2663")
            {
                name = "Принтер HP Deskjet D2663",
            }).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void OfferSimple_GivenSalesNotesGreaterThan50_ThrowsArgumentException()
        {
            Constructor(() => new offer(id: "12346", price: 600, currencyId: CurrencyEnum.EUR, categoryId: 1, name: "Наручные часы Casio A1234567B")
            {
                sales_notes = "Элемент используется для отражения информации о:" +
                              " минимальной сумме заказа, минимальной партии товара, " +
                              "необходимости предоплаты (указание элемента обязательно) " +
                              "вариантов оплаты, описания акций и распродаж " +
                              "(указание элемента необязательно)",
            }).ShouldThrow<ArgumentException>();
        }
    }
}

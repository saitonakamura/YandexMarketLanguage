using System;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;

namespace YandexMarketLanguageTests
{
    [TestFixture]
    public class ParamTests : BasicTests
    {
        [Test]
        public void Param_GivenNameAndValue_HaveValuesInXml()
        {
            var param = new param("Тип", "моноблок");

            var xParam = new YmlSerializer().ToXDocument(param).Root;

            xParam.Should().NotBeNull();
            xParam.Should().HaveAttribute("name", "Тип");
            xParam.Should().HaveValue("моноблок");
        }

        [Test]
        public void Param_GivenNameValueAndUnit_HaveValuesInXml()
        {
            var param = new param("Размер экрана", 27.ToString(), "дюйм");

            var xParam = new YmlSerializer().ToXDocument(param).Root;

            xParam.Should().NotBeNull();
            xParam.Should().HaveAttribute("name", "Размер экрана");
            xParam.Should().HaveAttribute("unit", "дюйм");
            xParam.Should().HaveValue(27.ToString());
        }

        [Test]
        public void ParamName_GivenNullName_ThrowsArgumentException()
        {
            var param = new param("Тип", "моноблок");

            Call(() => param.name = null).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ParamName_GivenEmptyName_ThrowsArgumentException()
        {
            var param = new param("Тип", "моноблок");

            Call(() => param.name = "").ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ParamName_GivenWhitespaceName_ThrowsArgumentException()
        {
            var param = new param("Тип", "моноблок");

            Call(() => param.name = "    ").ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ParamValue_GivenNullValue_ThrowsArgumentException()
        {
            var param = new param("Тип", "моноблок");

            Call(() => param.value = null).ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ParamValue_GivenEmptyValue_ThrowsArgumentException()
        {
            var param = new param("Тип", "моноблок");

            Call(() => param.value = "").ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ParamValue_GivenWhitespaceValue_ThrowsArgumentException()
        {
            var param = new param("Тип", "моноблок");

            Call(() => param.value = "    ").ShouldThrow<ArgumentException>();
        }
    }
}

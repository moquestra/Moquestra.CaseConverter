using System;
using System.Globalization;

using Xunit;

namespace Moquestra.CaseConverter.Tests
{
    public class CaseConverterExtensionsTests
    {
        [Theory]
        [InlineData("MY-DATA-OUTPUT", "MyDataOutput", "myDataOutput", "my_data_output")]
        [InlineData("42user_name", "42userName", "42userName", "42user_name")]
        [InlineData("USER_LOGIN_COUNT", "UserLoginCount", "userLoginCount", "user_login_count")]
        [InlineData("UserID", "UserId", "userId", "user_id")]
        [InlineData("HTTPServer", "HttpServer", "httpServer", "http_server")]
        [InlineData("XML2Value", "Xml2Value", "xml2Value", "xml2value")]
        [InlineData("XML2JsonParser", "Xml2JsonParser", "xml2JsonParser", "xml2json_parser")]
        [InlineData("XML2_JsonParser", "Xml2JsonParser", "xml2JsonParser", "xml2_json_parser")]
        [InlineData("version42beta", "Version42beta", "version42beta", "version42beta")]
        [InlineData("user__name", "UserName", "userName", "user_name")]
        [InlineData("user+ID", "User+Id", "user+Id", "user+id")]
        public void Conversions_WordBoundaries_ReturnExpectedCasing(string value, string expectedPascal, string expectedCamel, string expectedSnake)
        {
            var pascal = value.ToPascalCase();
            var camel = value.ToCamelCase();
            var snake = value.ToSnakeCase();

            Assert.Equal(expectedPascal, pascal);
            Assert.Equal(expectedCamel, camel);
            Assert.Equal(expectedSnake, snake);
        }

        [Fact]
        public void Conversions_TurkishCulture_UseInvariantCasing()
        {
            var previousCulture = CultureInfo.CurrentCulture;

            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

                var pascal = "INPUT_ID".ToPascalCase();
                var camel = "INPUT_ID".ToCamelCase();
                var snake = "INPUT_ID".ToSnakeCase();

                Assert.Equal("InputId", pascal);
                Assert.Equal("inputId", camel);
                Assert.Equal("input_id", snake);
            }
            finally
            {
                CultureInfo.CurrentCulture = previousCulture;
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Conversions_EmptyOrWhitespaceInput_ReturnEmpty(string value)
        {
            var pascal = value.ToPascalCase();
            var camel = value.ToCamelCase();
            var snake = value.ToSnakeCase();

            Assert.Empty(pascal);
            Assert.Empty(camel);
            Assert.Empty(snake);
        }

        [Fact]
        public void Conversions_NullInput_ThrowArgumentNullException()
        {
            var pascal = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToPascalCase());
            var camel = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToCamelCase());
            var snake = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToSnakeCase());

            Assert.Equal("value", pascal.ParamName);
            Assert.Equal("value", camel.ParamName);
            Assert.Equal("value", snake.ParamName);
        }
    }
}

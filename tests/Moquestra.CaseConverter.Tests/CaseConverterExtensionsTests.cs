using System;
using System.Globalization;

using Xunit;

namespace Moquestra.CaseConverter.Tests
{
    public class CaseConverterExtensionsTests
    {
        [Theory]
        [InlineData("MY-DATA-OUTPUT", "MyDataOutput", "myDataOutput", "my_data_output", "my-data-output", "MY_DATA_OUTPUT", "MY-DATA-OUTPUT")]
        [InlineData("42user_name", "42userName", "42userName", "42user_name", "42user-name", "42USER_NAME", "42USER-NAME")]
        [InlineData("USER_LOGIN_COUNT", "UserLoginCount", "userLoginCount", "user_login_count", "user-login-count", "USER_LOGIN_COUNT", "USER-LOGIN-COUNT")]
        [InlineData("UserID", "UserId", "userId", "user_id", "user-id", "USER_ID", "USER-ID")]
        [InlineData("HTTPServer", "HttpServer", "httpServer", "http_server", "http-server", "HTTP_SERVER", "HTTP-SERVER")]
        [InlineData("XML2Value", "Xml2Value", "xml2Value", "xml2value", "xml2value", "XML2VALUE", "XML2VALUE")]
        [InlineData("XML2JsonParser", "Xml2JsonParser", "xml2JsonParser", "xml2json_parser", "xml2json-parser", "XML2JSON_PARSER", "XML2JSON-PARSER")]
        [InlineData("XML2_JsonParser", "Xml2JsonParser", "xml2JsonParser", "xml2_json_parser", "xml2-json-parser", "XML2_JSON_PARSER", "XML2-JSON-PARSER")]
        [InlineData("version42beta", "Version42beta", "version42beta", "version42beta", "version42beta", "VERSION42BETA", "VERSION42BETA")]
        [InlineData("user__name", "UserName", "userName", "user_name", "user-name", "USER_NAME", "USER-NAME")]
        [InlineData("user+ID", "User+Id", "user+Id", "user+id", "user+id", "USER+ID", "USER+ID")]
        public void Conversions_WordBoundaries_ReturnExpectedCasing(string value, string expectedPascal, string expectedCamel, string expectedSnake, string expectedKebab, string expectedScreamingSnake, string expectedCobol)
        {
            var pascal = value.ToPascalCase();
            var camel = value.ToCamelCase();
            var snake = value.ToSnakeCase();
            var kebab = value.ToKebabCase();
            var screamingSnake = value.ToScreamingSnakeCase();
            var cobol = value.ToCobolCase();

            Assert.Equal(expectedPascal, pascal);
            Assert.Equal(expectedCamel, camel);
            Assert.Equal(expectedSnake, snake);
            Assert.Equal(expectedKebab, kebab);
            Assert.Equal(expectedScreamingSnake, screamingSnake);
            Assert.Equal(expectedCobol, cobol);
        }

        [Fact]
        public void Conversions_TurkishCulture_UseInvariantCasing()
        {
            var previousCulture = CultureInfo.CurrentCulture;

            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

                var pascal = "INPUT_id".ToPascalCase();
                var camel = "INPUT_id".ToCamelCase();
                var snake = "INPUT_id".ToSnakeCase();
                var kebab = "INPUT_id".ToKebabCase();
                var screamingSnake = "INPUT_id".ToScreamingSnakeCase();
                var cobol = "INPUT_id".ToCobolCase();

                Assert.Equal("InputId", pascal);
                Assert.Equal("inputId", camel);
                Assert.Equal("input_id", snake);
                Assert.Equal("input-id", kebab);
                Assert.Equal("INPUT_ID", screamingSnake);
                Assert.Equal("INPUT-ID", cobol);
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
            var kebab = value.ToKebabCase();
            var screamingSnake = value.ToScreamingSnakeCase();
            var cobol = value.ToCobolCase();

            Assert.Empty(pascal);
            Assert.Empty(camel);
            Assert.Empty(snake);
            Assert.Empty(kebab);
            Assert.Empty(screamingSnake);
            Assert.Empty(cobol);
        }

        [Fact]
        public void Conversions_NullInput_ThrowArgumentNullException()
        {
            var pascal = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToPascalCase());
            var camel = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToCamelCase());
            var snake = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToSnakeCase());
            var kebab = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToKebabCase());
            var screamingSnake = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToScreamingSnakeCase());
            var cobol = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToCobolCase());

            Assert.Equal("value", pascal.ParamName);
            Assert.Equal("value", camel.ParamName);
            Assert.Equal("value", snake.ParamName);
            Assert.Equal("value", kebab.ParamName);
            Assert.Equal("value", screamingSnake.ParamName);
            Assert.Equal("value", cobol.ParamName);
        }
    }
}

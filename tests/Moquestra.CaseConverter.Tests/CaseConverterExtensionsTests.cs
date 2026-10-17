using System;
using System.Globalization;

using Xunit;

namespace Moquestra.CaseConverter.Tests
{
    public class CaseConverterExtensionsTests
    {
        [Theory]
        [InlineData("MY-DATA-OUTPUT", "MyDataOutput", "myDataOutput", "my_data_output", "my-data-output", "MY_DATA_OUTPUT", "MY-DATA-OUTPUT", "My-Data-Output", "My Data Output", "My-data-output", "My data output")]
        [InlineData("42user_name", "42userName", "42userName", "42user_name", "42user-name", "42USER_NAME", "42USER-NAME", "42user-Name", "42user Name", "42user-name", "42user name")]
        [InlineData("USER_LOGIN_COUNT", "UserLoginCount", "userLoginCount", "user_login_count", "user-login-count", "USER_LOGIN_COUNT", "USER-LOGIN-COUNT", "User-Login-Count", "User Login Count", "User-login-count", "User login count")]
        [InlineData("UserID", "UserId", "userId", "user_id", "user-id", "USER_ID", "USER-ID", "User-Id", "User Id", "User-id", "User id")]
        [InlineData("HTTPServer", "HttpServer", "httpServer", "http_server", "http-server", "HTTP_SERVER", "HTTP-SERVER", "Http-Server", "Http Server", "Http-server", "Http server")]
        [InlineData("XML2Value", "Xml2Value", "xml2Value", "xml2value", "xml2value", "XML2VALUE", "XML2VALUE", "Xml2Value", "Xml2Value", "Xml2value", "Xml2value")]
        [InlineData("XML2JsonParser", "Xml2JsonParser", "xml2JsonParser", "xml2json_parser", "xml2json-parser", "XML2JSON_PARSER", "XML2JSON-PARSER", "Xml2Json-Parser", "Xml2Json Parser", "Xml2json-parser", "Xml2json parser")]
        [InlineData("XML2_JsonParser", "Xml2JsonParser", "xml2JsonParser", "xml2_json_parser", "xml2-json-parser", "XML2_JSON_PARSER", "XML2-JSON-PARSER", "Xml2-Json-Parser", "Xml2 Json Parser", "Xml2-json-parser", "Xml2 json parser")]
        [InlineData("version42beta", "Version42beta", "version42beta", "version42beta", "version42beta", "VERSION42BETA", "VERSION42BETA", "Version42beta", "Version42beta", "Version42beta", "Version42beta")]
        [InlineData("user__name", "UserName", "userName", "user_name", "user-name", "USER_NAME", "USER-NAME", "User-Name", "User Name", "User-name", "User name")]
        [InlineData("user+ID", "User+Id", "user+Id", "user+id", "user+id", "USER+ID", "USER+ID", "User+Id", "User+Id", "User+id", "User+id")]
        public void Conversions_WordBoundaries_ReturnExpectedCasing(string value, string expectedPascal, string expectedCamel, string expectedSnake, string expectedKebab, string expectedScreamingSnake, string expectedCobol, string expectedHeader, string expectedTitle, string expectedTrain, string expectedSentence)
        {
            var pascal = value.ToPascalCase();
            var camel = value.ToCamelCase();
            var snake = value.ToSnakeCase();
            var kebab = value.ToKebabCase();
            var screamingSnake = value.ToScreamingSnakeCase();
            var cobol = value.ToCobolCase();
            var header = value.ToHeaderCase();
            var title = value.ToTitleCase();
            var train = value.ToTrainCase();
            var sentence = value.ToSentenceCase();

            Assert.Equal(expectedPascal, pascal);
            Assert.Equal(expectedCamel, camel);
            Assert.Equal(expectedSnake, snake);
            Assert.Equal(expectedKebab, kebab);
            Assert.Equal(expectedScreamingSnake, screamingSnake);
            Assert.Equal(expectedCobol, cobol);
            Assert.Equal(expectedHeader, header);
            Assert.Equal(expectedTitle, title);
            Assert.Equal(expectedTrain, train);
            Assert.Equal(expectedSentence, sentence);
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
                var header = "INPUT_id".ToHeaderCase();
                var title = "INPUT_id".ToTitleCase();
                var train = "INPUT_id".ToTrainCase();
                var sentence = "INPUT_id".ToSentenceCase();

                Assert.Equal("InputId", pascal);
                Assert.Equal("inputId", camel);
                Assert.Equal("input_id", snake);
                Assert.Equal("input-id", kebab);
                Assert.Equal("INPUT_ID", screamingSnake);
                Assert.Equal("INPUT-ID", cobol);
                Assert.Equal("Input-Id", header);
                Assert.Equal("Input Id", title);
                Assert.Equal("Input-id", train);
                Assert.Equal("Input id", sentence);
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
            var header = value.ToHeaderCase();
            var title = value.ToTitleCase();
            var train = value.ToTrainCase();
            var sentence = value.ToSentenceCase();

            Assert.Empty(pascal);
            Assert.Empty(camel);
            Assert.Empty(snake);
            Assert.Empty(kebab);
            Assert.Empty(screamingSnake);
            Assert.Empty(cobol);
            Assert.Empty(header);
            Assert.Empty(title);
            Assert.Empty(train);
            Assert.Empty(sentence);
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
            var header = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToHeaderCase());
            var title = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToTitleCase());
            var train = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToTrainCase());
            var sentence = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToSentenceCase());

            Assert.Equal("value", pascal.ParamName);
            Assert.Equal("value", camel.ParamName);
            Assert.Equal("value", snake.ParamName);
            Assert.Equal("value", kebab.ParamName);
            Assert.Equal("value", screamingSnake.ParamName);
            Assert.Equal("value", cobol.ParamName);
            Assert.Equal("value", header.ParamName);
            Assert.Equal("value", title.ParamName);
            Assert.Equal("value", train.ParamName);
            Assert.Equal("value", sentence.ParamName);
        }
    }
}

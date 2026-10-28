using System;
using System.Globalization;

using Xunit;

namespace Moquestra.CaseConverter.Tests
{
    public class CaseConverterExtensionsTests
    {
        [Theory]
        [InlineData("MY-DATA-OUTPUT", "MyDataOutput", "myDataOutput", "my_data_output", "my-data-output", "MY_DATA_OUTPUT", "MY-DATA-OUTPUT", "My-Data-Output", "My Data Output", "My-data-output", "My data output", "mydataoutput")]
        [InlineData("42user_name", "42userName", "42userName", "42user_name", "42user-name", "42USER_NAME", "42USER-NAME", "42user-Name", "42user Name", "42user-name", "42user name", "42username")]
        [InlineData("USER_LOGIN_COUNT", "UserLoginCount", "userLoginCount", "user_login_count", "user-login-count", "USER_LOGIN_COUNT", "USER-LOGIN-COUNT", "User-Login-Count", "User Login Count", "User-login-count", "User login count", "userlogincount")]
        [InlineData("UserID", "UserId", "userId", "user_id", "user-id", "USER_ID", "USER-ID", "User-Id", "User Id", "User-id", "User id", "userid")]
        [InlineData("HTTPServer", "HttpServer", "httpServer", "http_server", "http-server", "HTTP_SERVER", "HTTP-SERVER", "Http-Server", "Http Server", "Http-server", "Http server", "httpserver")]
        [InlineData("XML2Value", "Xml2Value", "xml2Value", "xml2value", "xml2value", "XML2VALUE", "XML2VALUE", "Xml2Value", "Xml2Value", "Xml2value", "Xml2value", "xml2value")]
        [InlineData("XML2JsonParser", "Xml2JsonParser", "xml2JsonParser", "xml2json_parser", "xml2json-parser", "XML2JSON_PARSER", "XML2JSON-PARSER", "Xml2Json-Parser", "Xml2Json Parser", "Xml2json-parser", "Xml2json parser", "xml2jsonparser")]
        [InlineData("XML2_JsonParser", "Xml2JsonParser", "xml2JsonParser", "xml2_json_parser", "xml2-json-parser", "XML2_JSON_PARSER", "XML2-JSON-PARSER", "Xml2-Json-Parser", "Xml2 Json Parser", "Xml2-json-parser", "Xml2 json parser", "xml2jsonparser")]
        [InlineData("version42beta", "Version42beta", "version42beta", "version42beta", "version42beta", "VERSION42BETA", "VERSION42BETA", "Version42beta", "Version42beta", "Version42beta", "Version42beta", "version42beta")]
        [InlineData("user__name", "UserName", "userName", "user_name", "user-name", "USER_NAME", "USER-NAME", "User-Name", "User Name", "User-name", "User name", "username")]
        [InlineData("user+ID", "UserId", "userId", "user_id", "user-id", "USER_ID", "USER-ID", "User-Id", "User Id", "User-id", "User id", "userid")]
        public void Conversions_WordBoundaries_ReturnExpectedCasing(string value, string expectedPascal, string expectedCamel, string expectedSnake, string expectedKebab, string expectedScreamingSnake, string expectedCobol, string expectedHeader, string expectedTitle, string expectedTrain, string expectedSentence, string expectedFlat)
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
            var flat = value.ToFlatCase();

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
            Assert.Equal(expectedFlat, flat);
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
                var flat = "INPUT_id".ToFlatCase();

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
                Assert.Equal("inputid", flat);
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
            var flat = value.ToFlatCase();

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
            Assert.Empty(flat);
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
            var flat = Assert.Throws<ArgumentNullException>(() => ((string)null!).ToFlatCase());

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
            Assert.Equal("value", flat.ParamName);
        }

        [Theory]
        [InlineData("user.name", SeparatorOptions.None, "UserName", "userName", "user_name", "user-name", "USER_NAME", "USER-NAME", "User-Name", "User Name", "User-name", "User name", "username")]
        [InlineData("user.name", SeparatorOptions.PreserveInner, "User.Name", "user.Name", "user.name", "user.name", "USER.NAME", "USER.NAME", "User.Name", "User.Name", "User.name", "User.name", "user.name")]
        public void Conversions_SeparatorOptions_ReturnExpectedCasing(string value, SeparatorOptions options, string expectedPascal, string expectedCamel, string expectedSnake, string expectedKebab, string expectedScreamingSnake, string expectedCobol, string expectedHeader, string expectedTitle, string expectedTrain, string expectedSentence, string expectedFlat)
        {
            var pascal = value.ToPascalCase(options);
            var camel = value.ToCamelCase(options);
            var snake = value.ToSnakeCase(options);
            var kebab = value.ToKebabCase(options);
            var screamingSnake = value.ToScreamingSnakeCase(options);
            var cobol = value.ToCobolCase(options);
            var header = value.ToHeaderCase(options);
            var title = value.ToTitleCase(options);
            var train = value.ToTrainCase(options);
            var sentence = value.ToSentenceCase(options);
            var flat = value.ToFlatCase(options);

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
            Assert.Equal(expectedFlat, flat);
        }

        [Theory]
        [InlineData("__user__name__", SeparatorOptions.None, "user_name")]
        [InlineData("__user__name__", SeparatorOptions.PreserveLeading, "__user_name")]
        [InlineData("__user__name__", SeparatorOptions.PreserveTrailing, "user_name__")]
        [InlineData("__user__name__", SeparatorOptions.PreserveInner, "user__name")]
        [InlineData("__user__name__", SeparatorOptions.PreserveLeading | SeparatorOptions.PreserveTrailing, "__user_name__")]
        [InlineData("__user__name__", SeparatorOptions.PreserveLeading | SeparatorOptions.PreserveTrailing | SeparatorOptions.PreserveInner, "__user__name__")]
        [InlineData("user__name", SeparatorOptions.PreserveInner, "user__name")]
        [InlineData("user.+name", SeparatorOptions.PreserveInner, "user.+name")]
        [InlineData("XML2.JsonParser", SeparatorOptions.None, "xml2_json_parser")]
        [InlineData("XML2.JsonParser", SeparatorOptions.PreserveInner, "xml2.json_parser")]
        [InlineData(" User Name ", SeparatorOptions.PreserveLeading | SeparatorOptions.PreserveTrailing | SeparatorOptions.PreserveInner, " user name ")]
        public void ToSnakeCase_SeparatorOptions_ReturnExpectedCasing(string value, SeparatorOptions options, string expected)
        {
            var result = value.ToSnakeCase(options);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToSnakeCase_UndefinedOptions_ThrowsArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => "UserId".ToSnakeCase((SeparatorOptions)8));

            Assert.Equal("options", exception.ParamName);
        }
    }
}

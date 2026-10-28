# Moquestra.CaseConverter

[![Build and test](https://github.com/moquestra/Moquestra.CaseConverter/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/moquestra/Moquestra.CaseConverter/actions/workflows/build-and-test.yml)
[![NuGet](https://img.shields.io/nuget/v/Moquestra.CaseConverter)](https://www.nuget.org/packages/Moquestra.CaseConverter/)

A string extension library for casing conversion.

## Usage

```csharp
using Moquestra.CaseConverter;

var values = new[]
{
    "UserID",
    "User+Id",
    "HTTPServer",
    "XML2JsonParser",
};

foreach (var value in values)
{
    Console.WriteLine(value);
    Console.WriteLine(value.ToPascalCase());
    Console.WriteLine(value.ToCamelCase());
    Console.WriteLine(value.ToSnakeCase());
    Console.WriteLine(value.ToKebabCase());
    Console.WriteLine(value.ToScreamingSnakeCase());
    Console.WriteLine(value.ToCobolCase());
    Console.WriteLine(value.ToHeaderCase());
    Console.WriteLine(value.ToTitleCase());
    Console.WriteLine(value.ToTrainCase());
    Console.WriteLine(value.ToSentenceCase());
    Console.WriteLine(value.ToFlatCase());
}
```

Results with the default options:

| Method | `UserID` | `User+Id` | `HTTPServer` | `XML2JsonParser` |
|---|---|---|---|---|
| `ToPascalCase()` | `UserId` | `UserId` | `HttpServer` | `Xml2JsonParser` |
| `ToCamelCase()` | `userId` | `userId` | `httpServer` | `xml2JsonParser` |
| `ToSnakeCase()` | `user_id` | `user_id` | `http_server` | `xml2json_parser` |
| `ToKebabCase()` | `user-id` | `user-id` | `http-server` | `xml2json-parser` |
| `ToScreamingSnakeCase()` | `USER_ID` | `USER_ID` | `HTTP_SERVER` | `XML2JSON_PARSER` |
| `ToCobolCase()` | `USER-ID` | `USER-ID` | `HTTP-SERVER` | `XML2JSON-PARSER` |
| `ToHeaderCase()` | `User-Id` | `User-Id` | `Http-Server` | `Xml2Json-Parser` |
| `ToTitleCase()` | `User Id` | `User Id` | `Http Server` | `Xml2Json Parser` |
| `ToTrainCase()` | `User-id` | `User-id` | `Http-server` | `Xml2json-parser` |
| `ToSentenceCase()` | `User id` | `User id` | `Http server` | `Xml2json parser` |
| `ToFlatCase()` | `userid` | `userid` | `httpserver` | `xml2jsonparser` |

With the default options:

- `ToPascalCase()` capitalizes the first character of each word, lowercases the rest, and joins the words.
- `ToCamelCase()` joins words in lowercase, capitalizing the first character of each word after the first.
- `ToSnakeCase()` and `ToKebabCase()` join lowercase words with underscores and hyphens, respectively.
- `ToScreamingSnakeCase()` and `ToCobolCase()` join uppercase words with underscores and hyphens, respectively.
- `ToHeaderCase()` and `ToTitleCase()` capitalize the first character of each word, lowercase the rest, and join words with hyphens and spaces, respectively.
- `ToTrainCase()` and `ToSentenceCase()` capitalize only the first character of the first word, lowercase the rest, and join words with hyphens and spaces, respectively.
- `ToFlatCase()` joins lowercase words without separators.

### Separator preservation

All conversion methods accept `SeparatorOptions` to specify where original separators are preserved.

- `None` is the default and normalizes separators for the casing style.
- `PreserveLeading`, `PreserveTrailing`, and `PreserveInner` preserve the type, count, and order of leading, trailing, and inner separators, respectively.
- Combine options with `|`. Positions are determined from the original string, and no output separators are added next to preserved separators.

```csharp
var value = "__user__name__";
var options = SeparatorOptions.PreserveLeading | SeparatorOptions.PreserveTrailing;

Console.WriteLine(value.ToSnakeCase(options));
```

Results for the same input with each option:

| Options | snake_case |
|---|---|
| `None` | `user_name` |
| `PreserveLeading` | `__user_name` |
| `PreserveTrailing` | `user_name__` |
| `PreserveInner` | `user__name` |
| `PreserveLeading \| PreserveTrailing` | `__user_name__` |
| `PreserveLeading \| PreserveTrailing \| PreserveInner` | `__user__name__` |

### Word boundaries

Words are separated by explicit separators and casing changes.

- Whitespace (including tabs and line breaks), punctuation, and symbols separate words. This includes underscores, hyphens, `.`, and `+`.
- By default, inner separators are normalized for the casing style, and leading and trailing separators are removed.
- An uppercase letter starts a new word after a lowercase letter or decimal digit.
- Acronyms are split before the uppercase letter that begins the next word.
- Formats with output separators skip the separator before a word immediately following a digit, unless an explicit separator is present.

For example, `XML2JsonParser` splits into `XML2`, `Json`, and `Parser`:

| Input | PascalCase | camelCase | snake_case |
|---|---|---|---|
| `XML2JsonParser` | `Xml2JsonParser` | `xml2JsonParser` | `xml2json_parser` |

### Casing and symbols

Casing conversions use invariant rules, independent of the current culture.

- Acronyms and brand names are normalized like ordinary words.
- Preserving separators still applies the selected casing style to the words.
- camelCase, Train-case, and Sentence case determine the first word across the entire input.

Results with `SeparatorOptions.PreserveInner`:

| Input | PascalCase | camelCase | snake_case |
|---|---|---|---|
| `User+Id` | `User+Id` | `user+Id` | `user+id` |

- Different inputs can produce the same result; original word boundaries cannot be recovered.
- Repeated PascalCase conversion can change the result.
- Results are not checked for identifier validity or name uniqueness.

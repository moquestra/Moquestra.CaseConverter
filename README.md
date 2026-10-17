# Moquestra.CaseConverter

A string extension library for PascalCase, camelCase, and snake_case conversion.

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
    Console.WriteLine($"{value.ToPascalCase()}, {value.ToCamelCase()}, {value.ToSnakeCase()}");
}
```

Output (PascalCase, camelCase, snake_case):

```
UserId, userId, user_id
User+Id, user+Id, user+id
HttpServer, httpServer, http_server
Xml2JsonParser, xml2JsonParser, xml2json_parser
```

- `ToPascalCase()` capitalizes the first character of each word, lowercases the rest, and joins the words.
- `ToCamelCase()` joins words in lowercase, capitalizing the first character of each word after the first.
- `ToSnakeCase()` joins lowercase words with underscores.

### Word boundaries

Words are separated by explicit separators and casing changes.

- Underscores, hyphens, and whitespace (including tabs and line breaks) separate words.
- Repeated separators are collapsed, and leading and trailing separators are removed.
- An uppercase letter starts a new word after a lowercase letter or decimal digit.
- Acronyms are split before the uppercase letter that begins the next word.
- snake_case skips the underscore before a word immediately following a digit, unless an explicit separator is present.

For example, `XML2JsonParser` splits into `XML2`, `Json`, and `Parser`:

| Input | PascalCase | camelCase | snake_case |
|---|---|---|---|
| `XML2JsonParser` | `Xml2JsonParser` | `xml2JsonParser` | `xml2json_parser` |

### Casing and symbols

Casing conversions use invariant rules, independent of the current culture.

- Acronyms and brand names are normalized like ordinary words.
- Symbols such as `.` and `+` are preserved, with no separators added next to them.
- camelCase determines the first word across the entire input.

| Input | PascalCase | camelCase | snake_case |
|---|---|---|---|
| `User+Id` | `User+Id` | `user+Id` | `user+id` |

- Different inputs can produce the same result; original word boundaries cannot be recovered.
- Repeated PascalCase conversion can change the result.
- Results are not checked for identifier validity or name uniqueness.

using System;
using System.Text;

namespace Moquestra.CaseConverter
{
    /// <summary>
    /// Converts string casing.
    /// </summary>
    public static class CaseConverterExtensions
    {
        private enum CharType
        {
            None,
            Word,
            Symbol,
        }

        /// <summary>
        /// Converts a string to PascalCase.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToPascalCase(this string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var result = new StringBuilder(value.Length);
            var index = 0;

            CharType charType;

            while ((charType = FindWord(value, index, out var wordStart, out var wordEnd)) != CharType.None)
            {
                if (charType == CharType.Symbol)
                {
                    result.Append(value[wordStart]);
                }
                else
                {
                    result.Append(char.ToUpperInvariant(value[wordStart]));

                    for (var characterIndex = wordStart + 1; characterIndex <= wordEnd; characterIndex++)
                        result.Append(char.ToLowerInvariant(value[characterIndex]));
                }

                index = wordEnd + 1;
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts a string to camelCase.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToCamelCase(this string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var result = new StringBuilder(value.Length);
            var index = 0;
            var firstWord = true;

            CharType charType;

            while ((charType = FindWord(value, index, out var wordStart, out var wordEnd)) != CharType.None)
            {
                if (charType == CharType.Symbol)
                {
                    result.Append(value[wordStart]);
                }
                else
                {
                    result.Append(firstWord
                        ? char.ToLowerInvariant(value[wordStart])
                        : char.ToUpperInvariant(value[wordStart]));

                    for (var characterIndex = wordStart + 1; characterIndex <= wordEnd; characterIndex++)
                        result.Append(char.ToLowerInvariant(value[characterIndex]));

                    firstWord = false;
                }

                index = wordEnd + 1;
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts a string to snake_case.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToSnakeCase(this string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var result = new StringBuilder(value.Length);
            var index = 0;
            var previousWasWord = false;

            CharType charType;

            while ((charType = FindWord(value, index, out var wordStart, out var wordEnd)) != CharType.None)
            {
                if (charType == CharType.Symbol)
                {
                    result.Append(value[wordStart]);
                    previousWasWord = false;
                }
                else
                {
                    if (previousWasWord)
                    {
                        var followsDigit = wordStart == index && char.IsDigit(value[wordStart - 1]);

                        if (!followsDigit)
                            result.Append('_');
                    }

                    for (var characterIndex = wordStart; characterIndex <= wordEnd; characterIndex++)
                        result.Append(char.ToLowerInvariant(value[characterIndex]));

                    previousWasWord = true;
                }

                index = wordEnd + 1;
            }

            return result.ToString();
        }

        private static CharType FindWord(string value, int start, out int wordStart, out int wordEnd)
        {
            var index = start;

            while (index < value.Length && IsWordSeparator(value[index]))
                index++;

            wordStart = index;
            wordEnd = index;

            if (index == value.Length)
                return CharType.None;

            if (!char.IsLetterOrDigit(value[index]))
                return CharType.Symbol;

            index++;

            while (index < value.Length && char.IsLetterOrDigit(value[index]))
            {
                if (char.IsUpper(value[index]))
                {
                    var previous = value[index - 1];

                    if (char.IsLower(previous) || char.IsDigit(previous))
                        break;

                    if (char.IsUpper(previous) && index + 1 < value.Length && char.IsLower(value[index + 1]))
                        break;
                }

                index++;
            }

            wordEnd = index - 1;

            return CharType.Word;
        }

        private static bool IsWordSeparator(char value)
        {
            return value == '_' || value == '-' || char.IsWhiteSpace(value);
        }
    }
}

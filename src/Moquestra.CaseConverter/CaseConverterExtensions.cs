using System;
using System.Globalization;
using System.Text;

namespace Moquestra.CaseConverter
{
    /// <summary>
    /// Converts string casing.
    /// </summary>
    public static class CaseConverterExtensions
    {
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
            var wordStart = true;
            var previousCategory = UnicodeCategory.OtherNotAssigned;

            for (var index = 0; index < value.Length; index++)
            {
                var character = value[index];
                var category = char.GetUnicodeCategory(character);

                if (IsLetterOrDigit(category))
                {
                    result.Append(wordStart || IsWordBoundary(value, index, previousCategory, category)
                        ? char.ToUpperInvariant(character)
                        : char.ToLowerInvariant(character));

                    wordStart = false;
                }
                else
                {
                    if (!IsWordSeparator(character))
                        result.Append(character);

                    wordStart = true;
                }

                previousCategory = category;
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
            var firstWord = true;
            var wordStart = true;
            var previousCategory = UnicodeCategory.OtherNotAssigned;

            for (var index = 0; index < value.Length; index++)
            {
                var character = value[index];
                var category = char.GetUnicodeCategory(character);

                if (IsLetterOrDigit(category))
                {
                    result.Append((wordStart || IsWordBoundary(value, index, previousCategory, category)) && !firstWord
                        ? char.ToUpperInvariant(character)
                        : char.ToLowerInvariant(character));

                    wordStart = false;
                    firstWord = false;
                }
                else
                {
                    if (!IsWordSeparator(character))
                        result.Append(character);

                    wordStart = true;
                }

                previousCategory = category;
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
            var previousWasWord = false;
            var wordStart = true;
            var previousCategory = UnicodeCategory.OtherNotAssigned;

            for (var index = 0; index < value.Length; index++)
            {
                var character = value[index];
                var category = char.GetUnicodeCategory(character);

                if (IsLetterOrDigit(category))
                {
                    if ((wordStart || (IsWordBoundary(value, index, previousCategory, category) && previousCategory != UnicodeCategory.DecimalDigitNumber)) &&
                        previousWasWord)
                    {
                        result.Append('_');
                    }

                    result.Append(char.ToLowerInvariant(character));
                    wordStart = false;
                    previousWasWord = true;
                }
                else
                {
                    if (!IsWordSeparator(character))
                    {
                        result.Append(character);
                        previousWasWord = false;
                    }

                    wordStart = true;
                }

                previousCategory = category;
            }

            return result.ToString();
        }

        private static bool IsLetterOrDigit(UnicodeCategory category)
        {
            switch (category)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                case UnicodeCategory.ModifierLetter:
                case UnicodeCategory.OtherLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsWordBoundary(string value, int index, UnicodeCategory previous, UnicodeCategory current)
        {
            if (current != UnicodeCategory.UppercaseLetter)
                return false;

            if (previous == UnicodeCategory.LowercaseLetter || previous == UnicodeCategory.DecimalDigitNumber)
                return true;

            return previous == UnicodeCategory.UppercaseLetter
                && index + 1 < value.Length
                && char.GetUnicodeCategory(value[index + 1]) == UnicodeCategory.LowercaseLetter;
        }

        private static bool IsWordSeparator(char value)
        {
            return value == '_' || value == '-' || char.IsWhiteSpace(value);
        }
    }
}

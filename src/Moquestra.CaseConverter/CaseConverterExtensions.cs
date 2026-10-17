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
            return ConvertCaseCore(value, WordCasing.InitialUpper);
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
            return ConvertCaseCore(value, WordCasing.FirstWordInitialLower);
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
            return ConvertCaseCore(value, WordCasing.Lower, '_');
        }

        /// <summary>
        /// Converts a string to kebab-case.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToKebabCase(this string value)
        {
            return ConvertCaseCore(value, WordCasing.Lower, '-');
        }

        /// <summary>
        /// Converts a string to SCREAMING_SNAKE_CASE.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToScreamingSnakeCase(this string value)
        {
            return ConvertCaseCore(value, WordCasing.Upper, '_');
        }

        /// <summary>
        /// Converts a string to COBOL-CASE.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToCobolCase(this string value)
        {
            return ConvertCaseCore(value, WordCasing.Upper, '-');
        }

        /// <summary>
        /// Converts a string to Header-Case.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToHeaderCase(this string value)
        {
            return ConvertCaseCore(value, WordCasing.InitialUpper, '-');
        }

        /// <summary>
        /// Converts a string to Title Case.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToTitleCase(this string value)
        {
            return ConvertCaseCore(value, WordCasing.InitialUpper, ' ');
        }

        /// <summary>
        /// Converts a string to Train-case.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToTrainCase(this string value)
        {
            return ConvertCaseCore(value, WordCasing.FirstWordInitialUpper, '-');
        }

        /// <summary>
        /// Converts a string to Sentence case.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToSentenceCase(this string value)
        {
            return ConvertCaseCore(value, WordCasing.FirstWordInitialUpper, ' ');
        }

        /// <summary>
        /// Converts a string to flatcase.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>
        /// The converted string, or an empty string if the input is empty or contains only whitespace, underscores, or hyphens.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public static string ToFlatCase(this string value)
        {
            return ConvertCaseCore(value, WordCasing.Lower);
        }

        private static string ConvertCaseCore(string value, WordCasing casing, char? separator = null)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var needsWordBoundary = separator.HasValue ||
                casing == WordCasing.InitialUpper ||
                casing == WordCasing.FirstWordInitialLower;

            var result = new StringBuilder(value.Length);
            var previousWasWord = false;
            var firstWord = true;
            var wordStart = true;
            var previousCategory = UnicodeCategory.OtherNotAssigned;

            for (var index = 0; index < value.Length; index++)
            {
                var character = value[index];
                var category = char.GetUnicodeCategory(character);

                if (IsLetterOrDigit(category))
                {
                    var isWordStart = needsWordBoundary && (wordStart || IsWordBoundary(value, index, previousCategory, category));

                    if (separator.HasValue && isWordStart && previousCategory != UnicodeCategory.DecimalDigitNumber && previousWasWord)
                        result.Append(separator.Value);

                    var upperCase = casing == WordCasing.Upper ||
                        (casing == WordCasing.InitialUpper && isWordStart) ||
                        (casing == WordCasing.FirstWordInitialLower && isWordStart && !firstWord) ||
                        (casing == WordCasing.FirstWordInitialUpper && firstWord);

                    result.Append(upperCase
                        ? ToUpperInvariant(character, category)
                        : ToLowerInvariant(character, category));

                    wordStart = false;
                    previousWasWord = true;
                    firstWord = false;
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

        private static char ToUpperInvariant(char value, UnicodeCategory category)
        {
            return category == UnicodeCategory.UppercaseLetter || category == UnicodeCategory.DecimalDigitNumber
                ? value
                : char.ToUpperInvariant(value);
        }

        private static char ToLowerInvariant(char value, UnicodeCategory category)
        {
            return category == UnicodeCategory.LowercaseLetter || category == UnicodeCategory.DecimalDigitNumber
                ? value
                : char.ToLowerInvariant(value);
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

        private enum WordCasing
        {
            Lower,
            Upper,
            InitialUpper,
            FirstWordInitialLower,
            FirstWordInitialUpper,
        }
    }
}

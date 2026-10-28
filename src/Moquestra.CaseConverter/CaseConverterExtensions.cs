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
        /// Converts a string to PascalCase with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToPascalCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.InitialUpper, options);
        }

        /// <summary>
        /// Converts a string to camelCase with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToCamelCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.FirstWordInitialLower, options);
        }

        /// <summary>
        /// Converts a string to snake_case with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToSnakeCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.Lower, options, '_');
        }

        /// <summary>
        /// Converts a string to kebab-case with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToKebabCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.Lower, options, '-');
        }

        /// <summary>
        /// Converts a string to SCREAMING_SNAKE_CASE with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToScreamingSnakeCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.Upper, options, '_');
        }

        /// <summary>
        /// Converts a string to COBOL-CASE with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToCobolCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.Upper, options, '-');
        }

        /// <summary>
        /// Converts a string to Header-Case with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToHeaderCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.InitialUpper, options, '-');
        }

        /// <summary>
        /// Converts a string to Title Case with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToTitleCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.InitialUpper, options, ' ');
        }

        /// <summary>
        /// Converts a string to Train-case with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToTrainCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.FirstWordInitialUpper, options, '-');
        }

        /// <summary>
        /// Converts a string to Sentence case with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToSentenceCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.FirstWordInitialUpper, options, ' ');
        }

        /// <summary>
        /// Converts a string to flatcase with optional separator preservation.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="options">The positions where original separators are preserved. None normalizes separators for the casing style.</param>
        /// <returns>
        /// The converted string, or an empty string for empty input or input containing only unpreserved separators.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="options"/> contains undefined bits.</exception>
        public static string ToFlatCase(this string value, SeparatorOptions options = SeparatorOptions.None)
        {
            return ConvertCaseCore(value, WordCasing.Lower, options);
        }

        private static string ConvertCaseCore(string value, WordCasing casing, SeparatorOptions options, char? separator = null)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            if ((options & ~(SeparatorOptions.PreserveLeading | SeparatorOptions.PreserveTrailing | SeparatorOptions.PreserveInner)) != SeparatorOptions.None)
                throw new ArgumentOutOfRangeException(nameof(options));

            var preserveLeading = (options & SeparatorOptions.PreserveLeading) != SeparatorOptions.None;
            var preserveTrailing = (options & SeparatorOptions.PreserveTrailing) != SeparatorOptions.None;
            var preserveInner = (options & SeparatorOptions.PreserveInner) != SeparatorOptions.None;
            var start = 0;
            var end = value.Length;

            while (start < end && IsWordSeparator(value[start], char.GetUnicodeCategory(value[start])))
                start++;

            while (end > start && IsWordSeparator(value[end - 1], char.GetUnicodeCategory(value[end - 1])))
                end--;

            if (start == value.Length)
                return preserveLeading || preserveTrailing ? value : string.Empty;

            var needsWordBoundary = separator.HasValue ||
                casing == WordCasing.InitialUpper ||
                casing == WordCasing.FirstWordInitialLower;

            var result = new StringBuilder(value.Length);
            if (preserveLeading)
                result.Append(value, 0, start);

            var previousWasWord = false;
            var firstWord = true;
            var wordStart = true;
            var previousCategory = UnicodeCategory.OtherNotAssigned;

            for (var index = start; index < end; index++)
            {
                var character = value[index];
                var category = char.GetUnicodeCategory(character);

                if (IsLetterOrDigit(category))
                {
                    var isWordStart = needsWordBoundary && (wordStart || IsWordBoundary(value, index, end, previousCategory, category));

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
                    if (!IsWordSeparator(character, category) || preserveInner)
                    {
                        result.Append(character);
                        previousWasWord = false;
                    }

                    wordStart = true;
                }

                previousCategory = category;
            }

            if (preserveTrailing)
                result.Append(value, end, value.Length - end);

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

        private static bool IsWordBoundary(string value, int index, int end, UnicodeCategory previous, UnicodeCategory current)
        {
            if (current != UnicodeCategory.UppercaseLetter)
                return false;

            if (previous == UnicodeCategory.LowercaseLetter || previous == UnicodeCategory.DecimalDigitNumber)
                return true;

            return previous == UnicodeCategory.UppercaseLetter
                && index + 1 < end
                && char.GetUnicodeCategory(value[index + 1]) == UnicodeCategory.LowercaseLetter;
        }

        private static bool IsWordSeparator(char value, UnicodeCategory category)
        {
            switch (category)
            {
                case UnicodeCategory.ConnectorPunctuation:
                case UnicodeCategory.DashPunctuation:
                case UnicodeCategory.OpenPunctuation:
                case UnicodeCategory.ClosePunctuation:
                case UnicodeCategory.InitialQuotePunctuation:
                case UnicodeCategory.FinalQuotePunctuation:
                case UnicodeCategory.OtherPunctuation:
                case UnicodeCategory.MathSymbol:
                case UnicodeCategory.CurrencySymbol:
                case UnicodeCategory.ModifierSymbol:
                case UnicodeCategory.OtherSymbol:
                    return true;
                default:
                    return char.IsWhiteSpace(value);
            }
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

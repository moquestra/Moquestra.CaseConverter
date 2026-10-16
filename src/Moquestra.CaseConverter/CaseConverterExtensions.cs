using System;
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

            for (var index = 0; index < value.Length; index++)
            {
                var character = value[index];

                if (char.IsLetterOrDigit(character))
                {
                    result.Append(wordStart || IsWordBoundary(value, index)
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

            for (var index = 0; index < value.Length; index++)
            {
                var character = value[index];

                if (char.IsLetterOrDigit(character))
                {
                    result.Append((wordStart || IsWordBoundary(value, index)) && !firstWord
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

            for (var index = 0; index < value.Length; index++)
            {
                var character = value[index];

                if (char.IsLetterOrDigit(character))
                {
                    if (previousWasWord &&
                        (wordStart || IsWordBoundary(value, index) && !char.IsDigit(value[index - 1])))
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
            }

            return result.ToString();
        }

        private static bool IsWordBoundary(string value, int index)
        {
            if (!char.IsUpper(value[index]))
                return false;

            var previous = value[index - 1];

            if (char.IsLower(previous) || char.IsDigit(previous))
                return true;

            return char.IsUpper(previous) && index + 1 < value.Length && char.IsLower(value[index + 1]);
        }

        private static bool IsWordSeparator(char value)
        {
            return value == '_' || value == '-' || char.IsWhiteSpace(value);
        }
    }
}

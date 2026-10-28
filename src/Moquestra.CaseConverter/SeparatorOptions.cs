using System;

namespace Moquestra.CaseConverter
{
    /// <summary>
    /// Specifies where original separators are preserved.
    /// </summary>
    [Flags]
    public enum SeparatorOptions
    {
        /// <summary>
        /// Normalizes separators for the casing style.
        /// </summary>
        None = 0,

        /// <summary>
        /// Preserves leading separators unchanged.
        /// </summary>
        PreserveLeading = 1,

        /// <summary>
        /// Preserves trailing separators unchanged.
        /// </summary>
        PreserveTrailing = 2,

        /// <summary>
        /// Preserves inner separators unchanged.
        /// </summary>
        PreserveInner = 4,
    }
}

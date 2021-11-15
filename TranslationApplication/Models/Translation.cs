using System;
using System.Collections.Generic;
using System.Text;

namespace TranslationApplication.Models
{
    /// <summary>
    /// Represents a single translated text
    /// </summary>
    public class Translation
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Language of the translation
        /// </summary>
        public string LanguageKey { get; set; }

        /// <summary>
        /// Label key that is translated
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The translated text
        /// </summary>
        public string TranslationText { get; set; }
    }
}

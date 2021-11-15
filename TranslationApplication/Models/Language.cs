using System;
using System.Collections.Generic;
using System.Text;

namespace TranslationApplication.Models
{
    /// <summary>
    /// Language used for translations
    /// </summary>
    public class Language
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the language
        /// </summary>
        public string LanguageName { get; set; }

        /// <summary>
        /// Key of the language
        /// </summary>
        public string LanguageKey { get; set; }
    }
}

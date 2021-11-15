using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslationApplication.Models;

namespace TranslationApplication.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TranslationsContext context)
        {
            context.Database.EnsureCreated();

            if(!context.Languages.Any())
            {
                var languages = new Language[]
                {
                    new Language{LanguageKey = "fi", LanguageName = "Suomi"},
                    new Language{LanguageKey = "en", LanguageName = "English"}
                };

                foreach (Language l in languages)
                    context.Languages.Add(l);

                context.SaveChanges();
            }

            if(!context.Translations.Any())
            {
                var translations = new Translation[]
                {
                    new Translation() {LanguageKey = "fi", Key = "test", TranslationText = "Testi"},
                    new Translation() {LanguageKey = "en", Key = "test", TranslationText = "Test"}
                };

                foreach (Translation t in translations)
                    context.Translations.Add(t);

                context.SaveChanges();
            }
        }
    }
}

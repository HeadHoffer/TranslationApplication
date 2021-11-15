using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TranslationApplication.Data;
using TranslationApplication.Models;
using TranslationApplication.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace TranslationApplication.Testing.UnitTests
{
    public class LanguageTests
    {
        [Fact]
        public void ShouldGetAllLanguages()
        {
            var options = new DbContextOptionsBuilder<TranslationsContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ShouldGetAllLanguages)}DB")
                .Options;

            TestDBHelper.EmptyTestDatabase(options);

            //Insert mock data to db
            using (var context = new TranslationsContext(options))
            {
                context.Languages.Add(new Language() { LanguageKey = "fi", LanguageName = "Suomi" });
                context.Languages.Add(new Language() { LanguageKey = "se", LanguageName = "Svenska" });
                context.Languages.Add(new Language() { LanguageKey = "en", LanguageName = "English" });
                context.SaveChanges();
            }

            //Use a clean instance of the DB context
            using (var context = new TranslationsContext(options))
            {
                LanguagesController controller = new LanguagesController(context);

                var result = controller.Get().Result;
                List<Language> languages = result.Value.ToList();

                Assert.Equal(3, languages.Count);
            }
        }

        [Fact]
        public void ShouldGetLanguageByKey()
        {
            var options = new DbContextOptionsBuilder<TranslationsContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ShouldGetLanguageByKey)}DB")
                .Options;

            TestDBHelper.EmptyTestDatabase(options);

            //Insert mock data to db
            using (var context = new TranslationsContext(options))
            {
                context.Languages.Add(new Language() { LanguageKey = "fi", LanguageName = "Suomi" });
                context.Languages.Add(new Language() { LanguageKey = "se", LanguageName = "Svenska" });
                context.Languages.Add(new Language() { LanguageKey = "en", LanguageName = "English" });
                context.SaveChanges();
            }

            //Use a clean instance of the DB context
            using (var context = new TranslationsContext(options))
            {
                LanguagesController controller = new LanguagesController(context);

                var result = controller.GetLanguage("fi").Result;
                var language = result.Value;

                Assert.Equal("Suomi", language.LanguageName);
            }
        }

        [Fact]
        public void ShouldPostLanguage()
        {
            var options = new DbContextOptionsBuilder<TranslationsContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ShouldPostLanguage)}DB")
                .Options;

            TestDBHelper.EmptyTestDatabase(options);

            //Insert mock data to db
            using (var context = new TranslationsContext(options))
            {
                context.Languages.Add(new Language() { LanguageKey = "fi", LanguageName = "Suomi" });
                context.Languages.Add(new Language() { LanguageKey = "se", LanguageName = "Svenska" });
                //context.Languages.Add(new Language() { LanguageKey = "en", LanguageName = "English" });
                context.SaveChanges();
            }

            //Use a clean instance of the DB context
            using (var context = new TranslationsContext(options))
            {
                LanguagesController controller = new LanguagesController(context);

                CreatedAtActionResult result = (CreatedAtActionResult)controller.Post("en", "English").Result.Result;
                Language language = (Language)result.Value;

                Assert.Equal(201, result.StatusCode);
                Assert.NotNull(language);
                Assert.Equal("English", language.LanguageName);
                Assert.Equal(3, context.Languages.Count());
            }
        }

        [Fact]
        public void ShouldDeleteLanguage()
        {
            var options = new DbContextOptionsBuilder<TranslationsContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ShouldDeleteLanguage)}DB")
                .Options;

            TestDBHelper.EmptyTestDatabase(options);

            //Insert mock data to db
            using (var context = new TranslationsContext(options))
            {
                context.Languages.Add(new Language() { LanguageKey = "fi", LanguageName = "Suomi" });
                context.Languages.Add(new Language() { LanguageKey = "se", LanguageName = "Svenska" });
                context.Languages.Add(new Language() { LanguageKey = "en", LanguageName = "English" });
                context.SaveChanges();
            }

            //Use a clean instance of the DB context
            using (var context = new TranslationsContext(options))
            {
                LanguagesController controller = new LanguagesController(context);

                var result = controller.Delete("fi").Result;
                var language = result.Value;

                Assert.Equal("Suomi", language.LanguageName);
                Assert.Equal(2, context.Languages.Count());
            }
        }
    }
}

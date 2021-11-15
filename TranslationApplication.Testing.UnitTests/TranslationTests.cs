using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TranslationApplication.Data;
using TranslationApplication.Models;
using TranslationApplication.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace TranslationApplication.Testing.UnitTests
{
    public class TranslationTests
    {
        [Fact]
        public void ShouldGetAllTranslations()
        {
            var options = new DbContextOptionsBuilder<TranslationsContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ShouldGetAllTranslations)}DB")
                .Options;

            TestDBHelper.EmptyTestDatabase(options);

            //Insert mock data to db
            using (var context = new TranslationsContext(options))
            {
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "fi", TranslationText = "Testi" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "en", TranslationText = "Test" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "chair", LanguageKey = "fi", TranslationText = "Tuoli" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "chair", LanguageKey = "en", TranslationText = "Chair" });
                context.SaveChanges();
            }

            //Use a clean instance of the DB Context
            using (var context = new TranslationsContext(options))
            {
                TranslationController controller = new TranslationController(context);

                var result = controller.GetTranslations().Result;
                List<Translation> translations = result.Value.ToList();

                Assert.Equal(4, translations.Count);
            }
        }

        [Fact]
        public void ShouldGetAllTranslationsByLanguage()
        {
            var options = new DbContextOptionsBuilder<TranslationsContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ShouldGetAllTranslationsByLanguage)}DB")
                .Options;

            TestDBHelper.EmptyTestDatabase(options);

            //Insert mock data to db
            using (var context = new TranslationsContext(options))
            {
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "fi", TranslationText = "Testi" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "en", TranslationText = "Test" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "chair", LanguageKey = "fi", TranslationText = "Tuoli" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "chair", LanguageKey = "en", TranslationText = "Chair" });
                context.SaveChanges();
            }

            //Use a clean instance of the DB Context
            using (var context = new TranslationsContext(options))
            {
                TranslationController controller = new TranslationController(context);

                var result = controller.GetTranslationsByLanguage("fi").Result;
                List<Translation> translations = result.Value.ToList();

                Assert.Equal(2, translations.Count);
            }
        }

        [Fact]
        public void ShouldGetTranslation()
        {
            var options = new DbContextOptionsBuilder<TranslationsContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ShouldGetTranslation)}DB")
                .Options;

            TestDBHelper.EmptyTestDatabase(options);

            //Insert mock data to db
            using (var context = new TranslationsContext(options))
            {
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "fi", TranslationText = "Testi" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "en", TranslationText = "Test" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "chair", LanguageKey = "fi", TranslationText = "Tuoli" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "chair", LanguageKey = "en", TranslationText = "Chair" });
                context.SaveChanges();
            }

            //Use a clean instance of the DB Context
            using (var context = new TranslationsContext(options))
            {
                TranslationController controller = new TranslationController(context);

                var result = controller.Get("fi", "chair").Result;
                var translation = result.Value;

                Assert.Equal("fi", translation.LanguageKey);
                Assert.Equal("Tuoli", translation.TranslationText);
            }
        }

        [Fact]
        public void ShouldPostTranslation()
        {
            var options = new DbContextOptionsBuilder<TranslationsContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ShouldPostTranslation)}DB")
                .Options;

            TestDBHelper.EmptyTestDatabase(options);

            //Insert mock data to db
            using (var context = new TranslationsContext(options))
            {
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "fi", TranslationText = "Testi" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "en", TranslationText = "Test" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "chair", LanguageKey = "en", TranslationText = "Chair" });
                context.SaveChanges();
            }

            //Use a clean instance of the DB Context
            using (var context = new TranslationsContext(options))
            {
                TranslationController controller = new TranslationController(context);

                CreatedAtActionResult result = (CreatedAtActionResult)controller.Post("fi", "chair", "Tuoli").Result.Result;
                Translation translation = (Translation)result.Value;

                Assert.Equal(201, result.StatusCode);
                Assert.NotNull(translation);
                Assert.Equal("fi", translation.LanguageKey);
                Assert.Equal("Tuoli", translation.TranslationText);
                Assert.Equal(4, context.Translations.Count());
            }
        }

        [Fact]
        public void ShouldDeleteTranslation()
        {
            var options = new DbContextOptionsBuilder<TranslationsContext>()
                .UseInMemoryDatabase(databaseName: $"{nameof(ShouldDeleteTranslation)}DB")
                .Options;

            TestDBHelper.EmptyTestDatabase(options);

            //Insert mock data to db
            using (var context = new TranslationsContext(options))
            {
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "fi", TranslationText = "Testi" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "test", LanguageKey = "en", TranslationText = "Test" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "chair", LanguageKey = "fi", TranslationText = "Tuoli" });
                context.Translations.Add(new Translation() { Id = Guid.NewGuid(), Key = "chair", LanguageKey = "en", TranslationText = "Chair" });
                context.SaveChanges();
            }

            //Use a clean instance of the DB Context
            using (var context = new TranslationsContext(options))
            {
                TranslationController controller = new TranslationController(context);

                var result = controller.Delete("fi", "chair").Result;
                var translation = result.Value;

                Assert.Equal("fi", translation.LanguageKey);
                Assert.Equal("Tuoli", translation.TranslationText);
                Assert.Equal(3, context.Translations.Count());
            }
        }
    }
}

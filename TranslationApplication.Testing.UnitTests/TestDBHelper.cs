using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TranslationApplication.Data;

namespace TranslationApplication.Testing.UnitTests
{
    public static class TestDBHelper
    {
        /// <summary>
        /// Empties the test database from all mock data
        /// </summary>
        /// <param name="options"></param>
        public static void EmptyTestDatabase(DbContextOptions<TranslationsContext> options)
        {
            using (var context = new TranslationsContext(options))
            {
                if(context.Translations.Any())
                {
                    foreach (var entry in context.Translations)
                        context.Translations.Remove(entry);

                    context.SaveChanges();
                }

                if(context.Languages.Any())
                {
                    foreach (var entry in context.Languages)
                        context.Languages.Remove(entry);

                    context.SaveChanges();
                }
            }
        }
    }
}

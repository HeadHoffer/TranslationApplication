using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TranslationApplication.Models;

namespace TranslationApplication.Data
{
    public class TranslationsContext : DbContext
    {
        public TranslationsContext(DbContextOptions<TranslationsContext> options) : base(options) { }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Translation> Translations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().ToTable("Language");
            modelBuilder.Entity<Translation>().ToTable("Translation");
        }
    }
}

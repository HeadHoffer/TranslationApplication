
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranslationApplication.Data;
using TranslationApplication.Models;

//Use localhost:nnnn/swagger
namespace TranslationApplication.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly TranslationsContext _context;

        public LanguagesController(TranslationsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all languages from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            return await _context.Languages.ToListAsync();
        }

        /// <summary>
        /// Finds a language from database based on a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("{key}", Name = "GetLanguage")]
        public async Task<ActionResult<Language>> Get(string key)
        {
            var lang = await _context.Languages.FirstOrDefaultAsync(x => x.LanguageKey == key);

            if (lang == null)
            {
                return NotFound();
            }

            return lang;
        }

        /// <summary>
        /// Adds a new language to the database
        /// </summary>
        /// <param name="key">Language key</param>
        /// <param name="name">Language name</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{key}/{name}")]
        public async Task<ActionResult<Language>> Post(string key, string name)
        {
            var lang = await _context.Languages.FirstOrDefaultAsync(x => x.LanguageKey == key);
            if (lang != null)
            {
                return BadRequest($"Language with key '{key}' already exists");
            }

            var language = new Language() { LanguageKey = key, LanguageName = name };

            _context.Languages.Add(language);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanguage", new { key = language.LanguageKey }, language);
        }

        /// <summary>
        /// Deletes a language from the database
        /// </summary>
        /// <param name="key">Language key</param>
        /// <returns></returns>
        [HttpDelete("{key}")]
        public async Task<ActionResult<Language>> Delete(string key)
        {
            var language = await _context.Languages.FirstOrDefaultAsync(x => x.LanguageKey == key);
            if (language == null)
            {
                return NotFound();
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

            return language;
        }
    }
}
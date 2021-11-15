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
    [Route("api/v1/translations")]
    [ApiController]
    public class TranslationController : ControllerBase
    {
        private readonly TranslationsContext _context;

        public TranslationController(TranslationsContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a single translation from the database
        /// </summary>
        /// <param name="languageKey"></param>
        /// <param name="labelKey"></param>
        /// <returns></returns>
        [HttpGet("{languageKey}/{labelKey}", Name = "Get")]
        public async Task<ActionResult<Translation>> Get(string languageKey, string labelKey)
        {
            var translation = await _context.Translations.FirstOrDefaultAsync(x => x.Key == labelKey && x.LanguageKey == languageKey);

            if (translation == null)
                return NotFound();

            return translation;
        }

        /// <summary>
        /// Gets all translations by language
        /// </summary>
        /// <param name="language">Language key</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getByLanguage/{language}")]
        public async Task<ActionResult<IEnumerable<Translation>>> GetTranslationsByLanguage(string language)
        {
            return await _context.Translations.Where(x => x.LanguageKey == language).ToListAsync();
        }

        /// <summary>
        /// Gets all translations from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<IEnumerable<Translation>>> Get()
        {
            return await _context.Translations.ToListAsync();
        }

        /// <summary>
        /// Adds a new translation to the database
        /// </summary>
        /// <param name="languageKey">Language of the translation</param>
        /// <param name="labelKey">Key for what is being translated</param>
        /// <param name="text">Translation text</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{languageKey}/{labelKey}")]
        public async Task<ActionResult<Translation>> Post(string languageKey, string labelKey, [FromBody] string text)
        {
            var tr = await _context.Translations.FirstOrDefaultAsync(x => x.LanguageKey == languageKey && x.Key == labelKey);
            if (tr != null)
                return BadRequest($"Translation with key '{labelKey}' already exists for language '{languageKey}'");

            var translation = new Translation() { Key = labelKey, LanguageKey = languageKey, TranslationText = text };

            _context.Translations.Add(translation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { languageKey = translation.LanguageKey, labelKey = translation.Key }, translation);
        }

        [HttpDelete]
        [Route("{languageKey}/{labelKey}")]
        public async Task<ActionResult<Translation>> Delete(string languageKey, string labelKey)
        {
            var tr = await _context.Translations.FirstOrDefaultAsync(x => x.LanguageKey == languageKey && x.Key == labelKey);
            if (tr == null)
                return NotFound();

            _context.Translations.Remove(tr);
            await _context.SaveChangesAsync();

            return tr;
        }
    }
}

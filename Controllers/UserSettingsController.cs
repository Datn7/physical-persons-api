using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using physical_persons_api;
using physical_persons_api.Entities;

namespace physical_persons_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserSettings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSettings>>> GetUserSettings()
        {
            return await _context.UserSettings.ToListAsync();
        }

        // GET: api/UserSettings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSettings>> GetUserSettings(int id)
        {
            var userSettings = await _context.UserSettings.FindAsync(id);

            if (userSettings == null)
            {
                return NotFound();
            }

            return userSettings;
        }

        // PUT: api/UserSettings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSettings(int id, UserSettings userSettings)
        {
            if (id != userSettings.Id)
            {
                return BadRequest();
            }

            _context.Entry(userSettings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSettingsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserSettings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserSettings>> PostUserSettings(UserSettings userSettings)
        {
            _context.UserSettings.Add(userSettings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserSettings", new { id = userSettings.Id }, userSettings);
        }

        // DELETE: api/UserSettings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSettings(int id)
        {
            var userSettings = await _context.UserSettings.FindAsync(id);
            if (userSettings == null)
            {
                return NotFound();
            }

            _context.UserSettings.Remove(userSettings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSettingsExists(int id)
        {
            return _context.UserSettings.Any(e => e.Id == id);
        }
    }
}

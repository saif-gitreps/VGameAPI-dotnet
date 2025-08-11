using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VGameAPI.Database;

namespace VGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(VideoGameDbContext context) : ControllerBase
    {
        private readonly VideoGameDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
        {
            return Ok(await _context.VideoGames.ToListAsync());
        }
        [HttpGet("{id}")]
        public ActionResult<VideoGame> GetVideoGameById(int id)
        {
            foreach (VideoGame game in videoGames)
            {
                if (game.ID == id)
                {
                    return Ok(game);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<VideoGame> AddVideoGame(VideoGame videoGame)
        {
            if (videoGame == null) 
            {
                return BadRequest("Video game cannot be null.");
            }

            videoGame.ID = videoGames.Count > 0 ? videoGames.Max(vg => vg.ID) + 1 : 1;

            return CreatedAtAction(nameof(GetVideoGameById), new { id = videoGame.ID }, videoGame);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVideoGame(int id, VideoGame videoGame)
        {
            VideoGame? existingGame = videoGames.FirstOrDefault((vg) => vg.ID == id);
            if (existingGame == null)
            {
                return NotFound();
            }

            existingGame.Title = videoGame.Title;
            existingGame.Platform = videoGame.Platform;
            existingGame.Developer = videoGame.Developer;
            existingGame.Publisher = videoGame.Publisher;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideoGame(int id)
        {
            VideoGame? existingGame = videoGames.FirstOrDefault((vg) => vg.ID == id);
            if (existingGame == null)
            {
                return NotFound();
            }
            videoGames.Remove(existingGame);
            return NoContent();
        }

    }

}

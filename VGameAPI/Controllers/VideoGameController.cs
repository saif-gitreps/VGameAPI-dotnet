using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace VGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>
                {
                    new VideoGame { ID = 1, Title = "The Legend of Zelda: Breach of the Wild",
                        Platform = "Nintendo Switch", Developer = "Nintendo EPD", Publisher = "Nintendo" },
                    new VideoGame { ID = 2, Title = "God of War", Platform = "PlayStation 4",
                        Developer = "Santa Monica Studio", Publisher = "Sony Interactive Entertainment" },
                    new VideoGame { ID = 3, Title = "Minecraft", Platform = "Multi-platform",
                        Developer = "Mojang Studios", Publisher = "Mojang Studios" }
                };

        [HttpGet]
        public ActionResult<List<VideoGame>> GetVideoGames()
        {
            return Ok(videoGames);
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

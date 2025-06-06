using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using VideoGameApi.model;
using VideoGameApi.Services;

namespace VideoGameApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VideoGameController : ControllerBase
{
    private VideoGameService _gameService;

    public VideoGameController(VideoGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VideoGame>> GetVideoGames()
    {
        IEnumerable<VideoGame> videoGames = _gameService.GetAllGames();

        if (!videoGames.Any()) return NotFound();

        return Ok(videoGames);
    }

    [HttpGet("{id}")]
    public ActionResult<VideoGame> GetVideoGameById(ObjectId id)
    {
        VideoGame? videoGame = _gameService.GetGameById(id);

        if (videoGame == null) return NotFound();

        return Ok(videoGame);
    }

    [HttpPost]
    public ActionResult<VideoGame> AddVideoGame(VideoGame? newGame)
    {
        if (newGame == null) return BadRequest();

        VideoGame addedGame = _gameService.AddGame(newGame);

        return CreatedAtAction(nameof(GetVideoGameById), new { Id = addedGame.Id }, addedGame);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateVideoGame(ObjectId id, VideoGame? updatedGame)
    {
        if (updatedGame == null) return BadRequest();

        bool isUpdated = _gameService.UpdateGame(id, updatedGame);

        if (isUpdated) return BadRequest();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteVideoGame(ObjectId id)
    {
        _gameService.DeleteGame(id);

        return NoContent();
    }
}
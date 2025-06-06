using MongoDB.Bson;
using VideoGameApi.model;

namespace VideoGameApi.Services;

public class VideoGameService(VideoGameDbContext context)
{
    public IEnumerable<VideoGame> GetAllGames()
    {
        return context.Games;
    }

    public VideoGame? GetGameById(ObjectId id)
    {
        return FindGameById(id);
    }

    public VideoGame AddGame(VideoGame game)
    {
        context.Games.Add(game);
        context.ChangeTracker.DetectChanges();
        Console.WriteLine(context.ChangeTracker.DebugView.LongView);

        context.SaveChanges();

        return game;
    }

    public bool UpdateGame(ObjectId id, VideoGame updatedGame)
    {
        VideoGame? gameToUpdate = FindGameById(id);

        if (gameToUpdate != null)
        {
            gameToUpdate.Title = updatedGame.Title;
            gameToUpdate.Developer = updatedGame.Developer;
            gameToUpdate.Platform = updatedGame.Platform;
            gameToUpdate.Publisher = updatedGame.Publisher;

            context.Games.Update(gameToUpdate);
            context.ChangeTracker.DetectChanges();

            Console.WriteLine(context.ChangeTracker.DebugView.LongView);

            context.SaveChanges();

            return true;
        }

        return false;
    }

    public void DeleteGame(ObjectId id)
    {
        var gameToDelete = FindGameById(id);

        if (gameToDelete != null)
        {
            context.Games.Remove(gameToDelete);
            context.ChangeTracker.DetectChanges();

            Console.WriteLine(context.ChangeTracker.DebugView.LongView);

            context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Game cannot be found!");
        }
    }


    private VideoGame? FindGameById(ObjectId id)
    {
        return context.Games.FirstOrDefault(game => game.Id == id);
    }
}
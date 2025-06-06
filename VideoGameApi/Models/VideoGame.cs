using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace VideoGameApi.model;

[Collection("games")]
public class VideoGame
{
    public ObjectId Id { get; set; }
    public string? Title { get; set; }
    public string? Platform { get; set; }
    public string? Developer { get; set; }
    public string? Publisher { get; set; }
}
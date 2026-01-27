using System;

namespace GameStores.Api.Models;

using System.Collections.Generic;

public class Game
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description {get;set;}
    public decimal Price {get; set;}
    public DateOnly ReleaseDate {get; set;}

    public Developer? Developer {get; set;}
    public int DeveloperId {get; set;}

    public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
}

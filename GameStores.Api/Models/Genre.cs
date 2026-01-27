using System;

namespace GameStores.Api.Models;

using System.Collections.Generic;

public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }

    // Many-to-Many
    public ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
}

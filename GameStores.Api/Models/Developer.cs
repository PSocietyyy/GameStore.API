using System;

namespace GameStores.Api.Models;

public class Developer
{
    public int Id { get; set; }
    public required string StudioName {get; set;}
    public string Description {get; set;} = string.Empty;
    public string Website {get; set;} = string.Empty;
    public Account? Account {get; set;} = null;
    public int AccountId {get; set;}

    public ICollection<Game> Games {get; set;} = new List<Game>();
}

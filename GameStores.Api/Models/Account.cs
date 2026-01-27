using System;

namespace GameStores.Api.Models;

public class Account
{
    public int Id { get; set; }
    public required string Name {get; set;}
    public required string Email {get; set;}
    public string Password {get; set;} = string.Empty;
    public Role Role {get; set;}
    public Developer? DeveloperProfile {get; set;}
}

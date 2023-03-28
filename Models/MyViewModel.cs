#pragma warning disable CS8618

namespace darts.Models;
public class MyViewModel

{
    public Team UsersTeam {get; set;}
    public List<Player> AvailablePlayers {get; set;} = new List<Player>(); 
    public List<Player> TeamPlayers {get; set;} = new List<Player>();
}
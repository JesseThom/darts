#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace darts.Models;

public class Team
{
    [Key]
    public int TeamId { get; set; }

    [Required(ErrorMessage="Team Name is Required!")]
    public string TeamName { get; set; } 
    
    public int TeamPoints {get; set; }
    public int TeamUpdate {get; set; }

    public int UserId {get; set; }
    public User? TeamOwner {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Player> TeamPlayers {get; set;} = new List<Player>();
}
#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
//next line is used for [NotMapped]
using System.ComponentModel.DataAnnotations.Schema;
namespace darts.Models;
public class Player

{
    [Key]
    public int PlayerId { get; set; }

    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public int Hat { get; set; } 
    public int HTon { get; set; } 
    public int LTon { get; set; } 
    public int Whrse { get; set; } 
    public int _9MR { get; set; } 
    public int _8MR { get; set; } 
    public int _7MR { get; set; } 
    public int _6MR { get; set; } 
    public int _5MR { get; set; } 
    public int PlayerPoints { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //use for many side on one to many
    public int? TeamId { get;set; }
    public Team? PlayerTeam {get;set;}
}
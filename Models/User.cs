#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace darts.Models;
public class User

{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage="Name is Required!")]
    [MinLength(2,ErrorMessage="Name must be at least 2 characters")]
    public string Name { get; set; } 
    
    [Required(ErrorMessage="Password is Required!")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    [Compare("Password")]
    public string Confirm { get; set; }
}
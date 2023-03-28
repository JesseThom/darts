#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace darts.Models;

[NotMapped]
public class Login
{
    [Required(ErrorMessage="Name is Required!")]
    [Display(Name ="Name")]
    public string NameLogin { get; set; } 

    [Required(ErrorMessage="Password is Required!")]
    [DataType(DataType.Password)]
    [Display(Name ="Password")]
    public string PasswordLogin { get; set; } 
}
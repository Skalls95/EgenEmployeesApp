using EmployeesApp.Web.Attributes;
using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Web.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [BannedWords("skit","gubbe","rövhål","jesus","groda")]
        [Required(ErrorMessage = "Du måste ange namn!")]
        [Display(Name = "Namn", Prompt = "Ange ditt namn.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Du måste ange email!")]
        [EmailAddress(ErrorMessage = "Måste ange giltig emailadress!")]
        [Display(Name = "Email", Prompt = "Ange din emailadress.")]
        public string Email { get; set; }
    }
}

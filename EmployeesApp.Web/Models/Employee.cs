﻿using System.ComponentModel.DataAnnotations;
using EmployeesApp.Web.Attributes;

namespace EmployeesApp.Web.Models
{

    public class Employee
    {
        public int Id { get; set; }

        [NoDigits(ErrorMessage = "Namnet får ej innehålla siffror!")]
        [Required(ErrorMessage = "Du måste ange namn!")]
        [Display(Name = "Namn", Prompt = "Ange ditt namn.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Du måste ange email!")]
        [EmailAddress(ErrorMessage = "Måste ange giltig emailadress!")]
        [Display(Name = "Email", Prompt = "Ange din emailadress.")]
        public required string Email { get; set; }

        public bool OnWork { get; set; }

        public decimal HourlyWage { get; set; } = 110;

        public List<StampTime> Stamps { get; set; } = new List<StampTime>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace petrotest.Models
{
    public class RegistrarViewModel
    {

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool Reserva { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace petrotest.Models
{
    public class EditarUsuarioViewModel
    {
        public string? Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool Estado { get; set; }
    }
}

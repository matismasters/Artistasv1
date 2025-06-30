using System.ComponentModel.DataAnnotations;

namespace Artistas.Models.DTOs
{
    public class LoginUsuarioDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

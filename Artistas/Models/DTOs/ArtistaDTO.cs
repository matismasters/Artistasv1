using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Artistas.Models.DTOs
{
    public class ArtistaDTO
    {
        public int Id { get; set; } = 0;
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string? Genero { get; set; }
        [Required]
        public DateOnly FechaNacimiento { get; set; }
        public string? Nacionalidad { get; set; }
        [Required]
        public int CategoriaId { get; set; } = 0;
    }
}

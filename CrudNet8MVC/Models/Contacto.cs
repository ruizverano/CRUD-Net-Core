using System.ComponentModel.DataAnnotations;

namespace CrudNet8MVC.Models
{
    public class Contacto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es obligarorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El telefono es obligarorio")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El celular es obligarorio")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El e_mail es obligarorio")]
        public string Email { get; set; }
    }
}

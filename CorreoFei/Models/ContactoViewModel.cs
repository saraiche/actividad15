using System.ComponentModel.DataAnnotations;

namespace CorreoFei.Models
{
    public class ContactoViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Nombre del contacto")]

        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo electrónica válida.")]
        public string Correo { get; set; }
    }
}

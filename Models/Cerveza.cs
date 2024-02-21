using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace CerveceriaAPI.Models
{
    [MetadataType(typeof(Cerveza))]
    public class Cerveza
    {
        public int Id { get; set; }

        [StringLength(4, ErrorMessage = "El campo {0} debe tener al menos {1} letras/números.", MinimumLength = 4)]
        public string Nombre { get; set; }

        public Marca? Marca { get; set; }

        public Cerveza(int id, string nombre, Marca marca)
        {
            Id = id;
            Nombre = nombre;
            Marca = marca;
        }

        public Cerveza()
        {
            Nombre = System.String.Empty;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApiTodo.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "El titulo no puede tener mas de 50 caracteres")]
        public string Title { get; set; }
        [MaxLength(500, ErrorMessage = "La descripcion no puede tener mas de 500 caracteres")]
        public string Description { get; set; }
        [MaxLength(50, ErrorMessage = "La condicion no puede tener mas de 50 caracteres")]
        public string Condition { get; set; }
        public string Creation_date { get; set; }
    }
}

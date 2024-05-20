using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesWebApi.Controllers
{
    [Table("Notes")]
    public class NoteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(maximumLength:256)]
        public string NoteName { get; init; }
        public DateTime? Created { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace core_ef_learning.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //public ICollection<Book> Books { get; set; }

    }
}
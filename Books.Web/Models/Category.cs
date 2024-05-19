using System.ComponentModel.DataAnnotations;

namespace Books.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Name { get; set; }

        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}

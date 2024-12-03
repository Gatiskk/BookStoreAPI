using System.ComponentModel.DataAnnotations;

namespace BookStoreAPI.Models
{
    namespace BookStoreAPI.Models
    {
        public class Book
        {
            public int Id { get; set; } 

            [Required]  // Makes sure the Title is required
            [StringLength(200)]  // Maximum length of 200 characters
            public string Title { get; set; }  

            [Required]  // Makes sure the Author is required
            public string Author { get; set; }  

            [Required]  // Makes sure the Genre is required
            public string Genre { get; set; } 

            public DateTime PublishedDate { get; set; }

            [Required]  // Makes sure the Price is required
            public decimal Price { get; set; }
        }
    }

}

using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Carcls
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        [StringLength(100, ErrorMessage = "Brand cannot be longer than 100 characters.")]
        public string? brand { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        public string? model { get; set; }

        public string? year { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        public string? color { get; set; }

        [Required(ErrorMessage = "Number plate is required.")]
        public string? numberplate { get; set; }

        public string? engine { get; set; }

        
        public string? fuel { get; set; }

       
        public string? image { get; set; }
        public IFormFile ImageFile { get; set; }

        public string? seatcapacity { get; set; }
    }
}
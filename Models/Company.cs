using System.ComponentModel.DataAnnotations;

namespace FertilizerShopWeb.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Company Type is required")]
        public string CompanyType { get; set; }  // Seed, Fertilizer, Chemicals

        [Required(ErrorMessage = "Contact Person is required")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please include '@' in the email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Product>? Products { get; set; }
    }
}

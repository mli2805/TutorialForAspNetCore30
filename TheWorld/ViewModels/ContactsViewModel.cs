using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
    public class ContactsViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(4096, MinimumLength = 10, ErrorMessage = "inconsistent message length")]
        public string Message { get; set; }
    }
}

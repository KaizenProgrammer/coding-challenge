using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace coding_challenge.ViewModels
{
    public class AddCustomerRequest
    {
        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [Required]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}

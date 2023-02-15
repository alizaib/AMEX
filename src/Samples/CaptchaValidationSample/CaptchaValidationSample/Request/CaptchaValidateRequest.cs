using System.ComponentModel.DataAnnotations;

namespace CaptchaValidationSample.Request
{
    public class CaptchaValidateRequest
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(6)]
        public string CaptchaCode { get; set; }
    }
}
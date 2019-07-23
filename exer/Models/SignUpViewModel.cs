using System;
using System.ComponentModel.DataAnnotations;

namespace Main.Models
{
    public class SignUpViewModel
    {
        [Required, MaxLength(256)]
        public string Username { get; set; }

        [Required, MaxLength(256)]
        public string FirstName { get; set; }

        [Required, MaxLength(256)]
        public string LastName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public SignUpViewModel() { }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MVCmodel.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [DataType(DataType.Password),Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace sample_api_csharp.Models
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Nome � obrigat�rio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Email � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endere�o de email v�lido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha � obrigat�rio.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo CEP � obrigat�rio.")]
        public string ZipCode { get; set; }
    }
}

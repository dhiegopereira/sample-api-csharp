using System.ComponentModel.DataAnnotations;

namespace sample_api_csharp.Models
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public string ZipCode { get; set; }
    }
}

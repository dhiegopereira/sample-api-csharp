using System.ComponentModel.DataAnnotations;

namespace sample_api_csharp.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "O campo Email � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endere�o de email v�lido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha � obrigat�rio.")]
        public string Password { get; set; }
    }
}

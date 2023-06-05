using System.Reflection.Metadata.Ecma335;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.MVC.Models
{
    public class ClienteCadastroModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1}caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do cliente.")]
        public  string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o Email do cliente.")]
        public string Email { get; set; }

        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Por favor, preencha 11 digitos numéricos sem pontos e traços.")]
        [Required(ErrorMessage = "Por favor, informe o CPF do cliente.")]
        public string Cpf { get; set; }
    }
}

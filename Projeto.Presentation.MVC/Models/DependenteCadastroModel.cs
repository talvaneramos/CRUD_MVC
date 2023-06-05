using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.MVC.Models
{
    public class DependenteCadastroModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do dependente.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de nascimento.")]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Por favor, selecione 1 cliente. ")]
        public string IdCliente { get; set; }
        public List<SelectListItem> ListagemDeClientes { get; set; }
    }
}

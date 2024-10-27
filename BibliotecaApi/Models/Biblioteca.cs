using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Models
{
    public class Biblioteca
    {
        [Key] // Indica que este é a chave primária
        public int Id { get; set; }
        
        [Required] // Indica que este campo é obrigatório
        public required string Nome { get; set; } // Modificador 'required' adicionado
        
        public required string InicioFuncionamento { get; set; } // Modificador 'required' adicionado
        
        public required string FimFuncionamento { get; set; } // Modificador 'required' adicionado
        
        public int Inauguracao { get; set; }
        
        [Phone] // Valida que o formato é de um telefone
        public required string Contato { get; set; } // Modificador 'required' adicionado
    }
}

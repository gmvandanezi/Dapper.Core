using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Core.Models
{
    public class Character
    {
        public string Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public ESexo Gender { get; set; }
        [Required]
        public EVocacao Vocacao { get; set; }

        public Character()
        {
            Id = Guid.NewGuid().ToString();
        }

    }

    public enum EVocacao { 
        Guerreiro, 
        Mago, 
        Sacerdote, 
        Arqueiro, 
        Assassino, 
        Druida 
    }
    public enum ESexo { 
        Masculino, 
        Feminino
    }
}

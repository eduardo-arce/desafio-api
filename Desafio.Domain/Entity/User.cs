using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(10)]
        public string AcessLevel { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

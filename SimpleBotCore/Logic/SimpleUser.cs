using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Logic
{
    public class SimpleUser
    {
        public string Id { get; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cor { get; set; }

        public SimpleUser(string userId)
        {
            this.Id = userId ?? throw new ArgumentNullException(nameof(userId));
        }

        public SimpleUser(string id, string nome, int idade, string cor)
        {
            this.Id = id;
            this.Nome = nome;
            this.Idade = idade;
            this.Cor = cor;
        }
    }
}

using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories.Interfaces
{
    public interface ISqlUserProfileRepository
    {
        SimpleUser TryLoadUser(string userId);
        SimpleUser Create(SimpleUser user);
        void AtualizaNome(string userId, string name);
        void AtualizaIdade(string userId, int idade);
        void AtualizaCor(string userId, string name);
    }
}

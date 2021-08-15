using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories.Interfaces
{
    public interface ISqlAskRepository
    {
        void StoreAsk(string ask);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public interface IAskRepository
    {
        void StoreAsk(string ask);
    }
}

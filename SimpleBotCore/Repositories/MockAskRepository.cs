using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class MockAskRepository : IMockAskRepository
    {
        Dictionary<string, string> _ask = new Dictionary<string, string>();

        public void StoreAsk(string ask)
        {
            _ask["ask"] = ask;
        }
    }
}

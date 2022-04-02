using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commento.Helpers
{
    public static class Validator
    {
        public static bool QueryValidator(string hmac, string token)
        {
            if (hmac == null || token == null)
                return false;
            return true;
        }
    }
}

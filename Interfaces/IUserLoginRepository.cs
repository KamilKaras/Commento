using System;
using System.Threading.Tasks;

namespace Commento
{
    public interface IUserLoginRepository
    {
        Task<Tuple<bool, byte[]>> CheckHmacCorrect(string token, string hmac, string secretKey);
        Task<Tuple<string, string>> HmacAndPayloadPrepare(string token, byte[] secretKeyByte);
    }
}
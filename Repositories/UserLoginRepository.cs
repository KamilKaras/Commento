using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Commento
{
    public class UserLoginRepository : IUserLoginRepository
    {
        public  List<User> UsersList { get; set; } = new List<User>()
        {
            new User(){Email = "tomek@wp.pl", Name = "Tomek"},
            new User(){Email = "monika@wp.pl", Name = "Monika"},
            new User(){Email = "bartek@wp.pl", Name = "Bartek"},
            new User(){Email = "filip@wp.pl", Name = "Filip"},
        };

        public async Task<Tuple<bool, byte[]>> CheckHmacCorrect(string token, string hmac, string secretKey)
        {
            byte[] hmacByte = Conversion.ConvertHexToByte(hmac);
            byte[] secretKeyByte = Conversion.ConvertHexToByte(secretKey);
            byte[] tokenByte = Conversion.ConvertHexToByte(token);

            byte[] expectedHmac = new HMACSHA256(secretKeyByte).ComputeHash(tokenByte);
            var checkHmacCorrect = expectedHmac.SequenceEqual(hmacByte);

            if (checkHmacCorrect)
                return Tuple.Create(true, secretKeyByte);
            return Tuple.Create(false, secretKeyByte);
        }

        public async Task<Tuple<string, string>> HmacAndPayloadPrepare(string token, byte[] secretKeyByte )
        {
            var newUser = ChooseRandomUser(new Random(), token);

            var newUserJson = JsonSerializer.Serialize(newUser);
            var newUserByte = Encoding.ASCII.GetBytes(newUserJson);
            var payload = Conversion.ConvertByteToHex(newUserByte);

            byte[] hmacToSendByte = new HMACSHA256(secretKeyByte).ComputeHash(newUserByte);
            var hmacToSend = Conversion.ConvertByteToHex(hmacToSendByte);

            return Tuple.Create(payload, hmacToSend);
        }

        private User ChooseRandomUser(Random random, string token)
        {
            var index = random.Next(UsersList.Count);
            UsersList[index].Token = token;
            return UsersList[index];
        }
    }
}

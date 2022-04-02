using Commento.Helpers;
using Commento.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Commento.Controllers
{
    [ApiController]
    public class CommentoController : ControllerBase
    {
        private string SecretKey { get; set; }

        public CommentoController(IConfiguration configuration)
        {
            SecretKey = configuration.GetSection("HMAC").GetValue<string>("Secret");
        }

        [HttpGet]
        [Route("authentication")]
        public async Task<IActionResult> UserLoginValidator([FromQuery] string hmac, [FromQuery] string token)
        {

            if (Validator.QueryValidator(hmac, token))
            {
                byte[] hmacByte = Conversion.ConvertHexToByte(hmac);
                byte[] secretKeyByte = Conversion.ConvertHexToByte(SecretKey);
                byte[] tokenByte = Conversion.ConvertHexToByte(token);

                byte[] expectedHmac = new HMACSHA256(secretKeyByte).ComputeHash(tokenByte);
                var checkHmacCorrect = expectedHmac.SequenceEqual(hmacByte);

                if (!checkHmacCorrect)
                    return Unauthorized();

                var newUser = new NewUser()
                {
                    Token = token,
                    Email = "kamil.karasiewicz11@gmail.com",
                    Name = "Kamil",
                };

                var newUserJson = JsonSerializer.Serialize(newUser);
                var newUserByte = Encoding.ASCII.GetBytes(newUserJson);
                var payload = Conversion.ConvertByteToHex(newUserByte);

                byte[] hmacToSendByte = new HMACSHA256(secretKeyByte).ComputeHash(newUserByte);
                var hmacToSend = Conversion.ConvertByteToHex(hmacToSendByte);

                return Redirect("https://commento.io/api/oauth/sso/callback?payload=" + payload + "&hmac=" + hmacToSend);
            }

            return BadRequest();
        }
    }
}

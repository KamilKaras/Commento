using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Commento.Controllers
{
    [ApiController]
    public class CommentoController : ControllerBase
    {
        private string SecretKey { get; set; }
        public IUserLoginRepository _userLoginRepository { get; set; }

        public CommentoController(IConfiguration configuration, IUserLoginRepository userLoginRepository)
        {
            SecretKey = configuration.GetSection("HMAC").GetValue<string>("Secret");
            _userLoginRepository = userLoginRepository;
        }

        [HttpGet]
        [Route("authentication")]
        public async Task<IActionResult> UserLoginValidator([FromQuery] string hmac, [FromQuery] string token)
        {

            if (Validator.QueryValidator(hmac, token))
            {
                try
                {
                    var hexDecode = await _userLoginRepository.CheckHmacCorrect(token, hmac, SecretKey);

                    if (!hexDecode.Item1)
                        return Unauthorized();

                    var dataToSend = await _userLoginRepository.HmacAndPayloadPrepare(token, hexDecode.Item2);

                    return Redirect("https://commento.io/api/oauth/sso/callback?payload=" + dataToSend.Item1 + "&hmac=" + dataToSend.Item2);
                }
                catch(Exception e)
                {
                    return NotFound(e);
                }
            }
            return BadRequest();

        }
    }
}

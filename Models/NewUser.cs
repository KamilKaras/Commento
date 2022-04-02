using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commento.Models
{
    public class NewUser
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }
}

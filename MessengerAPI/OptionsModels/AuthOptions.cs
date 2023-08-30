using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MessengerAPI.OptionsModels
{
    public class AuthOptions
    {
        public string ISSUER { get; set; } = string.Empty;
        public string AUDIENCE { get; set; } = string.Empty;
        public string KEY { get; set; } = string.Empty;
    }
}

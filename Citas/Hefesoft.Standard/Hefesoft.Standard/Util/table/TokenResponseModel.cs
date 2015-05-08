using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.Util.table
{
    public class TokenResponseModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string Username { get; set; }

        [JsonProperty(".issued")]
        public string IssuedAt { get; set; }

        [JsonProperty(".expires")]
        public string ExpiresAt { get; set; }
    }

    public class Usuario
    {
        public string UserName { get; set; }
        public string Password { get; set; }       
    }

    public class PushAzureService
    {
        public string idhubazure { get; set; }
        public string tag { get; set; }

        public string platform { get; set; }

        //Llave de la app windows store o windows phone
        public string key { get; set; }

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Services
{
    public class FirebaseSettings
    {
        [JsonPropertyName("project_id")]
        public string ProjectId => "that-rug-really-tied-the-room-together-72daa";

        [JsonPropertyName("private_key_id")]
        public string PrivateKeyId => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        [JsonPropertyName("client_email")]
        public string ClientEmail => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        
        [JsonPropertyName("client_id")]
        public string ClientID => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        
        [JsonPropertyName("auth_uri")]
        public string AuthURI => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        
        [JsonPropertyName("token_uri")]
        public string TokeURI => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        [JsonPropertyName("auth_provider_x509_cert_url")]
        public string AuthProviderCerturl => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        [JsonPropertyName("client_x509_cert_url")]
        public string ClientCerturl => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        [JsonPropertyName("universe_domain")]
        public string UniverseDomain => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NayaViewAnimeApi.Infrastructure.utils
{
    class CodeVeriferUtil
    {

        public static string GenerateCodeVerifier()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", ""); // PKCE usa Base64 URL Safe sin padding
        }
    }
}

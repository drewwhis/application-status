using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ApplicationStatus.Utilities;

public static class ApiKeyHasher
{
    public static string Hash(string apiKey)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: apiKey,
            salt: Encoding.ASCII.GetBytes(apiKey.Split(".")[0]),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}
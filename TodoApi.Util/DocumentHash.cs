using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using TodoApi.Util.Interfaces;

namespace TodoApi.Util
{
    public class DocumentHash(IConfiguration config) : IDocumentHash
    {
        private byte[] GetPepperInBytes()
        {
            var pepperString = config["Hashing:DocumentPepper"];
            if (string.IsNullOrEmpty(pepperString))
                throw new ArgumentNullException(nameof(pepperString), "The key 'pepper' for documents whas not configured yet.");
            
            return Encoding.UTF8.GetBytes(pepperString);
        }

        public string HashDocument(string document)
        {
            if (string.IsNullOrEmpty(document)) return string.Empty;

            var normalizedDocument = new string(document.Where(char.IsDigit).ToArray());

            using (var hmac = new HMACSHA256(GetPepperInBytes()))
            {
                var documentBytes = Encoding.UTF8.GetBytes(document);
                var hashBytes = hmac.ComputeHash(documentBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyDocument(string hashedDocument, string providedDocument)
        {
            if (string.IsNullOrEmpty(hashedDocument) || string.IsNullOrEmpty(providedDocument)) return false;

            var normalizedProvidedDocument = new string(providedDocument.Where(char.IsDigit).ToArray());

            using (var hmac = new HMACSHA256(GetPepperInBytes()))
            {
                var documentBytes = Encoding.UTF8.GetBytes(providedDocument);
                var hashBytes = hmac.ComputeHash(documentBytes);
                return Convert.ToBase64String(hashBytes) == hashedDocument;
            }
        }
    }
}

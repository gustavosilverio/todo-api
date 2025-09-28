namespace TodoApi.Util.Interfaces
{
    public interface IDocumentHash
    {
        /// <summary>
        /// Generate a deterministic hash for a document using HMAC-SHA256.
        /// </summary>
        /// <param name="document">The document in plain text (e.g.: CPF, CNPJ).</param>
        /// <returns>The hash in base64.</returns>
        string HashDocument(string document);

        /// <summary>
        /// Verify if the providedDocument is equal to the hashedDocument
        /// </summary>
        /// <param name="hashedDocument">The hash document</param>
        /// <param name="providedDocument">The provided document in plain text</param>
        /// <returns>True if they are equal, otherwise False</returns>
        bool VerifyDocument(string hashedDocument, string providedDocument);
    }
}

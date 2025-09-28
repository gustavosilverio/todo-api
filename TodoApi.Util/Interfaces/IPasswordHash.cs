namespace TodoApi.Util.Interfaces
{
    public interface IPasswordHash
    {
        /// <summary>
        /// Generate a hash from a plain text password
        /// </summary>
        /// <param name="password">The password in plain text</param>
        /// <returns>The hash of the password</returns>
        string HashPassword(string password);

        /// <summary>
        /// Verify if the provided password in plain text corresponds to the given hash
        /// </summary>
        /// <param name="hashedPassword">A password hash</param>
        /// <param name="providedPassword">The password in plain text</param>
        /// <returns>True if they are equal, otherwise False</returns>
        bool VerifyPassword(string hashedPassword, string providedPassword);
    }
}

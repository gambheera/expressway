using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Utility.Encriptors
{
    public class Hasher
    {
		public async static Task<string> CreatePasswordHash(string password)
		{
			return await CreatePasswordHash(password, await CreateSalt());
		}

		public async static Task<bool> Validate(string password, string passwordHash)
		{
			try
			{
				var saltPosition = 5;
				var saltSize = 10;
				var salt = passwordHash.Substring(saltPosition, saltSize);
				var hashedPassword = await CreatePasswordHash(password, salt);
				return hashedPassword == passwordHash;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private async static Task<string> CreatePasswordHash(string pwd, string salt)
		{
			string saltAndPwd = String.Concat(pwd, salt);
			string hashedPwd = await GetHashString(saltAndPwd);
			var saltPosition = 5;
			hashedPwd = hashedPwd.Insert(saltPosition, salt);
			return hashedPwd;
		}

		private async static Task<string> CreateSalt()
		{
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] buff = new byte[20];
			rng.GetBytes(buff);
			var saltSize = 10;
			string salt = Convert.ToBase64String(buff);
			if (salt.Length > saltSize)
			{
				salt = salt.Substring(0, saltSize);
				return salt.ToUpper();
			}

			var saltChar = '^';
			salt = salt.PadRight(saltSize, saltChar);
			return salt.ToUpper();
		}

		private async static Task<string> GetHashString(string password)
		{
			StringBuilder sb = new StringBuilder();
			foreach (byte b in await GetHash(password))
				sb.Append(b.ToString("X2"));
			return sb.ToString();
		}

		private async static Task<byte[]> GetHash(string password)
		{
			SHA384 sha = new SHA384CryptoServiceProvider();
			return sha.ComputeHash(Encoding.UTF8.GetBytes(password));
		}
	}
}

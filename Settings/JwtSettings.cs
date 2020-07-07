using System;
using System.Collections.Generic;
using System.Text;

namespace learn_netcore.Settings
{
    public class JwtSettings
	{
		// public string Value { get; set; }
		public string Secret { get; set; }
		public string Issuer { get; set; }

		/// <summary>
		/// The audience of a token is the intended recipient of the token. The audience value is a string. Typically the base address of the resource being accessed, such as "https://contoso.com".
		/// </summary>
		public string Audience { get; set; }

		public int ExpirationInDays { get; set; }
    }
}
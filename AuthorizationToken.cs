﻿/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;

namespace Microsoft.Partner.CSP.APISamples.SubscriptionStream
{
	class AuthorizationToken
	{
		/// <summary>
		/// Captures when the token expires
		/// </summary>
		private DateTime expiresOn { get; set; }

		/// <summary>
		/// Access token
		/// </summary>
		public string AccessToken { get; private set; }

		/// <summary>
		/// Constructor for getting an authorization token
		/// </summary>
		/// <param name="access_Token">access token</param>
		/// <param name="expires_in">number of seconds the token is valid for</param>
		public AuthorizationToken(string access_Token, long expires_in)
		{
			this.AccessToken = access_Token;
			this.expiresOn = DateTime.UtcNow.AddSeconds(expires_in);
		}

		public AuthorizationToken(string access_Token, DateTimeOffset expires_on)
		{
			this.AccessToken = access_Token;
			this.expiresOn = expires_on.DateTime;
		}
		/// <summary>
		/// Returns true if the authorization token is near expiry
		/// </summary>
		/// <returns>true if the authorization token is near expiry</returns>
		public bool IsNearExpiry()
		{
			//// if token is expiring in the next minute or expired, return true
			return DateTime.UtcNow > this.expiresOn.AddMinutes(-1);
		}
	}
}

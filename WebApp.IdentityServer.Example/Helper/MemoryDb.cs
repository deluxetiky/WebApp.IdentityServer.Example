using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace WebApp.IdentityServer.Example.Helper
{
	public class MemoryDb
	{
		// scopes define the API resources in your system
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("sinanbir.com.auth", "My API Display Name")
			};
		}

		// clients
		public static IEnumerable<Client> GetClients()
		{
			// client credentials client
			return new List<Client>
			{
				new Client
				{
					ClientId = "client1",
					AllowedGrantTypes = GrantTypes.ClientCredentials,

					ClientSecrets =
					{
						new Secret("password".Sha256())
					},
					AllowedScopes = { "sinanbir.com.auth" }
				},
 
                // resource owner password grant client
                new Client
				{
					ClientId = "ropcclient",
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
					AllowOfflineAccess = true,//Enables refresh token.
                    ClientSecrets =
					{
						new Secret("password".Sha256())
					},
					AllowedScopes = { "sinanbir.com.auth", IdentityServerConstants.StandardScopes.OfflineAccess, }
				}
			};
		}

		public static List<TestUser> GetUsers()
		{
			return new List<TestUser>
			{
				new TestUser
				{
					SubjectId = "1",
					Username = "sinan",
					Password = "bir"
				},
				new TestUser
				{
					SubjectId = "2",
					Username = "testuser",
					Password = "password"
				}
			};
		}
	}
}

// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityServer4;

namespace IdentityServerHost.Quickstart.UI {
    public class TestUsers {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };

                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "818727",
                        Username = "alice",
                        Password = "alice",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                                IdentityServerConstants.ClaimValueTypes.Json),
                            
                            new Claim("Role","internal"),
                            new Claim("Policy","principal")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "88421113",
                        Username = "bob",
                        Password = "bob",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                                IdentityServerConstants.ClaimValueTypes.Json),
                            
                            
                            new Claim("Role","internal"),
                            new Claim("Policy","principal")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "8187963874",
                        Username = "ben",
                        Password = "ben",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Ben Sam"),
                            new Claim(JwtClaimTypes.GivenName, "Ben"),
                            new Claim(JwtClaimTypes.FamilyName, "Sam"),
                            new Claim(JwtClaimTypes.Email, "bensam@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://ben.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
                                IdentityServerConstants.ClaimValueTypes.Json),
                            
                            new Claim("Role","internal"),
                            new Claim("Policy","principal")
                        }
                    },
                };
            }
        }
    }
}
using Lob.Net.Exceptions;
using Lob.Net.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lob.Net.Sample
{
    class Letters
    {
        private readonly ILobLetters lobLetters;

        public Letters(
            ILobLetters lobLetters
        )
        {
            this.lobLetters = lobLetters;
        }

        public async Task Run()
        {
            try
            {
                var result1 = await lobLetters.CreateAsync(new LetterRequest
                {
                    From = new AddressReference(new AddressRequest
                    {
                        Name = "Jean-Sébastien Goupil",
                        Company = "JSGoupil, LLC",
                        AddressLine1 = "123 Main Street",
                        AddressCity = "Seattle",
                        AddressState = "WA",
                        AddressZip = "98103",
                        AddressCountry = "US"
                    }),
                    To = new AddressReference("adr_3c76dcedb5f88d79"), // Saved address in LOB
                    DoubleSided = true,
                    AddressPlacement = AddressPlacement.InsertBlankPage,
                    Color = false,
                    File = "https://rapidcontactfunctions.azurewebsites.net/api/b27ecabc-779e-48db-ac5d-d256cddf8397/Documents/Export/5f330d4f-cf96-4f22-8a62-b7ae47b22b27", // Saved template in LOB
                    MergeVariables = new Dictionary<string, string>
                    {
                        {"variable_name", "Jean-Sébastien" }
                    }
                });

                var str1 = JsonConvert.SerializeObject(result1);
                Console.WriteLine(str1);

                var enumerable = lobLetters.ListObjectsAsync();
                var enumerator = enumerable.GetAsyncEnumerator();
                await enumerator.MoveNextAsync();
                var result2 = enumerator.Current;

                var str2 = JsonConvert.SerializeObject(result2);
                Console.WriteLine(str2);
            }
            catch (LobException ex)
            {
                throw ex;
            }
        }
    }
}

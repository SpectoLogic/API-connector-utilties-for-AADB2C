using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace SpectoLogic.Identity.AADB2C.APIConnectors.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<T> GetClaimsRequestAsync<T>(this Stream dataStream)
        {
            using (var streamReader = new StreamReader(dataStream))
            {
                var requestBody = await streamReader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(requestBody);
            }
        }
    }
}

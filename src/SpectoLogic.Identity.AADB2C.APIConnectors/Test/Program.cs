using Newtonsoft.Json;
using Test;
Console.WriteLine("Just a simple Test");

var request = new MyClaimsRequest();
request.PhoneNumber = "+43 12345";
request.LoyaltyId = 1;

var result = JsonConvert.SerializeObject(request);
Console.WriteLine(result);

var response = new MyClaimsResponse();
response.PhoneNumber = "+43 12345";
response.LoyaltyId = 1;

result = JsonConvert.SerializeObject(response);
Console.WriteLine(result);

/*
MemoryStream memStream = new MemoryStream();
var requestFromStream = memStream.GetClaimsRequestAsync<MyClaimsRequest>().Result;
*/
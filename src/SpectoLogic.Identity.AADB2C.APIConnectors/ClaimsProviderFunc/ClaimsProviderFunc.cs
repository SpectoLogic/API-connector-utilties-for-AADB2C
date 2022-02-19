using ClaimsProviderFunc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SpectoLogic.Identity.AADB2C.APIConnectors.Extensions;
using SpectoLogic.Identity.AADB2C.APIConnectors.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsProviderFunc
{
    public class ClaimsProviderFunc
    {
        private const string BASIC_AUTH_USERNAME = "admina";
        private const string BASIC_AUTH_PASSWORD = "PleaseUseCertificatesInstead!";

        [FunctionName("CreateUser")]
        public async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (!IsAuthenticated(req.Headers))
            {
                return new UnauthorizedResult();
            }

            try
            {
                var claimsReq = await req.Body.GetClaimsRequestAsync<MyClaimsRequest>();

                var phoneNumber = claimsReq.PhoneNumber;
                phoneNumber = phoneNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
                if (string.IsNullOrEmpty(phoneNumber) || !phoneNumber.StartsWith("+"))
                {
                    return ShowValidationError("Please provide a valid phone number e.g +43 123 4567");
                }

                return new OkObjectResult(
                    new MyClaimsResponse()
                    {
                        ADACId = Guid.NewGuid().ToString("D"),
                        PhoneNumber = phoneNumber
                    })
                {
                    StatusCode = 200
                };
            }
            catch (Exception)
            {
                var msgResponse = new MessageResponse()
                {
                    Action = ResponseActions.ShowBlockPage,
                    UserMessage = "There was a problem with your request. You are not able to sign up at this time."
                };
                return new BadRequestObjectResult(msgResponse);
            }
        }

        [FunctionName("AddClaims")]
        public async Task<IActionResult> AddClaims(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (!IsAuthenticated(req.Headers))
            {
                return new UnauthorizedResult();
            }
            var claimsReq = await req.Body.GetClaimsRequestAsync<MyClaimsRequest>();

            var claimsResponse = new AddClaimsResponse()
            {

            };
            return new OkObjectResult(claimsResponse);
        }

        public IActionResult ShowBlockError(string errorMessage)
        {
            var msgResponse = new ErrorMessageResponse()
            {
                Action = ResponseActions.ShowBlockPage,
                UserMessage = errorMessage
            };
            return new BadRequestObjectResult(msgResponse);
        }

        public IActionResult ShowValidationError(string errorMessage)
        {
            var msgResponse = new ErrorMessageResponse()
            {
                Action = ResponseActions.ValidationError,
                UserMessage = errorMessage
            };
            return new BadRequestObjectResult(msgResponse);
        }

        /// <summary>
        /// HANDLE BASIC AUTHENTICATION
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        private bool IsAuthenticated(IHeaderDictionary headers)
        {
            if (headers == null || !headers.ContainsKey("Authorization"))
            {
                return false;
            }

            string authHeader = headers["Authorization"];

            if (string.IsNullOrWhiteSpace(authHeader))
            {
                return false;
            }

            var username = BASIC_AUTH_USERNAME;
            var password = BASIC_AUTH_PASSWORD;
            var encoded = Convert.ToBase64String(
                Encoding.GetEncoding("ISO-8859-1")
                .GetBytes(username + ":" + password));

            var expectedHeader = $"Basic {encoded}";
            return expectedHeader == authHeader;
        }

    }
}

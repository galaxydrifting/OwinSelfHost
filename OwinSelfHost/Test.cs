using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace OwinSelfHost
{
    /// <summary>
    /// Reference https://docs.microsoft.com/en-us/aspnet/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api
    /// </summary>
    [TestClass]
    public class OwinSelfHostTests
    {
        public const string baseAddress = @"http://localhost:9000/";
        public const string url = @"http://localhost:9000/api/Server";

        [TestMethod]
        public void CallServerApi()
        {
            bool expected = true;
            bool actual = false;
            MockResponse result;

            // Start OWIN host 
            using (WebApp.Start<OwinSelfhost.Server.Startup>(url: baseAddress))
            {
                HttpClient httpClient = new HttpClient();

                StubApiParam fakeApiParam = new StubApiParam()
                {
                    Name = "ABC",
                    Value = 123
                };
                
                var stringInput = JsonSerializer.Serialize(fakeApiParam);
                var response = httpClient.PostAsync(url, new StringContent(stringInput, Encoding.UTF8, "application/json")).Result;

                result = JsonSerializer.Deserialize<MockResponse>(response.Content.ReadAsStringAsync().Result);
                actual = result.success;
            }

            Assert.AreEqual(expected, actual);
        }
    }

    public class StubApiParam
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class MockResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}

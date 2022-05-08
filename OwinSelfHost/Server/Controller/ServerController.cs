using System.Collections.Generic;
using System.Web.Http;

namespace OwinHosting
{
    public class ServerController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public MockResponse Post([FromBody] FakeRequestParam input)
        {
            var mockResponse = new MockResponse
            {
                success = true,
                message = "Message",
            };

            return mockResponse;
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }

    public class FakeRequestParam
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

namespace King.Azure.Integration.Test.Data
{
    using System;
    using System.Threading.Tasks;
    using King.Azure.Data;
    using Xunit;
    
    public class FileShareTests
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=kingdottest;AccountKey=raLQzql5BzvYrHGPxZKJIFHDe/B0+kTpJwGokbQHX5p6EVbx8xOt6XbPKJsUWdyOMTYYEKAvZ7ImqFIfpLGOJQ==;FileEndpoint=https://kingdottest.file.core.windows.net/";

        [Fact]
        public async Task CreateIfNotExists()
        {
            var random = new Random();
            var storage = new FileShare(string.Format("a{0}b", random.Next()), ConnectionString);
            var created = await storage.CreateIfNotExists();

            //Assert.IsTrue(created);
        }
    }
}
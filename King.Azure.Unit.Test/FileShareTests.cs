namespace King.Azure.Unit.Test.Data
{
    using System;
    using King.Azure.Data;
    using Microsoft.WindowsAzure.Storage;
    using Xunit;


    public class FileShareTests
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=kingazure;AccountKey=LQFXI8kFSh0TR0dk2bvukQZRxymByGn1amCiR8chpIZ+NkLHqx6IFMcApHGWQutKpWfPloJfNv3ySM+uOJ3f9g==;";

        [Fact]
        public void Constructor()
        {
            new FileShare("test", ConnectionString);
        }

        [Fact]
        public void ConstructorAccount()
        {
            new FileShare("test", CloudStorageAccount.Parse(ConnectionString));
        }

        [Fact]
        public void IsAzureStorage()
        {
            //Assert.IsNotNull(new FileShare("test", ConnectionString) as AzureStorage);
        }

        [Fact]
        public void IsIFileShare()
        {
            //Assert.IsNotNull(new FileShare("test", ConnectionString) as IFileShare);
        }

        [Fact]
        public void ConstructorNameNull()
        {
            //Assert.That(() => new FileShare(null, ConnectionString), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorAccountNameNull()
        {
            //Assert.That(() => new FileShare(null, CloudStorageAccount.Parse(ConnectionString)), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void Client()
        {
            var name = Guid.NewGuid().ToString();
            var t = new FileShare(name, ConnectionString);
            //Assert.IsNotNull(t.Client);
        }

        [Fact]
        public void Reference()
        {
            var name = Guid.NewGuid().ToString();
            var t = new FileShare(name, ConnectionString);
            //Assert.IsNotNull(t.Reference);
        }

        [Fact]
        public void Name()
        {
            var name = Guid.NewGuid().ToString();
            var t = new FileShare(name, ConnectionString);
            //Assert.AreEqual(name, t.Name);
        }
    }
}
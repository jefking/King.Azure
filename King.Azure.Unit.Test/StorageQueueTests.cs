namespace King.Azure.Unit.Test.Data
{
    using System;
    using King.Azure.Data;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;
    using NUnit.Framework;

    public class StorageQueueTests
    {
        private const string ConnectionString = "UseDevelopmentStorage=true;";

        [Fact]
        public void Constructor()
        {
            new StorageQueue("test", ConnectionString, TimeSpan.FromSeconds(22));
        }

        [Fact]
        public void IQueue()
        {
            //Assert.IsNotNull(new StorageQueue("test", ConnectionString) as IStorageQueue);
        }

        [Fact]
        public void ConstructorTableNull()
        {
            //Assert.That(() => new StorageQueue(null, ConnectionString), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorAccountTableNull()
        {
            //Assert.That(() => new StorageQueue(null, CloudStorageAccount.Parse(ConnectionString)), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorKeyNull()
        {
            //Assert.That(() => new StorageQueue("test", (string)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void Name()
        {
            var name = Guid.NewGuid().ToString();
            var t = new StorageQueue(name, ConnectionString);
            //Assert.AreEqual(name, t.Name);
        }

        [Fact]
        public void Client()
        {
            var name = Guid.NewGuid().ToString();
            var t = new StorageQueue(name, ConnectionString);
            //Assert.IsNotNull(t.Client);
        }

        [Fact]
        public void Reference()
        {
            var name = Guid.NewGuid().ToString();
            var t = new StorageQueue(name, ConnectionString);
            //Assert.IsNotNull(t.Reference);
        }

        [Fact]
        public void DeleteNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new StorageQueue(name, ConnectionString);

            //Assert.That(() => t.Delete(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void SaveMessageNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new StorageQueue(name, ConnectionString);

            //Assert.That(() => t.Send((CloudQueueMessage)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void SaveNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new StorageQueue(name, ConnectionString);

            //Assert.That(() => t.Send((object)null), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
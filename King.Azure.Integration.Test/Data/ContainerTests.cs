﻿namespace King.Service.Integration
{
    using King.Azure.Data;
    using Microsoft.WindowsAzure.Storage;
    using NUnit.Framework;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    [TestFixture]
    public class ContainerTests
    {
        private readonly string ConnectionString = "UseDevelopmentStorage=true;";
        private const string ContainerName = "testing";

        #region Helper
        private class Helper
        {
            public Guid Id
            {
                get;
                set;
            }
        }
        #endregion

        [SetUp]
        public void SetUp()
        {
            var storage = new Container(ContainerName, ConnectionString);
            storage.CreateIfNotExists().Wait();
        }

        [TearDown]
        public void TearDown()
        {
            var storage = new Container(ContainerName, ConnectionString);
            storage.Delete().Wait();
        }

        [Test]
        public async Task CreateIfNotExists()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new Container(name, ConnectionString);
            var created = await storage.CreateIfNotExists();

            Assert.IsTrue(created);
        }

        [Test]
        public async Task RoundTrip()
        {
            var helper = new Helper()
            {
                Id = Guid.NewGuid(),
            };

            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, helper);
            var returned = await storage.Get<Helper>(blobName);

            Assert.IsNotNull(returned);
            Assert.AreEqual(helper.Id, returned.Id);
        }

        [Test]
        public async Task RoundTripBytes()
        {
            var random = new Random();
            var bytes = new byte[1024];
            random.NextBytes(bytes);

            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, bytes);
            var returned = await storage.Get(blobName);

            Assert.IsNotNull(returned);
            Assert.AreEqual(bytes.Length, returned.Length);
            Assert.AreEqual(bytes, returned);
        }

        [Test]
        [ExpectedException(typeof(StorageException))]
        public async Task Delete()
        {
            var helper = new Helper()
            {
                Id = Guid.NewGuid(),
            };

            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, helper);
            await storage.Delete(blobName);
            await storage.Get<Helper>(blobName);
        }
    }
}
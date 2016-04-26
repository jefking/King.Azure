﻿namespace King.Service.Integration
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using King.Azure.Data;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Xunit;
    using System.Linq;

    public class ContainerTests
    {
        private readonly string ConnectionString = "UseDevelopmentStorage=true;";
        private readonly string ContainerName = 'a' + Guid.NewGuid().ToString().Replace("-", "");

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

        //[SetUp]
        public void SetUp()
        {
            var storage = new Container(ContainerName, ConnectionString);
            storage.CreateIfNotExists().Wait();
        }

        //[TearDown]
        public void TearDown()
        {
            var storage = new Container(ContainerName, ConnectionString);
            storage.Delete().Wait();
        }

        [Fact]
        public async Task ConstructorAccount()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var account = CloudStorageAccount.Parse(ConnectionString);
            var storage = new Container(name, account);
            var created = await storage.CreateIfNotExists();

            //Assert.IsTrue(created);
        }

        [Fact]
        public async Task CreateIfNotExists()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new Container(name, ConnectionString);
            var created = await storage.CreateIfNotExists();

            //Assert.IsTrue(created);

            var blobClient = storage.Account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(name);
            var permissions = await container.GetPermissionsAsync();
            Assert.Equal(BlobContainerPublicAccessType.Off, permissions.PublicAccess);
        }

        [Fact]
        public async Task CreateIfNotExistsPublic()
        {
            var name = 'a' + Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'a');
            var storage = new Container(name, ConnectionString, true);
            var created = await storage.CreateIfNotExists();

            //Assert.IsTrue(created);

            var blobClient = storage.Account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(name);
            var permissions = await container.GetPermissionsAsync();
            Assert.Equal(BlobContainerPublicAccessType.Blob, permissions.PublicAccess);
        }

        [Fact]
        public async Task Exists()
        {
            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);
            await storage.Save(blobName, Guid.NewGuid());
            var exists = await storage.Exists(blobName);

            //Assert.IsTrue(exists);
        }

        [Fact]
        public async Task ExistsNo()
        {
            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            var exists = await storage.Exists(blobName);

            //Assert.IsFalse(exists);
        }

        [Fact]
        public async Task GetBlockReference()
        {
            var name = string.Format("{0}.bin", Guid.NewGuid());
            var storage = new Container(ContainerName, ConnectionString);
            await storage.Save(name, new Helper());

            var block = storage.GetBlockReference(name);
            //Assert.IsNotNull(block);
            //Assert.IsTrue(block.Exists());
        }

        [Fact]
        public async Task GetPageReference()
        {
            var random = new Random();
            var bytes = new byte[1024];
            random.NextBytes(bytes);

            var name = string.Format("{0}.bin", Guid.NewGuid());
            var storage = new Container(ContainerName, ConnectionString);
            var blob = storage.Reference.GetPageBlobReference(name);
            await blob.CreateAsync(1024);
            await blob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);

            var page = storage.GetPageReference(name);
            //Assert.IsNotNull(page);
            //Assert.IsTrue(page.Exists());
        }

        [Fact]
        public async Task RoundTripObject()
        {
            var helper = new Helper()
            {
                Id = Guid.NewGuid(),
            };

            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, helper);
            var returned = await storage.Get<Helper>(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(helper.Id, returned.Id);

            var properties = await storage.Properties(blobName);
            //Assert.IsNotNull(properties);
            Assert.Equal("application/json", properties.ContentType);
        }

        [Fact]
        public async Task RoundTripText()
        {
            var data = Guid.NewGuid().ToString();
            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, data);
            var returned = await storage.GetText(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(data, returned);

            var properties = await storage.Properties(blobName);
            //Assert.IsNotNull(properties);
            Assert.Equal("text/plain", properties.ContentType);
        }

        [Fact]
        public async Task JsonContentType()
        {
            var helper = new Helper()
            {
                Id = Guid.NewGuid(),
            };

            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, helper);
            var returned = await storage.Properties(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal("application/json", returned.ContentType);
        }

        [Fact]
        public async Task RoundTripBytes()
        {
            var random = new Random();
            var bytes = new byte[1024];
            random.NextBytes(bytes);

            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, bytes);
            var returned = await storage.Get(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(bytes.Length, returned.Length);
            Assert.Equal(bytes, returned);

            var properties = await storage.Properties(blobName);
            //Assert.IsNotNull(properties);
            Assert.Equal("application/octet-stream", properties.ContentType);
        }

        [Fact]
        public async Task RoundTripStream()
        {
            var random = new Random();
            var bytes = new byte[1024];
            random.NextBytes(bytes);

            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, bytes);
            using (var returned = await storage.Stream(blobName) as MemoryStream)
            {
                var stored = returned.ToArray();

                //Assert.IsNotNull(stored);
                Assert.Equal(bytes.Length, stored.Length);
                Assert.Equal(bytes, stored);
            }
        }

        [Fact]
        public async Task BytesDefaultContentType()
        {
            var random = new Random();
            var bytes = new byte[1024];
            random.NextBytes(bytes);

            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, bytes);
            var returned = await storage.Properties(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(bytes.Length, returned.Length);
            Assert.Equal("application/octet-stream", returned.ContentType);
        }

        [Fact]
        public async Task BytesContentType()
        {
            var random = new Random();
            var bytes = new byte[1024];
            random.NextBytes(bytes);

            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, bytes, "application/pdf");
            var returned = await storage.Properties(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(bytes.Length, returned.Length);
            Assert.Equal("application/pdf", returned.ContentType);
        }

        [Fact]
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

            //Assert.That(() => storage.Get<Helper>(blobName), Throws.TypeOf<StorageException>());
        }

        [Fact]
        public async Task List()
        {
            var random = new Random();
            var bytes = new byte[16];
            random.NextBytes(bytes);
            var count = random.Next(1, 32);
            var storage = new Container(ContainerName, ConnectionString);
            for (var i = 0; i < count; i++)
            {
                var blobName = Guid.NewGuid().ToString();
                await storage.Save(blobName, bytes);
            }

            var blobs = storage.List();
            //Assert.Equal(count, blobs.Count());
        }

        [Fact]
        public async Task SnapShotPageBlob()
        {
            var random = new Random();
            var bytes = new byte[1024];
            random.NextBytes(bytes);

            var name = string.Format("{0}.bin", Guid.NewGuid());
            var storage = new Container(ContainerName, ConnectionString);
            var blob = storage.Reference.GetPageBlobReference(name);
            await blob.CreateAsync(1024);
            await blob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);

            var snapshot = await storage.Snapshot(name);
            //Assert.IsTrue(snapshot.IsSnapshot);

            var returned = storage.GetPageReference(snapshot.Name, snapshot.SnapshotTime);
            //Assert.IsNotNull(returned);
            //Assert.IsTrue(returned.IsSnapshot);
        }

        [Fact]
        public async Task SnapShotBlockBlob()
        {
            var random = new Random();
            var bytes = new byte[16];
            random.NextBytes(bytes);

            var name = string.Format("{0}.bin", Guid.NewGuid());
            var storage = new Container(ContainerName, ConnectionString);
            await storage.Save(name, bytes);

            var snapshot = await storage.Snapshot(name);
            //Assert.IsTrue(snapshot.IsSnapshot);

            var returned = storage.GetPageReference(snapshot.Name, snapshot.SnapshotTime);
            //Assert.IsNotNull(returned);
            //Assert.IsTrue(returned.IsSnapshot);
        }

        [Fact]
        public async Task SnapShoAndDelete()
        {
            var random = new Random();
            var bytes = new byte[16];
            random.NextBytes(bytes);

            var name = string.Format("{0}.bin", Guid.NewGuid());
            var storage = new Container(ContainerName, ConnectionString);
            await storage.Save(name, bytes);

            var snapshot = await storage.Snapshot(name);
            await storage.Delete(name);
        }

        [Fact]
        public async Task SnapShoAndDeleteSafe()
        {
            var random = new Random();
            var bytes = new byte[16];
            random.NextBytes(bytes);

            var name = string.Format("{0}.bin", Guid.NewGuid());
            var storage = new Container(ContainerName, ConnectionString);
            await storage.Save(name, bytes);

            var snapshot = await storage.Snapshot(name);

            //Assert.That(() => storage.Delete(name, false), Throws.TypeOf<StorageException>());
        }

        [Fact]
        public async Task SnapshotNonExistant()
        {
            var blob = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);
            //Assert.IsNull(await storage.Snapshot(blob));
        }

        [Fact]
        public async Task DontLoseContentType()
        {
            var cache = "public, max-age=31536000";
            var contentType = "text/guid";
            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, Guid.NewGuid().ToString(), contentType);
            await storage.SetCacheControl(blobName);
            var returned = await storage.Properties(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(cache, returned.CacheControl);
            Assert.Equal(contentType, returned.ContentType);
        }

        [Fact]
        public async Task DontLoseCacheControl()
        {
            var cache = "public, max-age=31536000";
            var contentType = "text/guid";
            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, Guid.NewGuid().ToString(), contentType);
            await storage.SetCacheControl(blobName);
            await storage.Save(blobName, Guid.NewGuid().ToString(), contentType);
            var returned = await storage.Properties(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(cache, returned.CacheControl);
            Assert.Equal(contentType, returned.ContentType);
        }

        [Fact]
        public async Task SetCacheControlDefault()
        {
            var cache = "public, max-age=31536000";
            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, Guid.NewGuid().ToString());
            await storage.SetCacheControl(blobName);
            var returned = await storage.Properties(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(cache, returned.CacheControl);
        }

        [Fact]
        public async Task SetCacheControlZero()
        {
            var cache = "public, max-age=31536000";
            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, Guid.NewGuid().ToString());
            await storage.SetCacheControl(blobName, 0);
            var returned = await storage.Properties(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(cache, returned.CacheControl);
        }

        [Fact]
        public async Task SetCacheControl()
        {
            var cache = "public, max-age=1000";
            var blobName = Guid.NewGuid().ToString();
            var storage = new Container(ContainerName, ConnectionString);

            await storage.Save(blobName, Guid.NewGuid().ToString());
            await storage.SetCacheControl(blobName, 1000);
            var returned = await storage.Properties(blobName);

            //Assert.IsNotNull(returned);
            Assert.Equal(cache, returned.CacheControl);
        }

        [Fact]
        public async Task CopyToFrom()
        {
            var random = new Random();
            var bytes = new byte[16];
            random.NextBytes(bytes);

            var from = string.Format("{0}.bin", Guid.NewGuid());
            var to = string.Format("{0}.bin", Guid.NewGuid());
            var storage = new Container(ContainerName, ConnectionString);
            await storage.Save(from, bytes);

            var uri = await storage.Copy(from, to);

            //Assert.IsNotNull(uri);

            var exists = await storage.Exists(to);
            var data = await storage.Get(to);
            Assert.Equal(bytes, data);

            await storage.Delete(from);
            await storage.Delete(to);
        }

        [Fact]
        public async Task Copy()
        {
            var random = new Random();
            var bytes = new byte[16];
            random.NextBytes(bytes);

            var toContainerName = 'a' + Guid.NewGuid().ToString().Replace("-", string.Empty);
            var toContainer = new Container(toContainerName, ConnectionString);
            await toContainer.CreateIfNotExists();

            var from = string.Format("{0}.bin", Guid.NewGuid());
            var to = string.Format("{0}.bin", Guid.NewGuid());
            var storage = new Container(ContainerName, ConnectionString);
            await storage.Save(from, bytes);

            var uri = await storage.Copy(from, toContainer, to);

            //Assert.IsNotNull(uri);

            var exists = await toContainer.Exists(to);
            var data = await toContainer.Get(to);
            Assert.Equal(bytes, data);

            await storage.Delete(from);
            await toContainer.Delete(to);
            await toContainer.Delete();
        }

        [Fact]
        public async Task CopyContainerName()
        {
            var random = new Random();
            var bytes = new byte[16];
            random.NextBytes(bytes);

            var toContainerName = 'a' + Guid.NewGuid().ToString().Replace("-", string.Empty);
            var toContainer = new Container(toContainerName, ConnectionString);
            await toContainer.CreateIfNotExists();

            var from = string.Format("{0}.bin", Guid.NewGuid());
            var to = string.Format("{0}.bin", Guid.NewGuid());
            var storage = new Container(ContainerName, ConnectionString);
            await storage.Save(from, bytes);

            var uri = await storage.Copy(from, toContainerName, to);

            //Assert.IsNotNull(uri);

            var exists = await toContainer.Exists(to);
            var data = await toContainer.Get(to);
            Assert.Equal(bytes, data);

            await storage.Delete(from);
            await toContainer.Delete(to);
            await toContainer.Delete();
        }
    }
}
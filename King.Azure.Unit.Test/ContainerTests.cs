namespace King.Azure.Unit.Test.Data
{
    using King.Azure.Data;
    using Microsoft.WindowsAzure.Storage;

    using System;
    using Xunit;

    public class ContainerTests
    {
        private const string ConnectionString = "UseDevelopmentStorage=true;";

        [Fact]
        public void Constructor()
        {
            new Container("test", ConnectionString);
        }

        [Fact]
        public void IsIContainer()
        {
            //Assert.IsNotNull(new Container("test", ConnectionString) as IContainer);
        }

        [Fact]
        public void IsAzureStorage()
        {
            //Assert.IsNotNull(new Container("test", ConnectionString) as AzureStorage);
        }

        [Fact]
        public void ConstructorNameNull()
        {
            //Assert.That(() => new Container(null, ConnectionString), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorAccountNameNull()
        {
            //Assert.That(() => new Container(null, CloudStorageAccount.Parse(ConnectionString)), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ConstructorKeyNull()
        {
            //Assert.That(() => new Container("test", (string)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void DefaultCacheDuration()
        {
            //Assert.AreEqual(31536000, Container.DefaultCacheDuration);
        }

        [Fact]
        public void Name()
        {
            var name = Guid.NewGuid().ToString();
            var t = new Container(name, ConnectionString);
            //Assert.AreEqual(name, t.Name);
        }

        [Fact]
        public void IsPublic()
        {
            var name = Guid.NewGuid().ToString();
            var t = new Container(name, ConnectionString, true);
            //Assert.IsTrue(t.IsPublic);
        }

        [Fact]
        public void Client()
        {
            var name = Guid.NewGuid().ToString();
            var t = new Container(name, ConnectionString);
            //Assert.IsNotNull(t.Client);
        }

        [Fact]
        public void Reference()
        {
            var name = Guid.NewGuid().ToString();
            var t = new Container(name, ConnectionString);
            //Assert.IsNotNull(t.Reference);
        }

        [Fact]
        public void DeleteBlobNameNull()
        {
            var c = new Container("test", ConnectionString);
            //Assert.That(() => c.Delete(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void ExistsBlobNameNull()
        {
            var c = new Container("test", ConnectionString);
            //Assert.That(() => c.Exists(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void GetBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Get<object>(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void StreamBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Stream(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void SaveBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Save(null, new object()), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void SaveObjectNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Save(Guid.NewGuid().ToString(), (object)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void GetBytesBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Get(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void GetTextBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.GetText(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void SnapShotBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Snapshot(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void SaveBytesBlobNameNull()
        {
            var random = new Random();
            var bytes = new byte[1024];
            random.NextBytes(bytes);

            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Save(null, bytes), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void SaveTextBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Save(null, Guid.NewGuid().ToString()), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void SaveBytesNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Save(Guid.NewGuid().ToString(), (byte[])null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void SaveTextNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Save(Guid.NewGuid().ToString(), (string)null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void GetBlockReferenceBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.GetBlockReference(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void GetPageReferenceBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.GetPageReference(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void PropertiesBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Properties(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void SetCacheControlBlobNameNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.SetCacheControl(null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void CopyFromToFromNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Copy(null, Guid.NewGuid().ToString()), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void CopyFromToToNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Copy(Guid.NewGuid().ToString(), null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void CopyFromNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Copy(null, c, Guid.NewGuid().ToString()), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void CopyToNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Copy(Guid.NewGuid().ToString(), c, null), Throws.TypeOf<ArgumentException>());
        }

        [Fact]
        public void CopyTargetNull()
        {
            var c = new Container("test", ConnectionString);

            //Assert.That(() => c.Copy(Guid.NewGuid().ToString(), (IContainer)null, Guid.NewGuid().ToString()), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
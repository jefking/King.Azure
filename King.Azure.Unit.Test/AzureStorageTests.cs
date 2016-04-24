namespace King.Azure.Unit.Test.Data
{
    using System;
    using King.Azure.Data;
    using Microsoft.WindowsAzure.Storage;
    

    
    public class AzureStorageTests
    {
        const string ConnectionString = "UseDevelopmentStorage=true";

        [Fact]
        public void Constructor()
        {
            new AzureStorage(ConnectionString);
        }

        [Fact]
        public void IsIStorageAccount()
        {
            //Assert.IsNotNull(new AzureStorage(ConnectionString) as IStorageAccount);
        }

        [Fact]
        public void ConstructorConnectionStringNull()
        {
            //Assert.That(() => new AzureStorage((string)null), Throws.TypeOf<ArgumentNullException>());
        }

        [Fact]
        public void ConstructorAccountNull()
        {
            //Assert.That(() => new AzureStorage((CloudStorageAccount)null), Throws.TypeOf<ArgumentNullException>());
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;

namespace MaximStartsev.FunctionalSql.Test
{
    [TestClass]
    public class FuCommandTests
    {
        [TestMethod]
        public void NullConstructorArgumentError()
        {
            var connection = CreateConnection();
            Assert.ThrowsException<ArgumentNullException>(() => connection.CreateCommand(null));
        }
        [TestMethod]
        public void NotNullConstructorArgumentError()
        {
            var connection = CreateConnection();
            Assert.IsNotNull(connection.CreateCommand("SELECT * FROM [experiment1].[dbo].[table]"));
        }
        [TestMethod]
        public void Select()
        {
            var command = CreateCommand();
            Assert.IsNotNull(command.Select<object>());
        }
        [TestMethod]
        public void ExecuteScalar()
        {
            var data = CreateConnection().CreateCommand("SELECT TOP 1 Data FROM [experiments1].[dbo].[Table]").ExecuteScalar();
            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void ExecuteNonQuery()
        {
            CreateConnection().CreateCommand("UPDATE [experiments1].[dbo].[Table] SET Data = '3333' WHERE id = 1").ExecuteNonQuery();
        }
        private FuCommand CreateCommand()
        {
            var connection = CreateConnection();
            return connection.CreateCommand("SELECT * FROM [experiments1].[dbo].[Table]");
        }
        private FuConnection CreateConnection()
        {
            var connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Encrypt = False; TrustServerCertificate = True";
            return FuConnection.Create(new SqlConnection(connectionString));
        }
    }
}

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

        private FuCommand CreateCommand()
        {
            var connection = CreateConnection();
            return connection.CreateCommand("SELECT * FROM [experiment1].[dbo].[table]");
        }
        private FuConnection CreateConnection()
        {
            var connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = True; Persist Security Info = False; Pooling = False; MultipleActiveResultSets = False; Encrypt = False; TrustServerCertificate = True";
            return FuConnection.Create(new SqlConnection(connectionString));
        }
    }
}

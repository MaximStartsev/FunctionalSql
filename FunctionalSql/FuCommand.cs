using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximStartsev.FunctionalSql
{
    public class FuCommand:IDisposable
    {
        private IDbCommand _command;
        private FuConnection _connection;
        internal FuCommand(FuConnection connection, IDbCommand command)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
        public void ExecuteScalar()
        {
            _command.ExecuteScalar();
        }
        
        internal IDataReader ExecuteReader()
        {
            return _command.ExecuteReader();
        }
        private void Test()
        {
            var fuset = new FuSet<object>(this);
        }

        public FuSet<T> Select<T>() where T: new()
        {
            return new FuSet<T>(this);
        }

        public void Dispose()
        {
            _command.Dispose();
        }
      
    }
}

using System;
using System.Data;

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
        public object ExecuteScalar()
        {
            try
            {
                _connection.Open();
                return _command.ExecuteScalar();
            }
            finally
            {
                _connection.Close();
            }
        }
        
        public void ExecuteNonQuery()
        {
            try
            {
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }

        internal IDataReader ExecuteReader()
        {
            return _command.ExecuteReader();
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

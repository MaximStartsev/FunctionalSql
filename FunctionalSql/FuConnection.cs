using System;
using System.Collections.Generic;
using System.Data;

namespace MaximStartsev.FunctionalSql
{
    public sealed class FuConnection:IDisposable
    {
        private readonly IDbConnection _connection;
        public bool IsOpened { get; private set; }
        private FuConnection(IDbConnection connection)
        {
            _connection = connection;
        }
        internal void Open()
        {
            if (IsOpened) return;
            _connection.Open();
            IsOpened = true;
        }
        public FuCommand CreateCommand(string sql, Dictionary<string, object> parameters = null)
        {
            if (String.IsNullOrEmpty(sql)) throw new ArgumentNullException(nameof(sql));
            var command = _connection.CreateCommand();
            command.CommandText = sql;
            command.Connection = _connection;
            if(parameters != null)
            {
                foreach (var pair in parameters)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = pair.Key;
                    parameter.Value = pair.Value;
                    command.Parameters.Add(parameter);
                }
            }
            return new FuCommand(this, command);
        }
        public static FuConnection Create(IDbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            return new FuConnection(connection);
        }
        internal void Close()
        {
            _connection.Close();
            IsOpened = false;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}

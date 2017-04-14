using System;
using System.Data;

namespace MaximStartsev.FunctionalSql
{
    public sealed class FuConnection
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
        public FuCommand CreateCommand(string sql)
        {
            if (String.IsNullOrEmpty(sql)) throw new ArgumentNullException(nameof(sql));
            var command = _connection.CreateCommand();
            command.CommandText = sql;
            command.Connection = _connection;
            return new FuCommand(this, command);
        }
        public static FuConnection Create(IDbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            return new FuConnection(connection);
        }
    }
}

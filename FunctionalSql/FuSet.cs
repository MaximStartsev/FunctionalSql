using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MaximStartsev.FunctionalSql
{
    public class FuSet<T> : IEnumerable<T> where T: new()
    {
        private readonly FuCommand _command;
        internal FuSet(FuCommand command)
        {
            _command = command;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FuSetEnumerator<T>(_command);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FuSetEnumerator<T>(_command);
        }

        public sealed class FuSetEnumerator<K> : IEnumerator<K> where K: new()
        {
            private readonly IDataReader _reader;
            private readonly FuCommand _command;
            /// <summary>
            /// сопоставление номеров столбцов и свойств типа
            /// </summary>
            private readonly IDictionary<int, PropertyInfo> _columns;
            
            internal FuSetEnumerator(FuCommand command)
            {
                if (command == null) throw new ArgumentNullException(nameof(command));
                _reader = command.ExecuteReader();
                _columns = GenerateColumns(_reader);
            }

            public K Current => Generate();

            object IEnumerator.Current => Generate();

            public void Dispose()
            {
                _reader.Dispose();
            }

            public bool MoveNext()
            {
                return _reader.Read();
            }
            /// <summary>
            /// Not implemented
            /// </summary>
            public void Reset()
            {
                throw new NotImplementedException();
            }

            private K Generate()
            {
                var k = new K();
                foreach (var column in _columns)
                {
                    column.Value.SetValue(k, _reader.GetValue(column.Key));
                }
                return k;
            }
          
            private static IDictionary<int, PropertyInfo> GenerateColumns(IDataReader reader)
            {
                var properties = typeof(K).GetProperties();
                var columns = new Dictionary<int, string>();
                var result = new Dictionary<int, PropertyInfo>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    var property = properties.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
                    if (property != null)
                    {
                        result.Add(i, property);
                    }
                }
                return result;
            }
        }
    }
}

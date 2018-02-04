using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using MusicIdentificationSystem.DAL.Context;
using MusicIdentificationSystem.DAL.DatabaseConfiguration;

namespace MusicIdentificationSystem.DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository where T : class
    {
        public Func<DatabaseContext> ResolveDbContext { get; private set; }

        public Func<IDatabaseConfigurationManager> GetDatabaseConfiguration { get; private set; }

        public IEnumerable<U> ExecuteProcedure<U>(string procedureName, params SqlParameter[] parameters)
        {
            using (var context = this.ResolveDbContext())
            {
                var sqlParameters = new List<object>();
                if (parameters != null && parameters.Any())
                {
                    procedureName += " " + string.Join(
                        ",",
                        parameters.Select(x => string.Format("{0} = {0}", x.ParameterName)));
                    sqlParameters.AddRange(parameters);
                }
                return context.Database.SqlQuery<U>(procedureName, sqlParameters.ToArray()).ToList();
            }

        }

        public List<Dictionary<string, object>> ExecuteProcedure(string procedureName, params object[] parameters)
        {
            using (var context = this.ResolveDbContext())
            {
                var result = new List<Dictionary<string, object>>();
                context.Database.Connection.Open();
                using (var cmd = context.Database.Connection.CreateCommand())
                {
                    cmd.CommandText = procedureName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = this.GetDatabaseConfiguration().DbConnectionTimeout;
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader[i];
                            }
                            result.Add(row);
                        }
                    }
                }

                return result;
            }
        }

        public void InitializeContext(Func<DatabaseContext> databaseContextSetter)
        {
            this.ResolveDbContext = databaseContextSetter;
            //this.GetDatabaseConfiguration = databaseConfigurationSetter;
        }

        public IEnumerable<U> GetList<U>(Expression<Func<T, bool>> predicate = null)
        {
            using (var context = this.ResolveDbContext())
            {
                var set = context.Set<T>();
                if (predicate != null)
                {
                    return set.Where(predicate).ToList().Select(Mapper.Map<T, U>).ToList();
                }
                return set.ToList().Select(Mapper.Map<T, U>).ToList();
            }
        }

        public static DataTable ToDataTable<TTable>(IEnumerable<TTable> items)
        {
            var dataTable = new DataTable(typeof(TTable).Name);

            var properties = typeof(TTable).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var type = prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType;

                dataTable.Columns.Add(prop.Name, type ?? throw new InvalidOperationException());

            }

            foreach (var item in items)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
    }
}

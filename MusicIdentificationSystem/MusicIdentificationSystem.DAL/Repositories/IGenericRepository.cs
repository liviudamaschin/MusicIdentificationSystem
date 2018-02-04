using MusicIdentificationSystem.DAL.Context;
using MusicIdentificationSystem.DAL.DatabaseConfiguration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MusicIdentificationSystem.DAL.Repositories
{
    public interface IGenericRepository
    {
        Func<DatabaseContext> ResolveDbContext { get; }

        void InitializeContext(Func<DatabaseContext> databaseContextSetter);

        IEnumerable<U> ExecuteProcedure<U>(string procedureName, params SqlParameter[] parameters);

        List<Dictionary<string, object>> ExecuteProcedure(string procedureName, params object[] parameters);
    }
}

using MusicIdentificationSystem.DAL.Context;
using MusicIdentificationSystem.DAL.DatabaseConfiguration;
using MusicIdentificationSystem.DAL.Repositories;
using StructureMap;
namespace MusicIdentificationSystem.DAL.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class, IGenericRepository
    {
        private readonly IContainer container;

        public UnitOfWork(IContainer container, T repository)
        {
            this.container = container;
            this.Repository = repository;

            this.Repository.InitializeContext(this.SetContext, this.SetDatabaseConfigurationManager);
        }

        public T Repository { get; set; }

        public DatabaseContext SetContext()
        {
            return this.container.GetInstance<DatabaseContext>();
        }

        private IDatabaseConfigurationManager SetDatabaseConfigurationManager()
        {
            return container.GetInstance<IDatabaseConfigurationManager>();
        }
    }
}

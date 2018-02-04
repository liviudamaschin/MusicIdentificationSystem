
namespace MusicIdentificationSystem.DAL.UnitOfWork
{
    public interface IUnitOfWork<T>
    {
        T Repository { get; set; }
    }
}

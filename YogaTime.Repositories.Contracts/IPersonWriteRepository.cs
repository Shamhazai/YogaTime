using YogaTime.Context.Contracts.Models;

namespace YogaTime.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Person"/>
    /// </summary>
    public interface IPersonWriteRepository : IRepositoryWriter<Person>
    {
    }
}

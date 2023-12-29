﻿using Microsoft.EntityFrameworkCore;
using Serilog;
using YogaTime.Common.Entity.InterfaceDB;
using YogaTime.Common.Entity.Repositories;
using YogaTime.Context.Contracts.Models;
using YogaTime.Repositories.Contracts;

namespace YogaTime.Repositories.Implementations
{
    public class PersonReadRepository : IPersonReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public PersonReadRepository(IDbRead reader)
        {
            this.reader = reader;
            Log.Information("Инициализирован абстракция IDbReader в классе PersonReadRepository");
        }

        Task<IReadOnlyCollection<Person>> IPersonReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Person>()
                .NotDeletedAt()
                .OrderBy(x => x.LastName)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Person?> IPersonReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Person>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Person>> IPersonReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Person>()
                .NotDeletedAt()
                .ByIds(ids)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.Patronymic)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<IReadOnlyCollection<Person>> IPersonReadRepository.GetAllByGroupIdAsync(Guid groupId, CancellationToken cancellationToken)
            => reader.Read<Person>()
                .NotDeletedAt()
                .Where(x => x.GroupId == groupId)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ThenBy(x => x.Patronymic)
                .ToReadOnlyCollectionAsync(cancellationToken);
    }
}

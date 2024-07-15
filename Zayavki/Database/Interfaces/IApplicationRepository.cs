using System.Runtime.CompilerServices;

namespace Zayavki;

public interface IApplicationRepository
{
    public List<ApplicationDbEntity> Get();

    public Task<ApplicationDbEntity> GetAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<ApplicationDbEntity> CreateAsync(ApplicationDbEntity application, CancellationToken cancellationToken = default);

    public void Update(Guid id, ApplicationDbEntity applicationIn);

    public void Remove(ApplicationDbEntity applicationIn);

    public void Remove(Guid id);
}

using CED.Domain.Repository;
using CED.Infrastructure.Entity_Framework_Core;
using Microsoft.Extensions.Logging;

namespace CED.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ILogger<UnitOfWork> _logger;
    private readonly CEDDBContext _ceddbContext;
    public UnitOfWork(ILogger<UnitOfWork> logger,CEDDBContext ceddbContext )
    {
        _logger = logger;
        _ceddbContext = ceddbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("On save changes...");
        return await _ceddbContext.SaveChangesAsync(cancellationToken);
    }
}
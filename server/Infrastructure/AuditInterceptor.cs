using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using server.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Infrastructure.Interceptors
{
    public class AuditInterceptor : ISaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if (dbContext == null) return new ValueTask<InterceptionResult<int>>(result);

            var entries = dbContext.ChangeTracker.Entries()
                .Where(e => e.Entity is AuditBaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (AuditBaseEntity)entry.Entity;
                var now = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = GetCurrentUserId() ?? 0;
                    entity.isDeleted = false;
                }

                if (entry.State == EntityState.Modified)
                {
                    entity.ModifiedDate = now;
                    entity.ModifiedBy = GetCurrentUserId() ?? 0;
                }
            }

            return new ValueTask<InterceptionResult<int>>(result);
        }

        private int? GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? int.Parse(userId) : (int?)null;
        }

        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result) => result;
        public ValueTask<InterceptionResult<int>> SavedChangesAsync(SaveChangesCompletedEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default) => new ValueTask<InterceptionResult<int>>(result);
        public void SavedChanges(SaveChangesCompletedEventData eventData) { }
        public void SaveChangesFailed(DbContextErrorEventData eventData) { }
    }
}
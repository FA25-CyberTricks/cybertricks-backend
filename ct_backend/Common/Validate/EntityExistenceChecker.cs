using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ct_backend.Common.Validate
{
    public static class EntityExistenceChecker
    {
        /// <summary>
        /// Trả về entity đầu tiên thỏa mãn điều kiện, hoặc null nếu không có
        /// </summary>
        public static async Task<TEntity?> GetIfExistsAsync<TEntity>(
            DbContext context,
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken ct = default
        ) where TEntity : class
        {
            return await context.Set<TEntity>()
                .FirstOrDefaultAsync(predicate, ct);
        }

        /// <summary>
        /// Check tồn tại đơn giản (chỉ true/false)
        /// </summary>
        public static async Task<bool> ExistsAsync<TEntity>(
            DbContext context,
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken ct = default
        ) where TEntity : class
        {
            return await context.Set<TEntity>()
                .AnyAsync(predicate, ct);
        }
    }
}

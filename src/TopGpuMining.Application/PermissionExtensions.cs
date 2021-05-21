using TopGpuMining.Core;
using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Extensions;
using TopGpuMining.Core.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TopGpuMining.Application
{
    public static class PermissionExtensions
    {
        public static void ApplyFilterBasedOnRole<TEntity>(this SearchCriteria<TEntity> search) where TEntity : AuditableEntity
        {
            if (search.FilterExpression == null)
                search.FilterExpression = a => true;

            if (Thread.CurrentPrincipal.IsInRole(AppRoles.ADMIN_ROLE))
                return;

            var userId = Thread.CurrentPrincipal.GetUserId();

            if (Thread.CurrentPrincipal.IsInRole(AppRoles.ADMIN_ROLE))
                search.FilterExpression = search.FilterExpression.And(a => a.CreatedByUserId == userId);
        }
    }
}

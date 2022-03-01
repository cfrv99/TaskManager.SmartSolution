using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Responses;

namespace TaskManager.Core.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (condition)
                query = query.Where(predicate);

            return query;
        }

        public static IQueryable<T> PagedBy<T>(this IQueryable<T> query, IPaged input)
        {
            return query.Skip((input.CurrentPage - 1) * input.Limit).Take(input.Limit);
        }

        public static IOrderedQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, ISorting input, bool desc = false)
        {
            IOrderedQueryable<T> result = null;

            result = desc ? query.OrderByDescending(i => i.GetType().GetProperty(input.Sorting)) : query.OrderBy(i => i.GetType().GetProperty(input.Sorting));

            return result;
        }
    }
}

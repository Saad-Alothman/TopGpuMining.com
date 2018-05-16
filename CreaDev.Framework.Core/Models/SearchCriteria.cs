using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Core.Models
{
    public class SearchCriteria<T> : SearchCriteriaBase
    {

        public Expression<Func<T, bool>> FilterExpression { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> SortExpression { get; set; }

        public bool ExcludeDeleted { get; set; } = true;


        public SearchCriteria()
        {
            this.PageSize = 20;
            this.PageNumber = 1;
            this.FilterExpression = arg => true;
        }
        public SearchCriteria(Expression<Func<T, bool>> filterExpression)
        {
            this.PageSize = 20;
            this.PageNumber = 1;
            this.FilterExpression = filterExpression;
        }
        public SearchCriteria(int? pageNumber)
        {
            this.PageNumber = pageNumber ?? 1;
            this.PageSize = 20;
            this.FilterExpression = arg => true;
        }
        public SearchCriteria(int pageSize, int pageNumber)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
            this.FilterExpression = arg => true;
        }
        public SearchCriteria(int pageSize, int pageNumber, Func<IQueryable<T>, IOrderedQueryable<T>> sortExpression, Expression<Func<T, bool>> filterExpression) : this(pageSize, pageNumber)
        {
            this.FilterExpression = filterExpression;
            this.SortExpression = sortExpression;
        }

    }

    public class AccountSearchCriteria<T> where T : class
    {

        public Expression<Func<T, bool>> FilterExpression { get; set; }

        public Func<IQueryable<T>, IOrderedQueryable<T>> SortExpression { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int StartIndex { get { return PageSize * (PageNumber - 1); } }

        public AccountSearchCriteria()
        {
            this.PageSize = 20;
            this.PageNumber = 1;
        }

        public AccountSearchCriteria(int? pageNumber)
        {
            this.PageSize = 20;
            this.PageNumber = pageNumber ?? 1;
        }

        public AccountSearchCriteria(int pageSize, int pageNumber)
        {
            this.PageSize = pageSize;
            this.PageNumber = pageNumber;
        }

        public AccountSearchCriteria(int pageSize, int pageNumber, Func<IQueryable<T>, IOrderedQueryable<T>> sortExpression, Expression<Func<T, bool>> filterExpression) : this(pageSize, pageNumber)
        {
            this.FilterExpression = filterExpression;
            this.SortExpression = sortExpression;
        }

    }
}

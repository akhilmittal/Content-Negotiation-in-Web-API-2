using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DataModel
{
    public class ProductActionResult<T> where T : class
    {
        public T Entity { get; private set; }
        public ProductActionStatus Status { get; private set; }

        public Exception Exception { get; private set; }


        public ProductActionResult(T entity, ProductActionStatus status)
        {
            Entity = entity;
            Status = status;
        }

        public ProductActionResult(T entity, ProductActionStatus status, Exception exception) : this(entity, status)
        {
            Exception = exception;
        }

    }
}

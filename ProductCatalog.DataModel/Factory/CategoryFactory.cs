using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DataModel.Factory
{
   public class CategoryFactory
    {
        public CategoryFactory()
        {

        }

        public Entities.Category CreateCategory(Category Category)
        {
            return new Entities.Category()
            {
                CategoryId = Category.CategoryId,
                CategoryName = Category.CategoryName,
            };
        }



        public Category CreateCategory(Entities.Category Category)
        {
            return new Category()
            {
                CategoryId = Category.CategoryId,
                CategoryName = Category.CategoryName,
            };
        }
    }
}

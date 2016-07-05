using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DataModel
{
    public interface IProductCatalogRepository
    {
        ProductActionResult<Product> DeleteProduct(int id);
        ProductActionResult<ProductCatalog.DataModel.Category> DeleteCategory(int id);
        ProductCatalog.DataModel.Product GetProduct(int id);
        ProductCatalog.DataModel.Category GetCategory(int id);
        System.Linq.IQueryable<ProductCatalog.DataModel.Category> GetCategories();
        System.Linq.IQueryable<ProductCatalog.DataModel.Product> GetProducts();
        System.Linq.IQueryable<ProductCatalog.DataModel.Product> GetProducts(int CategoryId);
        ProductActionResult<ProductCatalog.DataModel.Product> InsertProduct(ProductCatalog.DataModel.Product product);
        ProductActionResult<ProductCatalog.DataModel.Category> InsertCategory(ProductCatalog.DataModel.Category category);
        ProductActionResult<ProductCatalog.DataModel.Product> UpdateProduct(ProductCatalog.DataModel.Product product);
        ProductActionResult<ProductCatalog.DataModel.Category> UpdateCategory(ProductCatalog.DataModel.Category category);
    }
}

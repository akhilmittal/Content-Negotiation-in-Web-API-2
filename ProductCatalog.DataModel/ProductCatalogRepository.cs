using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DataModel
{
    public class ProductCatalogRepository : IProductCatalogRepository
    {

        ProductCatalogEntities _ctx;
        public ProductCatalogRepository(ProductCatalogEntities ctx)
        {
            _ctx = ctx;

            // Disable lazy loading - if not, related properties are auto-loaded when
            // they are accessed for the first time, which means they'll be included when
            // we serialize (b/c the serialization process accesses those properties).  
            // 
            // We don't want that, so we turn it off.  We want to eagerly load them (using Include)
            // manually.

            _ctx.Configuration.LazyLoadingEnabled = false;

        }
        public ProductActionResult<Product> DeleteProduct(int id) {
            try
            {
                var exp = _ctx.Products.Where(e => e.ProductId == id).FirstOrDefault();
                if (exp != null)
                {
                    _ctx.Products.Remove(exp);
                    _ctx.SaveChanges();
                    return new ProductActionResult<Product>(null, ProductActionStatus.Deleted);
                }
                return new ProductActionResult<Product>(null, ProductActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new ProductActionResult<Product>(null, ProductActionStatus.Error, ex);
            }
        }
        public ProductActionResult<Category> DeleteCategory(int id)
        {
            try
            {

                var eg = _ctx.Categories.Where(e => e.CategoryId == id).FirstOrDefault();
                if (eg != null)
                {
                    // also remove all products linked to this Category

                    _ctx.Categories.Remove(eg);

                    _ctx.SaveChanges();
                    return new ProductActionResult<Category>(null, ProductActionStatus.Deleted);
                }
                return new ProductActionResult<Category>(null, ProductActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new ProductActionResult<Category>(null, ProductActionStatus.Error, ex);
            }
        }
        public Product GetProduct(int id) {
            return _ctx.Products.FirstOrDefault(e => e.ProductId == id);
        }
        public Category GetCategory(int id) { return _ctx.Categories.FirstOrDefault(eg => eg.CategoryId == id); }
        public System.Linq.IQueryable<Category> GetCategories() { return _ctx.Categories; }
        public System.Linq.IQueryable<Product> GetProducts() { return _ctx.Products; }
        public System.Linq.IQueryable<Product> GetProducts(int CategoryId) { return _ctx.Products.Where(eg => eg.Category == CategoryId); }
        public ProductActionResult<Product> InsertProduct(Product product)
        {
            try
            {
                _ctx.Products.Add(product);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new ProductActionResult<Product>(product, ProductActionStatus.Created);
                }
                else
                {
                    return new ProductActionResult<Product>(product, ProductActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new ProductActionResult<Product>(null, ProductActionStatus.Error, ex);
            }
        }
        public ProductActionResult<Category> InsertCategory(Category category) {
            try
            {
                _ctx.Categories.Add(category);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new ProductActionResult<Category>(category, ProductActionStatus.Created);
                }
                else
                {
                    return new ProductActionResult<Category>(category, ProductActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new ProductActionResult<Category>(null, ProductActionStatus.Error, ex);
            }
        }
        public ProductActionResult<Product> UpdateProduct(Product product)
        {
            try
            {

                // you can only update when an Product already exists for this id

                var existingProduct = _ctx.Products.FirstOrDefault(exp => exp.ProductId == product.ProductId);

                if (existingProduct == null)
                {
                    return new ProductActionResult<Product>(product, ProductActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingProduct).State = EntityState.Detached;

                // attach & save
                _ctx.Products.Attach(product);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(product).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new ProductActionResult<Product>(product, ProductActionStatus.Updated);
                }
                else
                {
                    return new ProductActionResult<Product>(product, ProductActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new ProductActionResult<Product>(null, ProductActionStatus.Error, ex);
            }
        }
        public ProductActionResult<Category> UpdateCategory(Category category)
        {
            try
            {

                // you can only update when an Category already exists for this id

                var existingCat = _ctx.Categories.FirstOrDefault(exg => exg.CategoryId == category.CategoryId);

                if (existingCat == null)
                {
                    return new ProductActionResult<Category>(category, ProductActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingCat).State = EntityState.Detached;

                // attach & save
                _ctx.Categories.Attach(category);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(category).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new ProductActionResult<Category>(category, ProductActionStatus.Updated);
                }
                else
                {
                    return new ProductActionResult<Category>(category, ProductActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new ProductActionResult<Category>(null, ProductActionStatus.Error, ex);
            }
        }
    }
}

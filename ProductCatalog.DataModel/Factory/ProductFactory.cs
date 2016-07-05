namespace ProductCatalog.DataModel.Factory
{
    public class ProductFactory
    {

        public ProductFactory()
        {

        }

        public Entities.Product CreateProduct(Product product)
        {
            return new Entities.Product()
            {
               ProductId=product.ProductId,
               ProductName=product.ProductName,
               Category=product.Category
            };
        }



        public Product CreateProduct(Entities.Product product)
        {
            return new Product()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Category = product.Category
            };
        }

    }
}

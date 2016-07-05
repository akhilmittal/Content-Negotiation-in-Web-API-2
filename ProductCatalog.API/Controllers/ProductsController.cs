using ProductCatalog.DataModel;
using ProductCatalog.DataModel.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductCatalog.API.Controllers
{
    public class ProductsController : ApiController
    {
        IProductCatalogRepository _productCatalogRepository;
        ProductFactory _productFactory = new ProductFactory();
        public ProductsController()
        {
            _productCatalogRepository = new ProductCatalogRepository(new ProductCatalogEntities());
        }
        public ProductsController(IProductCatalogRepository productCatalogRepository)
        {
            _productCatalogRepository = productCatalogRepository;
        }
        public IHttpActionResult Get()
        {
            try
            {
                var products = _productCatalogRepository.GetProducts();
                return Ok(products.ToList().Select(p => _productFactory.CreateProduct(p)));
            }
            catch (Exception)
            {

                return InternalServerError();
            }

            #region Default Content Negotiator
            //try
            //{
            //    var products = _productCatalogRepository.GetProducts();
            //    var customProducts = products.ToList().Select(p => _productFactory.CreateProduct(p));
            //    IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();

            //    ContentNegotiationResult result = negotiator.Negotiate(
            //        typeof(List<Product>), this.Request, this.Configuration.Formatters);
            //    if (result == null)
            //    {
            //        var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            //        throw new HttpResponseException(response);
            //    }

            //    var bestFormatter = result.Formatter;
            //    var bestMediaType = result.MediaType.MediaType;

            //    return new HttpResponseMessage()
            //    {
            //        Content = new ObjectContent<List<Entities.Product>>(
            //            customProducts.ToList(),                   // type of object to be serialized 
            //            result.Formatter,           // The media formatter
            //            result.MediaType.MediaType  // The MIME type
            //        )
            //    };

            //}
            //catch (Exception)
            //{

            //    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            //} 
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;


namespace DINGSOMETHING.Models.BusinessAccess
{
    public interface IProductRepository
    {
        int Create(Product product);
        
        Product GetById(string id);

        IEnumerable<Product> Get();

        IEnumerable<ProductCatalog> GetCatalog();

        int Update(Product product);

        
    }

}
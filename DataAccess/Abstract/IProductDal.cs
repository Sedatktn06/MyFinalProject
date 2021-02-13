using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //Product Katmanından bana baya bir ürün gelecek.
    public interface IProductDal:IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails();
        
    }
}

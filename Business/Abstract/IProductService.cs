using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    //IResult void olanlarda kullanırız.
    //Bir şey döndüreceğimiz zaman IDataResult kullanacağız.
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IResult Add(Product product);
        IResult Update(Product product);
        IDataResult<Product> GetById(int productId);
        //Transactional Yönetimi=>Uygulamalarda tutarlılığı korumak için yaptığımız bir yöntem.
        IResult AddTransactionalTest(Product product);
    }
}

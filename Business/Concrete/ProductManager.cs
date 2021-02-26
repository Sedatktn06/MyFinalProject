using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
   
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productDal = productDal;
        }



        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //Eğer mevcut kategori sayısı 15'i geçtiyse sisteme yeni ürün eklenemez...
             IResult result=BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                 CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckCategoryCounts());
            if (result!=null)
            {
                return result;
            }
             _productDal.Add(product);
             return new SuccessResult(Messages.ProductAdded);
        }


        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==05)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }



        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            //Buraya bir lambda verecez.Yani filtreleyeceğiz burada.Fonksiyonu kategori ile belirlediğimiz için verilen
            //kategori id sine göre filtreler.
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id));
        }



        public IDataResult<Product> GetById(int productId)
        {
            return  new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==productId));
        }



        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return  new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }



        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return  new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult();
        }
        

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId);
            if (result.Count >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }


        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }
        private IResult CheckCategoryCounts()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimetExceded);
            }
            return new SuccessResult();
        }

    }
}

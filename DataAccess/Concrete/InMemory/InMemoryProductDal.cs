using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        //Sanki bellekte data varda biz onunla çalışacağız.Şimdilik bunu kullanıyoruz.
        //Alttre kullanmamızın sebebi global değişleken olması.
        List<Product> _products;
        public InMemoryProductDal()
        {
            //Sanki Sql,Oracle veritabanından geliyormuş gibi simüle ediyoruz.
            _products = new List<Product> 
            {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1}
            };
        }
        public void Add(Product product)
        {
            //Kullanıcı arayüzünden gelen ürünü veritabanıma ekliyorum.
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //Bu şekilde silemeyiz.Çünkü bunlar referans tip.Herhangi bir ürünü sildiğimizde onun bellekteki yerini silmiş
            //olmayız.Bu yüzden bu kullanım yanlış.
            //_products.Remove(product);

            //2. Kulanım LINQ Kullanmadan
            /*
            Product productToDelete=null;
            foreach (var p in _products)
            {
                if (product.ProductId==p.ProductId)
                {
                    productToDelete = p;
                }
            }
            _products.Remove(productToDelete);
            */
            //Lambda =>
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);
            _products.Remove(productToDelete);


        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            //Kategori id'sine göre bütün elamanları gezer ve eşit olan id ye göre o id ye sahip bütün ürünleri getirir.
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün id'sine sahip olan listedeki ürünü bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;

        }
    }
}

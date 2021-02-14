using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Normalde new yapımda(parantez içinde olan new yapısında)InMemoryProductDal vardı.Ben EfProductDal kullanmak
            //istediğimde InMemoryProductDal'ı silip yerine EfProductDal yazdım.Tek değişiklikle işlemimi yaptım.
            //Bu yaptığımız hareket SOLID'ın O harfi.
            //O:Open Closed Principle=Yaptığın yazılıma yeni bir özellik ekliyorsan,mevcuttaki hiçbir koduna dokunmamalısın.
            //DTO=Data Transformation Access
            //ProductTest();
            //CategoryTest();
            ProductTestWithCategory();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        /* -------------------------------------------------*/

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var product in productManager.GetByUnitPrice(50, 150).Data)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        /* -------------------------------------------------*/

        private static void ProductTestWithCategory()
        {
            //Kategori ismi ile ürün ismini aynı anda getirebildim.
            ProductManager productManager = new ProductManager(new EfProductDal());
            var result = productManager.GetProductDetails();
            if (result.Success)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
    }
}

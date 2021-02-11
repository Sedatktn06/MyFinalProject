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

            ProductManager productManager = new ProductManager(new EfProductDal());
            foreach (var product in productManager.GetByUnitPrice(50,150))
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}

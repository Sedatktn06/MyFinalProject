using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //IDataResult mesaj ve işlem sonucunu tutacak.Aynı zamanda veride içerecek.Mesaj ve işlem sonucu içeren bir class var zaten.O yüzden
    //IResult kullandım.
    public interface IDataResult<T>:IResult
    {
        T Data { get; }//Ürün veya ürünler
    }
}

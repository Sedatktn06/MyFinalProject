using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //Getterlar constructorda set edilebilir.
        public Result(bool success,string message):this(success)//Aynı zamanda tek parametreli olan constructor'ı da çalıştır.
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success; 
        }


        public bool Success { get; }

        public string Message { get; }
    }
}

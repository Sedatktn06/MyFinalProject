using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    //duration=>cache'de ne kadar süre kalacak
    public interface ICacheManager
    {
        //Generic Metot
        T Get<T>(string key);
        //Diğer yöntem
        Object Get(string key);
        void Add(string key,object value,int duration);
        //Cache'de var mı kontrolü.
        bool IsAdd(string key);
        //Cache'den uçurma
        void Remove(string key);
        //Başı sonu önemli değil başında Get olanlar gibi.
        void RemoveByPattern(string pattern);
    }
}

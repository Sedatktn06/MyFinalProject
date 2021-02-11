using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    //Generic constraint:Generic kısıt demek.Yani T yerine göndereceğimiz nesneyi kısıtlarız.Burada sadece Entitiesdeki nesnelerimin gelmesini istiyorum.
    //class:Sadece referans tip gönderiyorum demek.
    //Yani T hem referans tip olacak hemde IEntity olacak veya IEntity implemente eden bir nesne olabilir.
    //new():new'lenebilir olmalı.Yani IEntity olmaz.IEntity implemente eden bir nesne olmalı.

    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        
    }
}

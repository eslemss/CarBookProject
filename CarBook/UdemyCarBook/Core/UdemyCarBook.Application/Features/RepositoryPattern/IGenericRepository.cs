using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyCarBook.Application.RepositoryPattern
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        void Create(T entity);  
        void Update(T entity);  
        void Remove(T entity);  
        T GetById(int id);
        List<T> GetCommentByBlogId(int id);
        public int GetCountCommentByBlog(int id);
    }
}

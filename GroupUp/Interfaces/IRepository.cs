using System.Collections.Generic;

namespace GroupUp.Interfaces
{
  public interface IRepository<T, Tid>
  {
    List<T> GetAll();
    T GetById(Tid id);
    T Create(T item);
    void Update(T item);
    void Delete(Tid id);
  }
}
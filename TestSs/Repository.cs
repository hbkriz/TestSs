using System;
using System.Collections.Generic;

namespace TestSs
{
   public class Repository<T> : IRepository<T> where T : IStoreable
   {
        private List<T> entities;
        public Repository()
        {
        entities = new List<T>();
        }

        public IEnumerable<T> All()
        {
            return entities;
        }

        public void Save(T item)
        {
            Delete(item.Id);
            entities.Add(item);
        }

        public T FindById(IComparable id)
        {
            return entities.Find(MatchedId(id));
        }

        private Predicate<T> MatchedId(IComparable id)
        {
            return match => match.Id.Equals(id);
        }

        public void Delete(IComparable id)
        {
            var entityToDelete = FindById(id);
            entities.Remove(entityToDelete);
        }
    }
}
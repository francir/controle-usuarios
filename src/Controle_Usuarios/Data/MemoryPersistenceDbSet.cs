using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Controle_Usuarios.Data
{
    public class MemoryPersistenceDbSet<T> : IDbSet<T> where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public MemoryPersistenceDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }
        public virtual T Find(params object[] keyValues)
        {
            if (!(typeof(T) is IEntity)) { throw new ArgumentException(string.Format("Entity [{0}] does not contain a property [Id], so it could not be converted to the IEntity interface, used in this function.", typeof(T).ToString())); }
            return this.SingleOrDefault(e => (e as IEntity).id == (Guid)keyValues.Single());
        }
        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }
        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }
        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }
        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }
        public T Create()
        {
            return Activator.CreateInstance<T>();
        }
        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }
        public ObservableCollection<T> Local
        {
            get { return _data; }
        }
        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }
        Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }
        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }

}
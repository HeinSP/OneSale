using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneSale.Elements
{
    public abstract class Element
    {
        public Element(SqlDataReader reader) { }
        public Element() { }
        public abstract T Load<T>(SqlDataReader reader) where T : Element;
        protected string caption;
        protected long num;

        public string Caption { get => caption; set => caption = value; }
        public long Num { get => num; set => num = value; }
    }
    public interface ILoadable<T>
    {
        T Load<T>();
    }
    public interface IRefreshable
    {
        void Refresh(SqlDataReader reader);
    }
    class ObservableElements<T> : ObservableCollection<T>, IRefreshable where T : Element, new()
    {
        public void Refresh(SqlDataReader reader)
        {
            this.Clear();
            while (reader.Read())
            {
                try
                {
                    T item = new T();
                    this.Add(item.Load<T>(reader) as T);
                }
                catch
                {

                }
            }
        }
        public T Find(int num)
        {
            return this.First(check => check.Num == num);
        }
        public T this[long num]
        {
            get { return this.First(check => check.Num == num); }
        }
        public T this[string caption]
        {
            get { return this.First(check => check.Caption == caption); }
        }
    }
}

namespace CreaDev.Framework.Core.Models
{
    public class SingletonBase<TType> where TType : class,new()
    {
        protected static TType instance;

        public static TType Instance
        {
            get { return (instance ?? (instance = new TType())); }
        }

        protected SingletonBase()
        {
          
        }

        static SingletonBase()
        {
        }
    }
}
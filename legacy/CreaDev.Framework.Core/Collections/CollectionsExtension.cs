using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CreaDev.Framework.Core.Collections
{
   public static class CollectionsExtension
    {
     
        public static IOrderedQueryable<T> OrderByDynamic<T>(IQueryable<T> source, string property, bool isDescending)
        {
            return ApplyOrder<T>(source, property, isDescending ? "OrderByDescending" : "OrderBy");
        }
        private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }


        public static List<T> Random<T>(this List<T> theList,int count)
        {
            if (theList == null || theList.Count < 1 || theList.Count <= count)
            {
                return theList;
            }
            List<int> randoms =NumberUtils.GetRandomInts(count, theList.Count, 0);
            List<T> SelectedItems = new List<T>();
            foreach (var i in randoms)
            {
                SelectedItems.Add(theList[i]);
            }
            return SelectedItems;
        }

       public static T Random<T>(this List<T> theList)
       {
           return theList.Random(1).FirstOrDefault();
       }
    }
    public static class NumberUtils
    {
        public static List<int> GetRandomInts(int count, int upperLimit, int LowerLimit = 1)
        {
            List<int> randoms = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int random = GetRandomInt(upperLimit, LowerLimit);
                int attemptCount = 0;
                while (randoms.Contains(random) && count < 100)
                {
                    random = GetRandomInt(upperLimit, LowerLimit);
                    attemptCount++;
                }
                randoms.Add(random);
            }
            return randoms;
        }
        static Random random = new Random();
        public static int GetRandomInt(int upperLimit, int LowerLimit = 1)
        {
            return random.Next(LowerLimit, upperLimit);
        }
    }

    public static class ReflectionUtils
    {
        public static string GetMemberName<T, TValue>(Expression<Func<T, TValue>> memberAccess)
        {
            return ((MemberExpression)memberAccess.Body).Member.Name;
        }
    }
}

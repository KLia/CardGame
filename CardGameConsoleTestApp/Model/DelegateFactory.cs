using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CardGameConsoleTestApp.Model
{
    public static class DelegateFactory
    {
        private static readonly IDictionary<string, Delegate> DelegatesDictionary;
        private static readonly string FullyQualifiedName = ConfigurationManager.AppSettings["TriggerNamespace"];

        static DelegateFactory()
        {
            DelegatesDictionary = new Dictionary<string, Delegate>();
        }

        public static Delegate GetDelegate(string className, string methodName)
        {
            var name = $"{className}{methodName}";
            if (DelegatesDictionary.ContainsKey(name))
            {
                return DelegatesDictionary[name];
            }

            var memberInfo = Type.GetType(FullyQualifiedName + "." + className);
            if (memberInfo == null)
            {
                return null;

            }
            MethodInfo method = memberInfo.GetMethod(methodName);
            Delegate result = CreateStaticDelegate(method);
            DelegatesDictionary.Add(name, result);

            return result;
        }

        private static Delegate CreateStaticDelegate(MethodInfo method)
        {
            var paramTypes = method.GetParameters().Select(p => p.ParameterType);

            Type delegateType = Expression.GetDelegateType(paramTypes.Append(method.ReturnType).ToArray());

            return Delegate.CreateDelegate(delegateType, method, true);
        }

        private static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> collection, TSource element)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            foreach (var element1 in collection) yield return element1;
            yield return element;
        }
    }
}

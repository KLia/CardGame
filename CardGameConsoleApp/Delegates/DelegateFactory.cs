using System;
using System.Collections.Generic;
using System.Reflection;

namespace CardGameConsoleApp.Delegates
{
    public static class DelegateFactory
    {
        private static readonly IDictionary<string, MethodInfo> MethodsDictionary;

        static DelegateFactory()
        {
            MethodsDictionary = new Dictionary<string, MethodInfo>();
        }

        private static MethodInfo GetMethodInfo(string fullyQualifiedName, string className, string methodName)
        {
            var name = $"{className}{methodName}";
            if (MethodsDictionary.ContainsKey(name))
            {
                return MethodsDictionary[name];
            }

            var memberInfo = Type.GetType(fullyQualifiedName + "." + className);
            if (memberInfo == null)
            {
                return null;

            }
            var method = memberInfo.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            MethodsDictionary.Add(name, method);

            return method;
        }

        public static void RunMethod(string fullyQualifiedName, string className, string methodName, object[] parameters)
        {
            var method = GetMethodInfo(fullyQualifiedName, className, methodName);
            method.Invoke(null, parameters);
        }
    }
}

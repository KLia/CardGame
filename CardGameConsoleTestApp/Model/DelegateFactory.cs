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
        private static readonly IDictionary<string, MethodInfo> MethodsDictionary;
        private static readonly string FullyQualifiedName = ConfigurationManager.AppSettings["TriggerNamespace"];

        static DelegateFactory()
        {
            MethodsDictionary = new Dictionary<string, MethodInfo>();
        }

        private static MethodInfo GetMethodInfo(string className, string methodName)
        {
            var name = $"{className}{methodName}";
            if (MethodsDictionary.ContainsKey(name))
            {
                return MethodsDictionary[name];
            }

            var memberInfo = Type.GetType(FullyQualifiedName + "." + className);
            if (memberInfo == null)
            {
                return null;

            }
            MethodInfo method = memberInfo.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            MethodsDictionary.Add(name, method);

            return method;
        }

        public static void RunMethod(string className, string methodName, object[] parameters)
        {
            MethodInfo method = GetMethodInfo(className, methodName);
            method.Invoke(null, parameters);
        }
    }
}

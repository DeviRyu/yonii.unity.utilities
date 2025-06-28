using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Yonii.Unity.Utilities
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetClasses(this Type baseType) =>
            Assembly
                .GetAssembly(baseType)
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t));
        
        public static string[] GetAllInheritedTypeNames(this Type baseType) 
        {
            var types = baseType.GetClasses().ToList();
            
            var result = new string[types.Count];
            var index = 0;
            types.ForEach(t =>
            {
                var descriptionObject = t.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                string name = null;
                if (descriptionObject is DescriptionAttribute description) 
                    name = description.Description;
                
                result[index] = name ?? t.Name;
                index++;
            });

            return result;
        }

        public static IEnumerable<Type> GetImplementingTypes(this Type type) =>
            AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly
                    .GetTypes())
                .Where(t => t
                    .GetInterfaces()
                    .Contains(type));
        
        public static IEnumerable<string> GetImplementingTypeNames(this Type type) =>
            type.GetImplementingTypes()
                .Select(t => t.Name);
    }
}
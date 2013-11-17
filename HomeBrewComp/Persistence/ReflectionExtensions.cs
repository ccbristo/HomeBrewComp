using System;
using System.Linq;
using NHibernate.Mapping.ByCode;
using HomeBrewComp.Domain;

namespace HomeBrewComp.Reflection
{
    internal static class ReflectionExtensions
    {
        public static bool IsEntity(this Type type)
        {
            if (!type.IsClass || type.IsGenericType || type.IsAbstract)
                return false;

            var entityBaseTypes = from baseType in type.GetBaseTypes()
                                  where baseType.IsGenericType &&
                                         baseType.GetGenericTypeDefinition() == typeof(Entity<>)
                                  select baseType;

            return entityBaseTypes.Any();
        }

        public static bool IsRootEntity(this Type type)
        {
            if (!type.IsClass || type.IsGenericType || type.IsAbstract)
                return false;

            return type.BaseType != null &&
                   type.BaseType.IsGenericType &&
                   type.BaseType.GetGenericTypeDefinition() == typeof(Entity<>);
        }

        public static bool IsValueObject(this Type type)
        {
            if (!type.IsClass || type.IsGenericType || type.IsAbstract)
                return false;

            var valueObjectBaseTypes = from baseType in type.GetBaseTypes()
                                  where baseType.IsGenericType &&
                                         baseType.GetGenericTypeDefinition() == typeof(ValueObject<>)
                                  select baseType;

            return valueObjectBaseTypes.Any();
        }

        public static bool IsEnumeration(this Type type)
        {
            if (!type.IsClass || type == typeof(Enumeration<>))
                return false;

            var enumerationType = from baseType in type.GetBaseTypes()
                                  where baseType.IsGenericType &&
                                      baseType.GetGenericTypeDefinition() == typeof(Enumeration<>)
                                  select baseType;

            return enumerationType.Any();
        }
    }
}
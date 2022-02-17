﻿/// <summary>
/// Codeit Corp
/// </summary>
namespace Codeit.Enterprise.Base.Common
{
    using System;
    using System.Linq;

    public partial class CodeitUtils
    {
        public static string GetGenericTypeName(Type type)
        {
            var typeName = string.Empty;

            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }

        public static string GetGenericTypeName(object @object)
        {
            return @object.GetType().GetGenericTypeName();
        }
    }

    public static class GenericTypeExtensions
    {
        public static string GetGenericTypeName(this Type type)
        {
            return CodeitUtils.GetGenericTypeName(type);
        }

        public static string GetGenericTypeName(this object @object)
        {
            return CodeitUtils.GetGenericTypeName(@object);
        }
    }
}
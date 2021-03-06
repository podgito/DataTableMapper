﻿using DataTableMapper.Attributes;
using System;
using System.Linq;

namespace DataTableMapper.Mapping
{
    internal class DefaultValueAttributeMapping : IMapping
    {
        public object Map(System.Reflection.PropertyInfo property, System.Data.DataRow row)
        {
            if (TypeHelper.IsSimpleType(property.PropertyType))
            {
                if (property.GetCustomAttributes(typeof(DefaultValueAttribute), true).Any())
                {
                    var defaultValueAttribute = (DefaultValueAttribute)property.GetCustomAttributes(typeof(DefaultValueAttribute), true).First();

                    return defaultValueAttribute.Value;
                }

                return null; //return null if we get this far
            }
            else
            {
                throw new ArgumentException("Property must be a simple type");
            }
        }
    }
}
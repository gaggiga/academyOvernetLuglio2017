using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GadgetStore.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class OnlyOneAttribute : ValidationAttribute
    {
        private readonly string[] _propertyNames;

        public OnlyOneAttribute(params string[] propertyNames)
        {
            if (propertyNames == null) throw new ArgumentNullException("propertyNames");
            if (propertyNames.Length < 2) throw new ArgumentOutOfRangeException("propertyNames");
            _propertyNames = propertyNames;
        }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);

            var values = new object[_propertyNames.Length];

            for(var i = 0; i < _propertyNames.Length; i++)
            {
                values[i] = properties.Find(_propertyNames[i], false).GetValue(value);
            }

            var howMany = values.Where(o => o != null).Count();

            return howMany == 1;
        }
    }
}
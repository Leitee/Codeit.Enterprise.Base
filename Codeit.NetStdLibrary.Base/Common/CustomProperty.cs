using Codeit.NetStdLibrary.Base.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codeit.NetStdLibrary.Base.Utils
{
    public class Property<T>
    {
        private T _value;

        public T Value
        {
            get
            {
                // insert desired logic here
                return _value;
            }
            set
            {
                // insert desired logic here
                _value = value;
            }
        }

        public static implicit operator T(Property<T> value)
        {
            return value.Value;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T> { Value = value };
        }
    }
}

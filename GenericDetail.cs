using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnitudeTest
{

    public class GenericDetail
    {
        public string PropertyName;
        public string PropertyType;
        public object PropertyValue;
    }

    public enum Operators
    {
        CONTAINS,
        EQUALS,
        GREATERTHAN,
        LESSTHAN
    }

    public enum Combiners
    {
        AND,
        OR
    }

}

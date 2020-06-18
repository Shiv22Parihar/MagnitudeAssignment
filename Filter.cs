using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnitudeTest
{

    public interface IFilter
    {
        bool IsMatch(List<GenericDetail> row);
    }

    public class SingleFilter : IFilter
    {
        public string PropertyName;
        public object PropertyValue;
        public Operators Operator;

        public bool IsMatch(List<GenericDetail> row)
        {
            foreach (var item in row)
            {
                if (item.PropertyName == PropertyName)
                {
                    return GetResultBasedOnPropertyType(item);
                }
            }
            return true;
        }

        private bool GetResultBasedOnPropertyType(GenericDetail item)
        {
            switch (item.PropertyType)
            {
                case "string": return GetResultBasedOnStringProperty(item.PropertyValue);
                case "int": return GetResultBasedOnIntProperty(item.PropertyValue);
                case "date": return GetResultBasedOnDateProperty(item.PropertyValue);
                default: return false;
            }
        }

        private bool GetResultBasedOnStringProperty(object value)
        {
            string val = value.ToString();
            if (Operator == Operators.EQUALS)
                return val.Equals(PropertyValue.ToString());
            if (Operator == Operators.CONTAINS)
                return val.Contains(PropertyValue.ToString());
            return true;
        }

        private bool GetResultBasedOnIntProperty(object value)
        {
            int val = int.Parse(value.ToString());

            if (Operator == Operators.EQUALS)
                return val == int.Parse(PropertyValue.ToString());
            if (Operator == Operators.GREATERTHAN)
                return val > int.Parse(PropertyValue.ToString());
            if (Operator == Operators.LESSTHAN)
                return val < int.Parse(PropertyValue.ToString());
            return true;
        }

        private bool GetResultBasedOnDateProperty(object value)
        {
            DateTime val = Convert.ToDateTime(value.ToString());

            if (Operator == Operators.EQUALS)
                return val == Convert.ToDateTime(PropertyValue.ToString());
            if (Operator == Operators.GREATERTHAN)
                return val > Convert.ToDateTime(PropertyValue.ToString());
            if (Operator == Operators.LESSTHAN)
                return val < Convert.ToDateTime(PropertyValue.ToString());
            return true;
        }
    }

    public class ComplexFilter : IFilter
    {
        public Combiners Combiner;
        public List<IFilter> Filter;

        public bool IsMatch(List<GenericDetail> row)
        {
            for (int i = 0; i < Filter.Count; i++)
            {
                if (Combiner == Combiners.AND)
                {
                    if (!Filter[i].IsMatch(row))
                        return false;
                }
                else
                {
                    if (Filter[i].IsMatch(row))
                        return true;
                }
            }
            if (Combiner == Combiners.AND)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Check(List<GenericDetail> row, SingleFilter data)
        {
            return true;
            // return the single check
        }
    }

}

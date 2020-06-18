using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnitudeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Case1();
            Case2();
            Console.ReadLine();
        }

        /// <summary>
        /// Case 1 : Single Filter
        /// </summary>
        public static void Case1()
        {
            ICSVReader reader = new CSVReader();

            IFilter filter = new SingleFilter() { Operator = Operators.GREATERTHAN, PropertyName = "EmployeeID", PropertyValue = 1 };

            List<string> reqColumns = new List<string>() { "EmployeeFirstName", "EmployeeLastName", "EmployeeDOB", "EmployeeID" };

            var data = reader.GetNextData(10, 0, filter, reqColumns);

            if (data.Count > 0)
                DisplayRecords(data);
            else
                Console.WriteLine("No Result Found for this filter search");

            data = reader.GetNextData(10, 11, filter, reqColumns);

            if (data.Count > 0)
                DisplayRecords(data);
            else
                Console.WriteLine("No Result Found for this filter search");
        }

        /// <summary>
        /// Case 2 : Complex Filter
        /// </summary>
        public static void Case2()
        {
            ICSVReader reader = new CSVReader();
            SingleFilter filter1 = new SingleFilter() { Operator = Operators.GREATERTHAN, PropertyName = "EmployeeID", PropertyValue = 1 };
            SingleFilter filter2 = new SingleFilter() { Operator = Operators.CONTAINS, PropertyName = "EmployeeFirstName", PropertyValue = "Su" };
            List<IFilter> filterList = new List<IFilter>();
            filterList.Add(filter1);
            filterList.Add(filter2);
            IFilter filter = new ComplexFilter()
            {
                Combiner = Combiners.AND,
                Filter = filterList
            };

            List<string> reqColumns = new List<string>() { "EmployeeFirstName", "EmployeeLastName", "EmployeeDOB", "EmployeeID" };

            var data = reader.GetNextData(5, 0, filter, reqColumns);

            if (data.Count > 0)
                DisplayRecords(data);
            else
                Console.WriteLine("No Result Found for this filter search");

            data = reader.GetNextData(10, 6, filter, reqColumns);

            if (data.Count > 0)
                DisplayRecords(data);
            else
                Console.WriteLine("No Result Found for this filter search");
        }

        /// <summary>
        /// Method to display record set
        /// </summary>
        /// <param name="data"></param>
        private static void DisplayRecords(List<List<GenericDetail>> data)
        {
            Console.WriteLine("-------------------Data In Current Set Begins----------------------");

            foreach (var item in data)
            {
                Console.WriteLine();
                var employeeFirstName = item.Single(x => x.PropertyName == "EmployeeFirstName").PropertyValue;
                var employeeLastName = item.Single(x => x.PropertyName == "EmployeeLastName").PropertyValue;
                var employeeID = item.Single(x => x.PropertyName == "EmployeeID").PropertyValue;
                var employeeDOB = item.Single(x => x.PropertyName == "EmployeeDOB").PropertyValue;

                Console.WriteLine("DOB of Employee {0} {1} with Employee ID - {2} is {3}", employeeFirstName, employeeLastName, employeeID, employeeDOB);
                Console.WriteLine();
            }

            Console.WriteLine("-------------------Data Of Current Set Ends----------------------");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnitudeTest
{
    public class CSVReader : ICSVReader
    {
        public List<List<GenericDetail>> GetNextData(int dataSize, int lastIndex, IFilter filter, List<string> reqColumns)
        {
            // Read file lines
            string[] allLines = File.ReadAllLines(ConfigurationManager.AppSettings["FilePath"]);

            // Prepare the GenericDetail DS usign first 2 lines of the file.
            List<GenericDetail> dataStruct = GetDataStruct(allLines[0], allLines[1]);

            // Take the required number of lines for which data needs to be written.
            List<string> lines = allLines.Skip(lastIndex + 2).ToList();

            // Filter the data based on given filter and get list of filtered data with only required columns.
            return GetFilteredData(dataSize, filter, lines, dataStruct, reqColumns);
        }

        private List<GenericDetail> GetDataStruct(string propertyName, string propertyType)
        {
            string[] propertyNames = propertyName.Split(',');
            string[] propertyTypes = propertyType.Split(',');

            List<GenericDetail> dataStruct = new List<GenericDetail>();

            for (int i = 0; i < propertyNames.Length; i++)
            {
                dataStruct.Add(new GenericDetail() { PropertyName = propertyNames[i], PropertyType = propertyTypes[i] });
            }
            return dataStruct;
        }

        private List<List<GenericDetail>> GetFilteredData(int dataSize, IFilter filter, List<string> lines, List<GenericDetail> dataStruct, List<string> reqColums)
        {
            List<List<GenericDetail>> filteredData = new List<List<GenericDetail>>();
            int counter = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                if (counter < dataSize)
                {
                    List<GenericDetail> data = new List<GenericDetail>();
                    data = UpdateValue(lines[i], dataStruct);
                    if (filter.IsMatch(data))
                    {
                        List<GenericDetail> reqData = new List<GenericDetail>();
                        reqData = TakeRequiredColumns(data, reqColums);
                        filteredData.Add(reqData);
                        counter++;
                    }
                }
            }

            return filteredData;
        }

        private List<GenericDetail> TakeRequiredColumns(List<GenericDetail> data, List<string> reqColumns)
        {
            List<GenericDetail> reqData = new List<GenericDetail>();
            for (int i = 0; i < data.Count; i++)
            {
                if (reqColumns.Contains(data[i].PropertyName))
                {
                    reqData.Add(data[i]);
                }
            }
            return reqData;
        }

        private List<GenericDetail> UpdateValue(string item, List<GenericDetail> dataStruct)
        {
            string[] values = item.Split(',');
            List<GenericDetail> dataSet = new List<GenericDetail>();
            for (int i = 0; i < values.Length; i++)
            {
                GenericDetail data = new GenericDetail() { PropertyValue = values[i], PropertyName = dataStruct[i].PropertyName, PropertyType = dataStruct[i].PropertyType };
                dataSet.Add(data);
            }
            return dataSet;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagnitudeTest
{
    public interface ICSVReader
    {
        // StateFull data
        List<List<GenericDetail>> GetNextData(int dataSize, int lastIndex, IFilter filter, List<string> reqColumns);
    }
}

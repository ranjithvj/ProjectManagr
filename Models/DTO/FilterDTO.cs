using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class FilterRequestDTO
    {
        public string SearchText { get; set; }

        ///OrderByString is computed by following

        //var orderByString = String.Empty;
        //foreach (var column in sortedColumns)
        //{
        //    orderByString += orderByString != String.Empty? "," : "";
        //    orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant? " asc" : " desc");
        //}
        public string OrderByString { get; set; }

        public int RecordCountStart { get; set; }
        public int RecordCountLength { get; set; }

    }

    public class FilterResponseDTO<T> where T : BaseModel
    {
        public int FilteredCount { get; set; }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}

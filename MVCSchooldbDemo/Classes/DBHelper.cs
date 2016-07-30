using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using Newtonsoft.Json;

namespace MVCSchooldbDemo.Classes
{
    public class DBHelper
    {
        public static string SortingAndPaging<T>(List<T> data, int page, int rows, string sort, string order)
        {
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                data = data.OrderBy($"{sort} {order}").ToList();
            }

            int totalCount = data.Count;

            data = data.Take(rows * page).Skip(rows * (page - 1)).ToList();

            return JsonConvert.SerializeObject(new { total = totalCount, rows = data });
        }

        public static List<T> FilterByKeywords<T>(List<T> data, string[] propertyNames, string[] keywords)
        {
            for (int i = 0; i < keywords.Count(); i++)
            {
                if (!string.IsNullOrEmpty(keywords[i]))
                {
                    data = data.Where($"{propertyNames[i]}.Contains(\"{keywords[i]}\")").ToList();
                }
            }

            return data;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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

            var totalCount = data.Count;

            data = data.Take(rows * page).Skip(rows * (page - 1)).ToList();

            return JsonConvert.SerializeObject(new {total = totalCount, rows = data});
        }

        public static List<T> FilterByKeywords<T>(List<T> data, string[] propertyNames, string[] keywords)
        {
            for (var i = 0; i < keywords.Count(); i++)
            {
                if (!string.IsNullOrEmpty(keywords[i]))
                {
                    data = data.Where($"{propertyNames[i]}.Contains(\"{keywords[i]}\")").ToList();
                }
            }

            return data;
        }

        public static List<T> FindByKeyword<T>(List<T> data, string propertyName, object keyword)
        {
            string expression;

            if (keyword is string)
            {
                expression = $"{propertyName}=\"{keyword}\"";
            }
            else
            {
                expression = $"{propertyName}={keyword}";
            }

            return data.Where(expression).ToList();
        }
    }
}
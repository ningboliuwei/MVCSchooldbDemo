using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Runtime.Remoting.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVCSchooldbDemo.Classes
{
    public class DBHelper
    {
        public static string Paging<T>(List<T> data, int page, int rows)
        {
            var totalCount = data.Count;

            data = data.Take(rows * page).Skip(rows * (page - 1)).ToList();

            return JsonConvert.SerializeObject(new { total = totalCount, rows = data });
        }


        public static List<T> Sorting<T>(List<T> data, string sort, string order)
        {
            if (!string.IsNullOrEmpty(sort) && !string.IsNullOrEmpty(order))
            {
                data = data.OrderBy($"{sort} {order}").ToList();
            }

            return data;
        }

        public static List<T> FilterByKeywords<T>(List<T> data, string queryData)
        {
            if (!string.IsNullOrEmpty(queryData))
            {
                JObject v = JObject.Parse(queryData);

                List<string> propertyNames = v.Properties().Select(p => p.Name).ToList();
                List<string> keywords = v.Properties().Select(p => p.Value.ToString()).ToList();


                for (var i = 0; i < keywords.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(keywords[i]))
                    {
                        data = data.Where($"{propertyNames[i]}.Contains(\"{keywords[i]}\")").ToList();
                    }
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

        public static string GetGridResult<T>(List<T> data, string queryParasString, int page, int rows, string sort, string order)
        {
            if (!string.IsNullOrEmpty(queryParasString))
            {
                data = DBHelper.FilterByKeywords(data, queryParasString);
            }

            data = DBHelper.Sorting(data, sort, order);

            return DBHelper.Paging(data, page, rows);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace ExtensionMethods
{
    public static class ListExtensions
    {
        public static DataTable ToDataTable<T>(this IList<T> list)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in list)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    var value = props[i].GetValue(item) ?? DBNull.Value;
                    if (value is decimal)
                    {
                        value = Math.Round((decimal)value, 2);
                    }
                    values[i] = value;
                }

                table.Rows.Add(values);
            }
            return table;
        }

        public static T GetRandom<T>(List<T> objects)
        {   
            var rand = objects[new Random().Next(objects.Count)];
            return rand;
        }
    }
}

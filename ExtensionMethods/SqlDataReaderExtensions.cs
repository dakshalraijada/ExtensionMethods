using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public static class SqlDataReaderExtensions
    {
        /// <summary>
        /// Gets the value of the specified column as a type, given the column name.
        /// </summary>
        /// <typeparam name="T">The expected type of the column being retrieved.</typeparam>
        /// <param name="reader">The reader from which to retrieve the column.</param>
        /// <param name="columnName">The name of the column to be retrieved.</param>
        /// <returns>The returned type object.</returns>
        public static T GetFieldValue<T>(this SqlDataReader reader, string columnName)
        {
            return reader.GetFieldValue<T>(reader.GetOrdinal(columnName));
        }

        public static T GetValue<T>(this SqlDataReader reader, string columnName)
        {
            object columnValue = reader[columnName];
            T returnValue = default;
            if (!(columnValue is DBNull))
            {
                returnValue = (T)Convert.ChangeType(columnValue, typeof(T));
            }
            return returnValue;
        }

        public static int ReadIntValue(this SqlDataReader reader, string field)
        {
            int intValue = 0;

            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    int.TryParse(rawValue.ToString(), out intValue);
                }
            }

            return intValue;
        }
        public static int? ReadNullableIntValue(this SqlDataReader reader, string field)
        {
            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    if (int.TryParse(rawValue.ToString(), out int value))
                    {
                        return value;
                    }
                    return null;
                }

                return null;
            }

            return null;
        }

        public static byte ReadByteValue(this SqlDataReader reader, string field)
        {
            byte value = 0;

            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    byte.TryParse(rawValue.ToString(), out value);
                }

            }

            return value;
        }
        public static byte? ReadNullableByteValue(this SqlDataReader reader, string field)
        {
            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    if (byte.TryParse(rawValue.ToString(), out byte value))
                    {
                        return value;
                    }
                    return null;
                }

                return null;
            }

            return null;
        }

        public static decimal ReadDecimalValue(this SqlDataReader reader, string field)
        {
            decimal decValue = 0;

            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    decimal.TryParse(rawValue.ToString(), out decValue);
                }
            }

            return decValue;
        }
        public static decimal? ReadNullableDecimalValue(this SqlDataReader reader, string field)
        {
            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    if (decimal.TryParse(rawValue.ToString(), out decimal value))
                    {
                        return value;
                    }

                    return null;
                }

                return null;
            }

            return null;

        }

        public static double ReadDoubleValue(this SqlDataReader reader, string field)
        {
            double doubleValue = 0;

            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    double.TryParse(rawValue.ToString(), out doubleValue);
                }
            }

            return doubleValue;
        }
        public static double? ReadNullableDoubleValue(this SqlDataReader reader, string field)
        {
            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    if (double.TryParse(rawValue.ToString(), out double value))
                    {
                        return value;
                    }
                    return null;
                }

                return null;
            }

            return null;
        }

        public static bool ReadBooleanValue(this SqlDataReader reader, string field)
        {
            bool booValue = false;

            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    bool.TryParse(rawValue.ToString(), out booValue);
                }
            }

            return booValue;
        }
        public static bool? ReadNullableBoolValue(this SqlDataReader reader, string field)
        {
            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    if (bool.TryParse(rawValue.ToString(), out bool value))
                    {
                        return value;
                    }

                    return null;
                }

                return null;
            }

            return null;
        }
        
        public static DateTime ReadDateTimeValue(this SqlDataReader reader, string field)
        {
            DateTime value = DateTime.MinValue;

            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    if (DateTime.TryParse(rawValue.ToString(), out value))
                        return value;
                }
            }

            return DateTime.MinValue;

        }
        public static DateTime? ReadNullableDateTimeValue(this SqlDataReader reader, string field)
        {
            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {

                    DateTime value;
                    if (DateTime.TryParse(rawValue.ToString(), out value))
                        return value;
                }
            }

            return null;
        }
        
        public static Guid ReadGuidValue(this SqlDataReader reader, string field)
        {
            Guid guidValue;

            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (Guid.TryParse(rawValue.ToString(), out Guid value))
                {
                    return value;
                }
            }

            return guidValue;
        }
        public static Guid? ReadNullableGuidValue(this SqlDataReader reader, string field)
        {
            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {

                    if (Guid.TryParse(rawValue.ToString(), out Guid value))
                    {
                        return value;
                    }

                    return null;
                }

                return null;
            }

            return null;
        }

        public static string ReadStringValue(this SqlDataReader reader, string field)
        {
            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];

                if (rawValue != DBNull.Value && rawValue != null)
                {
                    return rawValue.ToString();
                }
            }

            return string.Empty;

        }
        public static string ReadNullableTimeValueAsString(this SqlDataReader reader, string field, bool includeSeconds)
        {
            if (reader.GetSchemaTable().Select($"ColumnName = '{field}'").Any())
            {
                var rawValue = reader[field];
                if (rawValue != DBNull.Value && rawValue != null)
                {
                    return includeSeconds ? rawValue.ToString().Substring(0, 8) : rawValue.ToString().Substring(0, 5);
                }
            }
            return string.Empty;
        }

    }
}

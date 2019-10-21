using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ExtensionMethods
{
    /// <summary>
    /// There is a problem with the AddWithValue() function: 
    /// it has to infer the database type for your query parameter. 
    /// Here’s the thing: sometimes it gets it wrong. 
    /// This especially happens with database layers that deal in Object arrays or similar for the parameter data, 
    /// where some of the important information ADO.Net uses to infer the type is missing. 
    /// However, this can happen even when the .Net type is known. 
    /// VarChar vs NVarChar or Char from strings is one way. Date vs DateTime is another.
    /// Reference: https://blogs.msmvps.com/jcoehoorn/blog/2014/05/12/can-we-stop-using-addwithvalue-already/
    /// </summary>
    public static class SqlParameterCollectionExtensions
    {
        /// <summary>
        /// uses add with value
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SqlParameter AddNullableStringValue(this SqlParameterCollection collection, string name, string value)
        {
            return collection.AddWithValue(name, string.IsNullOrEmpty(value) ? DBNull.Value : (object)value);
        }

        public static SqlParameter AddNVarChar(this SqlParameterCollection collection, string name, string value, int size)
        {   
            return collection.Add(new SqlParameter("@" + name, SqlDbType.NVarChar, size, ParameterDirection.Input, false, 0, 0, name, DataRowVersion.Current, value.ToEmptyWhenNull()));
        }

        public static SqlParameter AddNVarCharMax(this SqlParameterCollection collection, string name, string value)
        {   
            return collection.Add(new SqlParameter("@" + name, SqlDbType.NVarChar, -1, ParameterDirection.Input, false, 0, 0, name, DataRowVersion.Current, value.ToEmptyWhenNull()));
        }

        public static SqlParameter AddNullableDateTime(this SqlParameterCollection collection, string name, DateTime? value)
        {
            SqlParameter dateParameter = new SqlParameter("@" + name, value);
            dateParameter.IsNullable = true;
            if (dateParameter.Value == null)
                dateParameter.Value = DBNull.Value;
            dateParameter.Direction = ParameterDirection.Input;
            dateParameter.SqlDbType = SqlDbType.DateTime;
            return collection.Add(dateParameter);

        }

        public static SqlParameter AddSmallInt(this SqlParameterCollection collection, string name, int value)
        {
            return collection.Add(new SqlParameter("@" + name, SqlDbType.SmallInt, 2, ParameterDirection.Input, false, 0, 0, name, DataRowVersion.Current, value));
        }

        public static SqlParameter AddInt(this SqlParameterCollection collection, string name, int value)
        {
            return collection.Add(new SqlParameter("@" + name, SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, name, DataRowVersion.Current, value));
        }

        public static SqlParameter AddNullableMoney(this SqlParameterCollection collection, string name, decimal? value)
        {
            object dbValue = DBNull.Value;
            if (value.HasValue)
                dbValue = value.Value;
            return collection.Add(new SqlParameter("@" + name, SqlDbType.Money, 8, ParameterDirection.Input, true, 0, 0, name, DataRowVersion.Current, dbValue));
        }

        public static SqlParameter AddNullableDecimal(this SqlParameterCollection collection, string name, decimal? value, byte precision, byte scale)
        {
            object dbValue = DBNull.Value;
            if (value.HasValue)
                dbValue = value.Value;
            return collection.Add(new SqlParameter("@" + name, SqlDbType.Decimal, precision, ParameterDirection.Input, true, precision, scale, name, DataRowVersion.Current, dbValue));
        }

        public static SqlParameter AddNullableInt(this SqlParameterCollection collection, string name, int? value)
        {
            object dbValue = DBNull.Value;
            if (value.HasValue)
                dbValue = value.Value;
            return collection.Add(new SqlParameter("@" + name, SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, name, DataRowVersion.Current, dbValue));
        }

        public static SqlParameter AddNullableBit(this SqlParameterCollection collection, string name, bool? value)
        {
            object dbValue = DBNull.Value;
            if (value.HasValue)
                dbValue = value.Value;
            return collection.Add(new SqlParameter("@" + name, SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, name, DataRowVersion.Current, dbValue));
        }

        public static SqlParameter AddBit(this SqlParameterCollection collection, string name, bool value)
        {
            return collection.Add(new SqlParameter("@" + name, SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, name, DataRowVersion.Current, value));
        }

        public static SqlParameter AddBitWithDefault(this SqlParameterCollection collection, string name, bool? value, bool defaultIfNull)
        {
            return collection.Add(new SqlParameter("@" + name, SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, name, DataRowVersion.Current, value.HasValue ? value.Value : defaultIfNull));
        }

        public static SqlParameter AddTable(this SqlParameterCollection collection, string name, DataTable value)
        {            
            return collection.Add(new SqlParameter("@" + name, SqlDbType.Structured, -1, ParameterDirection.Input, false, 0, 0, name, DataRowVersion.Current, value));
        }

    }
}

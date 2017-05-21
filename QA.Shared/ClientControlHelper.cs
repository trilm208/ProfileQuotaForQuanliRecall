using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace QA
{
    internal class ClientControlHelper
    {
        public static bool ValidateSaveResult(DataSet ds)
        {
            return ValidateSaveResult(ds, true);
        }

        public static bool ValidateSaveResult(DataSet ds, bool showInformationSaved)
        {
            var result = ds.FirstValue();

            if (string.IsNullOrEmpty(result) == false)
            {
                if (result.ToUpper() != "OK")
                {
                    UI.ShowError(result);
                    return false;
                }
                else if (showInformationSaved)
                {
                    UI.InformationSaved();
                    return true;
                }
            }

            return true;
        }

        /// <summary>
        /// Parameterized handeling of "Process"
        /// </summary>
        public static void Process(object target, object args)
        {
            if (args == null)
                return;

            if (args is DataRow)
            {
                ProcessDataRow(target, args as DataRow);
            }
            else
            {
                ProcessObject(target, args);
            }
        }

        private static void ProcessObject(object target, object args)
        {
            var properties = GetProperties(target);
            var type = args.GetType();
            var arg_properties = type.GetProperties();

            foreach (var arg_property in arg_properties)
            {
                var arg_name = arg_property.Name;
                var arg_value = arg_property.GetValue(args, null);

                foreach (var property in properties)
                {
                    if (string.Compare(arg_name, property.Name, true) == 0)
                    {
                        property.SetValue(target, arg_value, null);
                        break;
                    }
                }
            }
        }

        private static void ProcessDataRow(object target, DataRow row)
        {
            var columns = row.Table.Columns;
            var properties = GetProperties(target);

            foreach (DataColumn column in columns)
            {
                string name = column.ColumnName;
                object value = row[column];

                if (value == null || value == DBNull.Value)
                    value = string.Empty;

                foreach (var property in properties)
                {
                    if (string.Compare(name, property.Name, true) == 0)
                    {
                        property.SetValue(target, value.ToString(), null);
                        break;
                    }
                }
            }
        }

        private static IEnumerable<PropertyInfo> GetProperties(object target)
        {
            var result = new List<PropertyInfo>();
            var type = target.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                if (!property.CanWrite)
                    continue;

                if (property.DeclaringType == typeof(Form))
                    continue;

                if (property.DeclaringType == typeof(Control))
                    continue;

                if (property.PropertyType != typeof(string))
                    continue;
              

                result.Add(property);
            }

            return result;
        }
    }
}
using System.Text;

namespace System.Data
{
    internal static class DataExtensions
    {
        public static string CreateSchemaXml(this DataTable dataTable, string name)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("<xs:element name='{0}'>", name).AppendLine();
            sb.AppendLine("<xs:complexType>");
            sb.AppendLine("<xs:sequence>");

            foreach (DataColumn column in dataTable.Columns)
            {
                var columnName = column.ColumnName;
                var type = column.DataType;
                string typeName = char.ToLower(type.Name[0]) + type.Name.Substring(1);

                if (typeName.Contains("int"))
                    typeName = "int";

                sb.AppendFormat("<xs:element name='{0}' type='xs:{1}' minOccurs='0' />", columnName, typeName).AppendLine();
            }

            sb.AppendLine("</xs:sequence>");
            sb.AppendLine("</xs:complexType>");
            sb.AppendLine("</xs:element>");

            return sb.ToString();
        }
       
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Eric.Morrison
{
    static public class DataSetExtensions
    {
        static public bool HasTables(this DataSet ds)
        {
            var result = false;
            if (null != ds)
            {
                if (null != ds.Tables)
                {
                    if (ds.Tables.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        static public bool HasRows(this DataSet ds)
        {
            var result = false;
            if (null != ds)
            {
                if (null != ds.Tables)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        static public DataRow GetFirstRow(this DataSet ds)
        {
            DataRow result = null;
            if (ds.HasRows())
                result = ds.Tables[0].Rows[0];
            return result;
        }

        static public bool HasRows(this DataTable dt)
        {
            var result = false;
            if (null != dt)
            {
                if (dt.Rows.Count > 0)
                    result = true;
            }
            return result;
        }

        static public DataRow GetFirstRow(this DataTable dt)
        {
            DataRow result = null;
            if (null != dt)
            {
                if (dt.Rows.Count > 0)
                {
                    result = dt.Rows[0];
                }
            }
            return result;
        }

        static public void Trace(this DataSet ds)
        {
            string xsd = ds.GetXmlSchema();
            //Debug.WriteLine(xsd);

            Debug.WriteLine(string.Format("DataSetName: {0}", ds.DataSetName));

            DataTableCollection tables = ds.Tables;
            Trace(tables);

        }

        static void Trace(this DataTableCollection tables)
        {
            int nTables = tables.Count;
            for (int t = 0; t < nTables; ++t)
            {
                DataTable dt = tables[t];
                Debug.Indent();
                Debug.WriteLine(string.Format("DataTable[{0}]:", t));
                Trace(dt);
                Debug.Unindent();
            }
        }

        public static void Trace(this DataTable dt)
        {
            Debug.Indent();
            Debug.WriteLine(string.Format("TableName: {0}", dt.TableName));

            //Trace(dt.Columns);
            //Trace(dt.Rows);

            Trace(dt.Columns, dt.Rows);
            Debug.Unindent();
        }

        static void Trace(this DataColumnCollection columns)
        {
            int count = columns.Count;

            Debug.WriteLine("Columns:");
            Debug.Indent();
            Debug.WriteLine(string.Format("Count: {0}", count));

            Debug.WriteLine("List:");
            Debug.Indent();

            for (int c = 0; c < count; ++c)
            {
                DataColumn dc = columns[c];
                Debug.WriteLine(string.Format("[{0}]: {1}", c, dc.ColumnName));
            }

            Debug.Unindent();

            Debug.Unindent();
        }

        static void Trace(this DataRowCollection rows, bool traceData = false)
        {
            int count = rows.Count;

            Debug.WriteLine("Rows:");

            Debug.Indent();
            Debug.WriteLine(string.Format("Count: {0}", count));
            Debug.Unindent();

            foreach (var row in rows)
                Trace(row as DataRow);
        }

        static void Trace(this DataRow row)
        {
            var table = row.Table;
            var columns = table.Columns;

            int count = columns.Count;

            Debug.WriteLine("Data:");

            Debug.Indent();

            for (int c = 0; c < count; ++c)
            {
                DataColumn dc = columns[c];
                var dataOb = row.Field<object>(dc.ColumnName);
                var val = "NULL";
                if (null != dataOb)
                    val = dataOb.ToString();

                Debug.WriteLine(string.Format("{0}={1}", dc.ColumnName, val));
            }

            Debug.Unindent();
        }

        static void Trace(DataColumnCollection columns, DataRowCollection rows)
        {
            Debug.IndentLevel = 0;
            int count = columns.Count;
            List<int> columnWidths = GetColumnWidths(columns, rows);
            var sb = new StringBuilder();


            var appendFormatted = new Action<string, bool>(
                delegate (string data, bool lastColumn)
                {
                    if (lastColumn)
                    {
                        sb.Append(string.Format("| {0} |", data));
                    }
                    else
                    {
                        sb.Append(string.Format("| {0}", data));
                    }
                });

            for (int c = 0; c < columns.Count; ++c)
            {
                DataColumn dc = columns[c];
                var data = dc.ColumnName.ToString().PadRight(columnWidths[c]);
                appendFormatted(data, (c == count - 1));
            }
            sb.AppendLine();
            string s = string.Empty;
            sb.AppendLine(s.PadLeft(sb.ToString().Length, '-'));


            foreach (var row in rows)
            {
                for (int c = 0; c < count; ++c)
                {
                    DataColumn dc = columns[c];
                    var dataOb = (row as DataRow).Field<object>(dc.ColumnName);
                    var data = "NULL";
                    if (null != dataOb)
                    {
                        data = dataOb.ToString().PadRight(columnWidths[c]);
                    }
                    appendFormatted(data, (c == count - 1));
                }
                sb.AppendLine();
            }
            Debug.WriteLine(sb.ToString());
        }

        const int PADDING = 3;
        static List<int> GetColumnWidths(DataColumnCollection columns, DataRowCollection rows)
        {
            int count = columns.Count;

            //Set initial column widths
            var result = new List<int>(count);
            for (int i = 0; i < count; ++i)
            {
                DataColumn dc = columns[i];
                result.Add(dc.ColumnName.Length + PADDING);
            }

            //Calculate needed column widths
            foreach (var row in rows)
            {
                for (int c = 0; c < count; ++c)
                {
                    DataColumn dc = columns[c];
                    var dataOb = (row as DataRow).Field<object>(dc.ColumnName);
                    if (null != dataOb)
                    {
                        var width = dataOb.ToString().Length + PADDING;
                        result[c] = Math.Max(result[c], width);
                    }
                }
            }
            return result;
        }


    }//class
}//ns
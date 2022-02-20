using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;

namespace Charts.Services
{
    public class ChartService : IChartService
    {
        public MemoryStream CreateWaveform(double[,] result)
        {
            DataTable dTable;
            DataView dView;
            dTable = new DataTable();
            DataColumn column;
            DataRow row;
            //---
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Frequency";
            dTable.Columns.Add(column);
            //---
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Transmittance";
            dTable.Columns.Add(column);
            //---
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Phase spectrum";
            dTable.Columns.Add(column);
            //---
            column = new DataColumn();
            column.DataType = Type.GetType("System.Double");
            column.ColumnName = "Current I1";
            dTable.Columns.Add(column);
            int size = result.GetLength(0);
            //---
            for (int i = 0; i < size; i++)
            {
                row = dTable.NewRow();
                row["Frequency"] = result[i, 0];
                row["Transmittance"] = result[i, 1];
                row["Phase spectrum"] = result[i, 2];
                row["Current I1"] = result[i, 3];
                dTable.Rows.Add(row);
            }
            dView = new DataView(dTable);
            //---
            Chart chart01 = new Chart();
            chart01.Width = 800;
            chart01.Height = 800;
            chart01.ChartAreas.Add(new ChartArea("Magnitude"));
            chart01.ChartAreas.Add(new ChartArea("Phase"));
            chart01.ChartAreas.Add(new ChartArea("Current envelope I1"));

            chart01.ChartAreas["Magnitude"].AxisY.Title = "Amplitude spectrum";
            chart01.ChartAreas["Phase"].AxisY.Title = "Phase spectrum";
            chart01.ChartAreas["Current envelope I1"].AxisY.Title = "Current I1";
            chart01.DataBindTable(dView, "Frequency");
            //---
            chart01.Series["Transmittance"].ChartType = SeriesChartType.Line;
            chart01.Series["Phase spectrum"].ChartType = SeriesChartType.Line;
            chart01.Series["Current I1"].ChartType = SeriesChartType.Line;
            //---
            chart01.Series["Transmittance"].ChartArea = "Magnitude";
            chart01.Series["Phase spectrum"].ChartArea = "Phase";
            chart01.Series["Current I1"].ChartArea = "Current envelope I1";
            //---
            chart01.Titles.Add("Filter Transmittance: U2/U1 and Current envelope");
            chart01.ChartAreas[0].AxisX.Title = "Frequency [Hz]";
            chart01.ChartAreas[0].AxisX.LabelStyle.Format = "{#0.0}";
            chart01.ChartAreas[0].AxisX.Minimum = 0;
            chart01.ChartAreas[1].AxisX.Title = "Frequency [Hz]";
            chart01.ChartAreas[1].AxisX.LabelStyle.Format = "{#0.0}";
            chart01.ChartAreas[1].AxisX.Minimum = 0;
            chart01.ChartAreas[2].AxisX.Title = "Frequency [Hz]"; //
            chart01.ChartAreas[2].AxisX.LabelStyle.Format = "{#0.0}";
            chart01.ChartAreas[2].AxisX.Minimum = 0;
            //---Background
            chart01.Titles[0].Font =
                new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            chart01.ChartAreas[0].BackColor = System.Drawing.Color.White;
            chart01.ChartAreas[1].BackColor = System.Drawing.Color.White;

            //x-axis signatures
            chart01.ChartAreas[0].AxisX.TitleFont =
                new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            chart01.ChartAreas[1].AxisX.TitleFont =
                new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            chart01.ChartAreas[2].AxisX.TitleFont =
                new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            //y-axis signatures
            chart01.ChartAreas[0].AxisY.TitleFont =
                new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            chart01.ChartAreas[1].AxisY.TitleFont =
                new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);
            chart01.ChartAreas[2].AxisY.TitleFont =
               new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic);

            //color of chart
            chart01.Series["Transmittance"].Color = System.Drawing.Color.Blue;
            chart01.Series["Phase spectrum"].Color = System.Drawing.Color.Blue;
            chart01.Series["Current I1"].Color = System.Drawing.Color.Blue;
            //
            MemoryStream ms = new MemoryStream();
            chart01.SaveImage(ms);

            return ms;
        }
    }
}

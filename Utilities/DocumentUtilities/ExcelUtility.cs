using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities.DocumentUtilities
{
    public sealed class ExcelUtility
    {

        private ExcelFile excel = null;
        private ExcelWorksheet worksheet = null;

        #region General Setup

        /// <summary>
        /// Load a excel file from a path
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            if (excel != null) throw new Exception("Please call Close to shut down the current excel file first");
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            try
            {
                excel = ExcelFile.Load(path, LoadOptions.XlsDefault);
            }
            catch (Exception e)
            {
                try
                {
                    excel = ExcelFile.Load(path, LoadOptions.XlsxDefault);
                }
                catch (Exception e2)
                {
                    throw new Exception("Please ensure the excel file provided is from 97 or above");
                }
            }
        }

        /// <summary>
        /// Load a worksheet from the current excel file
        /// </summary>
        /// <param name="wsName"></param>
        public void LoadWorksheet(string wsName)
        {
            if (!ExcelIsLoaded()) throw new Exception("Please call Load to load a excel file first");
            worksheet = excel.Worksheets[wsName];
        }

        /// <summary>
        /// Load a worksheet by an index
        /// </summary>
        /// <param name="index"></param>
        public void LoadWorksheet(int index)
        {
            if (!ExcelIsLoaded()) throw new Exception("Please call Load to load a excel file first");
            worksheet = excel.Worksheets[index];
        }

        /// <summary>
        /// Find all the cells with the given value, return their coordination as [row,column]
        /// </summary>
        /// <param name="value"></param>
        public List<KeyValuePair<int, int>> FindCellByValue(string value)
        {
            if (!ExcelIsLoaded()) throw new Exception("Please call Load to load a excel file first");
            var rows = worksheet.Rows;
            var cells = new List<ExcelCell>();
            foreach (var r in rows)
            {
                cells.AddRange(r.AllocatedCells);
            }
            var found = cells.Where(m =>
            {
                var cv = m.Value;
                return string.Equals(value, cv);
            });
            return found.Count() > 0 ? found.Select(m => new KeyValuePair<int, int>(m.Row.Index, m.Column.Index)).ToList() : new List<KeyValuePair<int, int>>();

        }

        /// <summary>
        /// Find all the cells that contain the given value, return their coordination as [row,column]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<KeyValuePair<int, int>> FindCellsContainValue(string value)
        {
            if (!ExcelIsLoaded()) throw new Exception("Please call Load to load a excel file first");
            var rows = worksheet.Rows;
            var cells = new List<ExcelCell>();
            foreach (var r in rows)
            {
                cells.AddRange(r.AllocatedCells);
            }
            var found = cells.Where(m =>
            {
                var cv = m.Value;
                if (cv is string)
                {
                    if (cv.ToString().IndexOf(value) >= 0)
                    {
                        return true;
                    }
                }
                return false;
            });
            return found.Count() > 0 ? found.Select(m => new KeyValuePair<int, int>(m.Row.Index, m.Column.Index)).ToList() : new List<KeyValuePair<int, int>>();

        }

        #endregion

        #region Read

        public object ReadCell(string row, string col)
        {
            if (!WorksheetIsLoaded()) throw new Exception("Please call LoadWorksheet(string/int) to load a worksheet first");
            var cellVal = worksheet.Rows[row].Cells[col].Value;
            return cellVal;
        }

        public object ReadCell(int row, int col)
        {
            if (!WorksheetIsLoaded()) throw new Exception("Please call LoadWorksheet(string/int) to load a worksheet first");
            var cellVal = worksheet.Rows[row].Cells[col].Value;
            return cellVal;
        }

        #endregion

        #region Write

        public void WriteCell(string row, string col, object val)
        {
            if (!WorksheetIsLoaded()) throw new Exception("Please call LoadWorksheet(string/int) to load a worksheet first");
            worksheet.Rows[row].Cells[col].Value = val;
        }

        public void WriteCell(int row, int col, object val)
        {
            if (!WorksheetIsLoaded()) throw new Exception("Please call LoadWorksheet(string/int) to load a worksheet first");
            worksheet.Rows[row].Cells[col].Value = val;
        }

        #endregion

        #region Save

        public void Save(string path)
        {
            if (!ExcelIsLoaded()) throw new Exception("Please init an excel instance first");
            excel.Save(path);
        }

        public void SaveAllAsPdf(string path)
        {
            if (!ExcelIsLoaded()) throw new Exception("Please init an excel instance first");
            excel.Save(path, new PdfSaveOptions()
                {
                    SelectionType = SelectionType.EntireFile
                });
        }

        #endregion

        #region Private Helper

        private bool ExcelIsLoaded()
        {
            return excel != null;
        }

        private bool WorksheetIsLoaded()
        {
            return worksheet != null;
        }

        #endregion
    }
}

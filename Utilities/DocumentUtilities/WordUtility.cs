using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.Word;
using System.IO;

namespace KUtility.Utilities.DocumentUtilities
{
    public class WordUtility
    {

        private Application wordApplication;
        private Document wordDoc;
        private Fields fields;
        private Range range;
        object oMissing = System.Reflection.Missing.Value;

        #region Init & Dispose

        public void Read(string path)
        {
            if (!IsValid(path)) throw new InvalidOperationException("The provided path is not a valid word file");
            if (wordApplication == null) wordApplication = new Application();
            //disable msg
            wordApplication.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            if (wordDoc == null) wordDoc = new Document();
            var pathO = path as object;
            wordDoc = wordApplication.Documents.Add(ref pathO, ref oMissing, ref oMissing, ref oMissing);
        }

        public void Close()
        {
            ReleaseObject(wordDoc);
            wordDoc = null;
            ReleaseObject(wordApplication);
            wordApplication = null;
        }

        #endregion

        #region SELECTION

        public void SelectAll()
        {
            CheckDocLoaded();
            range = wordDoc.Content;
        }

        #endregion

        #region READ

        public List<string> GetAllFieldsValue()
        {
            LoadFields();
            var vals = new List<string>();
            foreach (Field f in fields)
            {
                vals.Add(f.Result.Text);
            }
            return vals;
        }

        public string GetFieldValue(int index)
        {
            LoadFields();
            return this.fields[index].Result.Text;
        }

        #endregion

        #region WRITE

        public void WriteField(int index, string value)
        {
            LoadFields();
            this.fields[index].Result.Text = value;
        }

        #endregion

        #region Style

        public void SetFontColor(WDColor color)
        {
            if (range == null) throw new Exception("Please Use Selection Functions to Select a Range first");
            range.Font.Color = (WdColor)color;
        }

        #endregion

        #region SAVE

        public void SaveAsPDF(string path)
        {
            CheckDocLoaded();
            var extension = Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension))
            {
                path = path + ".pdf";
            }
            
            wordDoc.SaveAs(path, WdSaveFormat.wdFormatPDF);
        }

        #endregion

        #region Private

        private bool IsValid(string path)
        {
            return Path.GetExtension(path).ToLower().IndexOf("doc") >= 0;
        }


        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        private void LoadFields()
        {
            if (fields == null)
            {
                if (wordDoc != null)
                {
                    this.fields = wordDoc.Fields;
                }
            }
        }

        private void CheckDocLoaded()
        {
            if (wordDoc == null) throw new Exception("Word Doc is null, please use Read to init");
        }
        #endregion
    }
}

public enum WDColor{
        wdColorAutomatic = -16777216,
        wdColorBlack = 0,
        wdColorDarkRed = 128,
        wdColorRed = 255,
        wdColorDarkGreen = 13056,
        wdColorOliveGreen = 13107,
        wdColorBrown = 13209,
        wdColorOrange = 26367,
        wdColorGreen = 32768,
        wdColorDarkYellow = 32896,
        wdColorLightOrange = 39423,
        wdColorLime = 52377,
        wdColorGold = 52479,
        wdColorBrightGreen = 65280,
        wdColorYellow = 65535,
        wdColorGray95 = 789516,
        wdColorGray90 = 1644825,
        wdColorGray875 = 2105376,
        wdColorGray85 = 2500134,
        wdColorGray80 = 3355443,
        wdColorGray75 = 4210752,
        wdColorGray70 = 5000268,
        wdColorGray65 = 5855577,
        wdColorGray625 = 6316128,
        wdColorDarkTeal = 6697728,
        wdColorPlum = 6697881,
        wdColorGray60 = 6710886,
        wdColorSeaGreen = 6723891,
        wdColorGray55 = 7566195,
        wdColorDarkBlue = 8388608,
        wdColorViolet = 8388736,
        wdColorTeal = 8421376,
        wdColorGray50 = 8421504,
        wdColorGray45 = 9211020,
        wdColorIndigo = 10040115,
        wdColorBlueGray = 10053222,
        wdColorGray40 = 10066329,
        wdColorTan = 10079487,
        wdColorLightYellow = 10092543,
        wdColorGray375 = 10526880,
        wdColorGray35 = 10921638,
        wdColorGray30 = 11776947,
        wdColorGray25 = 12632256,
        wdColorRose = 13408767,
        wdColorAqua = 13421619,
        wdColorGray20 = 13421772,
        wdColorLightGreen = 13434828,
        wdColorGray15 = 14277081,
        wdColorGray125 = 14737632,
        wdColorGray10 = 15132390,
        wdColorGray05 = 15987699,
        wdColorBlue = 16711680,
        wdColorPink = 16711935,
        wdColorLightBlue = 16737843,
        wdColorLavender = 16751052,
        wdColorSkyBlue = 16763904,
        wdColorPaleBlue = 16764057,
        wdColorTurquoise = 16776960,
        wdColorLightTurquoise = 16777164,
        wdColorWhite = 16777215,
}

using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUtility.Utilities.DocumentUtilities
{
    public sealed class PDFUtility
    {

        private PdfReader reader = null;
        private PdfConcatenate pdfConcat = null;
        private FileStream fileStream = null;

        #region Init & Disposal

        public void Read(string path)
        {
            if (reader != null) throw new Exception("Please close the current reading pdf first by using Close()");
            reader = new PdfReader(path);
            PdfReader.unethicalreading = true;
        }

        public void Close()
        {
            if (reader != null)
            {
                reader.Close();
                reader = null;
            }
        }

        /// <summary>
        /// Create a new PDF file, a default page with "Hello World!" will be inserted
        /// </summary>
        /// <param name="path">the full path of the pdf to be created</param>
        public string CreatePDF(string path)
        {
            if (reader != null) throw new Exception("Please close the current reading pdf first by using Close()");
            var extension = Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension)) path = path + ".pdf";
            else if (extension.IndexOf("pdf") < 0) throw new Exception("Invalid file extension. Make sure the path ends with .pdf");
            Document newPdf = new Document();
            PdfWriter.GetInstance(newPdf, new FileStream(path, FileMode.Create));
            newPdf.Open();
            //insert an empty paragraph to force creating a page
            newPdf.Add(new Paragraph("Hello World!"));
            newPdf.Close();
            return path;
        }

        #endregion


        #region READ

        public IDictionary<string, string> GetAllFormFields()
        {
            InitCheck();

            if (reader == null) throw new Exception("Please init the PdfReader by using Read(path) first");

            AcroFields pdfFormFields = reader.AcroFields;

            var fields = pdfFormFields.Fields;

            var result = fields.Select(m => new { key = m.Key, value = pdfFormFields.GetField(m.Key) }).ToDictionary(m => m.key, m => m.value);

            reader.Close();

            return result;
        }

        public IEnumerable<string> GetAllFormFieldNames()
        {
            InitCheck();

            var result = GetAllFormFields().Select(m => m.Key);

            reader.Close();

            return result;
        }

        public string GetFormFieldValue(string formFieldKey)
        {
            InitCheck();

            var fields = GetAllFormFields();

            try
            {
                var val = fields.SingleOrDefault(m => m.Key == formFieldKey).Value;
                return val;
            }
            catch (Exception e)
            {
                //error
                return string.Empty;
            }
        }

        /// <summary>
        /// If path is provided, read the page count of the file in the path. otherwise, read the file open in this Utility.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int PageCount(string path = null)
        {
            var reader = string.IsNullOrEmpty(path) ? this.reader : new PdfReader(path);
            var count = reader == null ? 0 : reader.NumberOfPages;
            if (!string.IsNullOrEmpty(path)) Dispose(reader);
            return count;
        }

        public List<int> GetPageIndexesByKeywords(List<string> keywords, bool caseSensitive)
        {
            InitCheck();
            var result = new List<int>();
            var pageCount = PageCount();
            for (var i = 1; i <= pageCount; i++)
            {
                var found = false;
                iTextSharp.text.pdf.parser.ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();
                var pContent = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, i);
                if (!caseSensitive)
                {
                    pContent = pContent.ToLower();
                }
                foreach (var k in keywords)
                {
                    var kw = !caseSensitive ? k.ToLower() : k;
                    if (pContent.IndexOf(k) >= 0)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        #endregion

        #region Page Manipulation

        /// <summary>
        /// Make sure the destination file is an brand new file because any content in it will be erased.
        /// Use AppendPages to add pages to it to construct a wanted file.
        /// If required to append pages to a existing pdf, please create a new pdf using CreatePDF and append the existing file, then continue to add other pages.
        /// </summary>
        /// <param name="path">The destination file to append to</param>
        public void SetDestinationPDF(string path)
        {
            if (this.pdfConcat != null) throw new Exception("PdfConcat is already set to a pdf file, please finish working with it first");
            fileStream = new FileStream(path, FileMode.Open);
            pdfConcat = new PdfConcatenate(fileStream);
        }

        /// <summary>
        /// append the selected pages from the source path file
        /// </summary>
        /// <param name="sourcePath">the file to get pages from</param>
        /// <param name="pages">a list of integer indicating the pages to append</param>
        /// <param name="moreToAppend">tell if there is any more pages to append, if yes, the PDFConcat will be left open, otherwise will be disposed</param>
        public void AppendPages(string sourcePath, List<int> pages, bool moreToAppend)
        {
            if (pdfConcat == null) throw new Exception("Please use SetDestinationPDF to set the PDF file to append to");
            if (string.IsNullOrEmpty(sourcePath)) return;
            if (Path.GetExtension(sourcePath).ToLower().IndexOf("pdf") < 0) throw new Exception("Source Path is not a pdf file.");

            var sourceReader = new PdfReader(sourcePath);
            if (pages != null)
            {
                sourceReader.SelectPages(pages);
            }
            pdfConcat.AddPages(sourceReader);
            Dispose(sourceReader);
            if (!moreToAppend)
            {
                Dispose(pdfConcat);
                Dispose(fileStream);
            }
        }

        /// <summary>
        /// append the whole pdf from the source path to the current destination file
        /// </summary>
        /// <param name="sourcePath">the file to get pages from</param>
        /// <param name="moreToAppend">tell if there is any more pages to append, if yes, the PDFConcat will be left open, otherwise will be disposed</param>
        public void AppendPages(string sourcePath, bool moreToAppend)
        {
            AppendPages(sourcePath, null, moreToAppend);
        }

        /// <summary>
        /// if the last AppendPages call has a true value for "moreToAppend", we need to explicitly call this method to dispose and close all stream and pdf concate
        /// </summary>
        public void FinishAppend()
        {
            if (pdfConcat != null) Dispose(pdfConcat);
            if (fileStream != null) Dispose(fileStream);
        }

        #endregion

        #region Private

        private void InitCheck()
        {
            if (reader == null) throw new Exception("Please init the PdfReader by using Read(path) first");
        }

        private void Dispose(object o)
        {
            if (o.GetType().GetMethod("Close") != null)
            {
                o.GetType().GetMethod("Close").Invoke(o, null);
            }
            o = null;
        }

        #endregion
    }
}

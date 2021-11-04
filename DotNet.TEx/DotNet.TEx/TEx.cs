using System;
using System.IO;
using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig.Exceptions;

namespace DotNet.TEx
{
    public class TEx : ITEx
    {

        public string GetTextFromPDF(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(filePath, "caminho do arquivo não pode ser vazio!");
            }

            try
            {
                var sb = new StringBuilder();

                using (var pdf = PdfDocument.Open(filePath))
                {
                    foreach (var page in pdf.GetPages())
                    {
                        // Either extract based on order in the underlying document with newlines and spaces.
                        var text = ContentOrderTextExtractor.GetText(page);

                        // Or based on grouping letters into words.
                        var otherText = string.Join(" ", page.GetWords());
                        sb.Append(otherText);

                        // Or the raw text of the page's content stream.
                        var rawText = page.Text;
                    }

                }

                return sb.ToString();
            }
            catch (PdfDocumentEncryptedException)
            {

                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string GetTextFromTxtFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(filePath, "caminho do arquivo não pode ser vazio!");
            }

            try
            {
                var texto = File.ReadAllText(filePath);

                return texto;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}

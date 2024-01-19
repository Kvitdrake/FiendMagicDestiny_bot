using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace FiendMagicDestiny_bot
{
    public class WordFileProcessor
    {
        private Application wordApp;
        private Microsoft.Office.Interop.Word.Document doc;
        public bool delParagraph = false;
        public WordFileProcessor()
        {
            wordApp = new Application();
            doc = wordApp.Documents.Add();
            // Устанавливаем стиль шрифта и размер
            doc.Content.Font.Name = "Times New Roman";
            doc.Content.Font.Size = 12;
            // Устанавливаем выравнивание по ширине
            //doc.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
            doc.Paragraphs.SpaceAfter = 0;
            doc.Paragraphs.SpaceBefore = 0;
            doc.Paragraphs.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;

            

            //doc.Paragraphs.Mirrorlindents
        }
        public void WriteToFile(string fileName, string data)
        {
            
                var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    writer.WriteLine(data);
                }
        }
        public void AddFormattedText(string text, string[] formattedWords)
        {
            Paragraph paragraph = doc.Content.Paragraphs.Add();             
            paragraph.Range.Text = text;
            
            paragraph.Range.ParagraphFormat.FirstLineIndent = wordApp.InchesToPoints(0.4f);
            foreach (string word in formattedWords)
            {
                Find find = paragraph.Range.Find;
                find.Text = word;

                find.Replacement.Text = word;
                find.Font.Bold = 1;
                find.Execute(Replace: WdReplace.wdReplaceAll);
            }
            if (delParagraph == true)
            {
                DelParagraph(paragraph);
            }
        }
        private void DelParagraph(Paragraph paragraph)
        {
            if (paragraph != null)
            {
                paragraph.CloseUp();
                Marshal.ReleaseComObject(paragraph.Range);
                Marshal.ReleaseComObject(paragraph);
                paragraph = null;
            }
        }
        public void SaveAndClose(string fileName)
        {
            try
            {
                var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
                doc.SaveAs(filePath, WdSaveFormat.wdFormatDocument);
            }
            finally
            {
                CloseDocument();
                QuitWordApplication();
                GC.SuppressFinalize(this);
            }
        }
        private void CloseDocument()
        {
            if (doc != null)
            {
                doc.Close();
                Marshal.ReleaseComObject(doc);
                doc = null;
            }
        }
        private void QuitWordApplication()
        {
            if (wordApp != null)
            {
                wordApp.Quit();
                Marshal.ReleaseComObject(wordApp);
                wordApp = null;
            }
        }
        public async System.Threading.Tasks.Task SendingFile(ITelegramBotClient botClient, long chatId, string fileName)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                InputFileStream inputFile = new InputFileStream(fileStream, Path.GetFileName(filePath));
                await botClient.SendDocumentAsync(chatId, inputFile);
            }
        }
        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

        }

        
    }
}

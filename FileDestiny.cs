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
    { //Microsoft.Office.Interop.Word.
        private Application wordApp;
        private Microsoft.Office.Interop.Word.Document doc;

        public WordFileProcessor()
        {
            wordApp = new Application();
            doc = wordApp.Documents.Add();
        }

        public void AddFormattedText(string text, string[] formattedWords)
        {
            Paragraph paragraph = doc.Content.Paragraphs.Add();
            paragraph.Range.Text = text;

            foreach (string word in formattedWords)
            {
                Find find = paragraph.Range.Find;
                find.Text = word;
                find.Replacement.Text = word;
                find.Font.Bold = 1;
                find.Execute(Replace: WdReplace.wdReplaceAll);
                Console.WriteLine(word + " ЭТО СТАЛО ЖИРНЫМ");
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
                doc.Close();
                Marshal.ReleaseComObject(doc);
                wordApp.Quit();
                Marshal.ReleaseComObject(wordApp);
            }
        }

        public void WriteToFile(string fileName, string data)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                writer.WriteLine(data);
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

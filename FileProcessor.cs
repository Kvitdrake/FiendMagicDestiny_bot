/*using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Telegram.Bot;
using Microsoft.Office.Interop.Word;

namespace FiendMagicDestiny_bot
{
    internal class WordFileProcessor
    {
        private Application wordApp;
        private Microsoft.Office.Interop.Word.Document doc;

        public WordFileProcessor()
        {
            wordApp = new Application();
            doc = wordApp.Documents.Add();
        }

        public void AddBoldText(string text, string[] formattedWords)
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
            }
        }
        public async System.Threading.Tasks.Task SendingFile(ITelegramBotClient botClient, long chatId, string fileName, WordFileProcessor processor)
        {
            processor.SaveAndClose(fileName);

            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
            {
                InputFileStream inputFile = new InputFileStream(fileStream, Path.GetFileName(FilePath));
                await botClient.SendDocumentAsync(chatId, inputFile);
            }
        }
        public void SaveAndClose(string fileName)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            doc.SaveAs(filePath, WdSaveFormat.wdFormatDocument);
            doc.Close();
            wordApp.Quit();
        }

        public void WriteToFile(string fileName, string data)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                writer.WriteLine(data);
            }
        }

        public void DeleteFile(string fileName)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }
    }
}*/
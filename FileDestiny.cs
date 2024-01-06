using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Color = DocumentFormat.OpenXml.Wordprocessing.Color;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using NPOI.XWPF.UserModel;
using Telegram.Bot;

namespace FiendMagicDestiny_bot
{
    internal class FileDestiny
    {
        /*public void WriteToFile(string fileName, string data)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using (StreamWriter writer = new StreamWriter(FilePath, true, Encoding.UTF8))
            {
                writer.WriteLine(data);
            }
        }*/
        public async Task SendingFile(ITelegramBotClient botClient, long chatId, string fileName)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
            {
                InputFileStream inputFile = new InputFileStream(fileStream, Path.GetFileName(FilePath));
                await botClient.SendDocumentAsync(chatId, inputFile);
            }
        }
        public void DeleteFile(string fileName)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            if (System.IO.File.Exists(FilePath))
                System.IO.File.Delete(FilePath);
        }
        public void HighlightWordsInDoc(string fileName, string[] wordsToHighlight)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                XWPFDocument document = new XWPFDocument(fs);

                foreach (var paragraph in document.Paragraphs)
                {
                    foreach (var run in paragraph.Runs)
                    {
                        foreach (string word in wordsToHighlight)
                        {
                            if (run.Text.Contains(word))
                            {
                                run.IsBold = true;  // Выделить жирным шрифтом
                                                    // Другие возможные форматирования:
                                                    // run.SetTextHighlightColor(XWPFColor.Yellow);  // Желтый цвет подсветки
                                                    // run.SetColor("FF0000");  // Красный цвет шрифта
                            }
                        }
                    }
                }

                document.Write(fs);
            }
        }
        /*public void WriteToFile(string filePath, string data)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                XWPFDocument document = new XWPFDocument();
                var paragraph = document.CreateParagraph();
                var run = paragraph.CreateRun();
                run.SetText(data);

                document.Write(fs);
            }
        }*/

    }
}

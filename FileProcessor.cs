using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Telegram.Bot;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Xceed.Words.NET;
using File = System.IO.File;

namespace FiendMagicDestiny_bot
{
    internal class FileWork
    {
        private static Dictionary<long, List<string>> fileData = new Dictionary<long, List<string>>();
        private static Dictionary<long, List<string>> addData = new Dictionary<long, List<string>>();

        public static async Task WriteToFile(long chatId, string text)
        {
            // Проверяем, есть ли уже данные для данного чата
            if (!fileData.ContainsKey(chatId))
            {
                fileData[chatId] = new List<string>();
            }

            // Добавляем текст в список данных для данного чата
            fileData[chatId].Add(text);
        }
        public static async Task WriteToFileAddition(long chatId, string text)
        {
            // Проверяем, есть ли уже данные для данного чата
            if (!addData.ContainsKey(chatId))
            {
                addData[chatId] = new List<string>();
            }

            // Добавляем текст в список данных для данного чата
            addData[chatId].Add(text);
        }

        public static async Task SendFormattedText( long chatId, string fileName)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            // Создаем новый документ
            using (var doc = DocX.Create(filePath))
            {
                // Проходимся по списку данных и добавляем их в документ
                foreach (var text in fileData[chatId])
                {
                    var paragraph = doc.InsertParagraph();

                    // Разделяем текст на отдельные слова
                    var words = text.Split(' ');

                    // Проходимся по каждому слову и проверяем, нужно ли его выделить
                    foreach (var word in words)
                    {
                        // Если слово или словосочетание нужно выделить, устанавливаем для него форматирование
                        if (word == "ЛЮДИ-НОСИТЕЛИ" && word == "АРХЕТИПА")
                        {
                            paragraph.Append(word).Bold();
                        }
                        else
                        {
                            paragraph.Append(word);
                        }

                        paragraph.Append(" ");
                    }
                }
            }
        }
        public async Task SendingFile(ITelegramBotClient botClient, long chatId, string fileName)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using (var fileStream = File.OpenRead(FilePath))
            {
                InputFileStream inputFile = new InputFileStream(fileStream, Path.GetFileName(FilePath));
                await botClient.SendDocumentAsync(chatId, inputFile);
            }
        }
        public void DeleteFile(string fileName, long chatId)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            if (System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
                fileData[chatId].Clear();
            }
        }
    }
}
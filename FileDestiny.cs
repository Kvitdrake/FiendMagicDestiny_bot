using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FiendMagicDestiny_bot
{
    internal class FileDestiny 
    {


        public void WriteToFile(string fileName, string data)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using(StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine(data);
            }
        }
        public async Task SendingFile(ITelegramBotClient botClient, long chatId, string fileName)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Open))
            {
                InputFileStream inputFile = new InputFileStream(fileStream, Path.GetFileName(FilePath));

                await botClient.SendDocumentAsync(chatId, inputFile);
            }
        }
        public void DeliteFile(string fileName)
        {
            var FilePath = Path.Combine(Environment.CurrentDirectory, fileName);
            if (System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
            }


        }

    }
}

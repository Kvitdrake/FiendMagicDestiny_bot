global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Microsoft.VisualBasic;
global using System.IO;
global using System.Runtime.InteropServices.ComTypes;
global using System.Security.Claims;
global using System.Threading;
global using Telegram.Bot;
global using Telegram.Bot.Exceptions;
global using Telegram.Bot.Polling;
global using Telegram.Bot.Types;
global using Telegram.Bot.Types.Enums;
global using Telegram.Bot.Types.ReplyMarkups;
global using Telegram.Bot.Args;
global using System.Data.Common;
global using System.Collections;
global using Telegram.Bots.Http;
using Telegram.Bots;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fiend.Magic_bot
{
    class Program
    {
        public static void Main()
        {
            var client = new TelegramBotClient("5900251255:AAFBjbPWnvf3BgEHKh58XdWx0Thxsj9Z_fQ");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var username = message.Chat.Username;
            if (update.Type == UpdateType.Message)
            {
                //if() короче, завтра пропроси Машу написать с разнх акков сообщения, узнай чат-ид её. 
                await botClient.SendTextMessageAsync(message.Chat.Id, "Твой персональный помощник для рассчёта предназначения. \r\t Введи отдельными сообщениями сначала имя, дату рождения, тг для связи, а потом просто через пробел все арканы человека.");
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Твой chatId: {chatId}");
                Console.WriteLine($"{username}   ||   {chatId}") ;
                //сообщение, которое придет послдним превращаешь в строку, строку делишь по символу пробела после чего конвертируешь части 
                return;
            }

        }

        private static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
    enum State
    {
        None, // новая запись, введи имя
        Name, // введи дату рождения
        Date_birth, // введи контакт
        TgContact, // введи арканы
        TarotCard, // 
        Work, // 
        Form, //
        Finish //
    }
}

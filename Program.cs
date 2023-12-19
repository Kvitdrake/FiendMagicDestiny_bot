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
using FiendMagicDestiny_bot;

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
        private static StateMachine _stateMachine;
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            var username = message.Chat.Username;
            if (update.Type == UpdateType.Message)
            {
                bool showId = false;
                bool showFirst = false; // не работает или я не понял
                if (_stateMachine == null)
                    _stateMachine = new StateMachine();

                //if() короче, завтра пропроси Машу написать с разнх акков сообщения, узнай чат-ид её. 
                await botClient.SendTextMessageAsync(message.Chat.Id, "Твой персональный помощник для рассчёта предназначения. \r\n ");
                showId = true; //описано выше

                await botClient.SendTextMessageAsync(message.Chat.Id, $"Твой chatId: {chatId}");
                Console.WriteLine($"{username}   ||   {chatId}");
                showId = true; //описано выше
                switch (_stateMachine.GetCurrentState(chatId))
                {
                    case State.None:
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Введи отдельными сообщениями сначала имя, дату рождения, тг для связи, а потом просто через пробел все арканы человека. \n\r Жду имя)");
                        _stateMachine.SetState(chatId, State.Name);
                        break;
                    case State.Name:
                        _stateMachine.SaveName(chatId, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Замечательно, введи дату рождения");
                        _stateMachine.SetState(chatId, State.Date_birth);
                        break;
                    case State.Date_birth:
                        _stateMachine.SaveDateDitth(chatId, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Соранил! Теперь контакт и переходим к арканам");
                        _stateMachine.SetState(chatId, State.TgContact);
                        break;
                    case State.TgContact:
                        _stateMachine.SaveContact(chatId, message.Text);
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Отлично. Данные человека у меня есть, присылай арканы (через пробел), пожалуйста)");
                        _stateMachine.SetState(chatId, State.TarotCard);
                        break;
                    case State.TarotCard:
                        _stateMachine.TransformationString(chatId, message.Text);
                        _stateMachine.BuilderList();
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Всё идет по плану, я уже наклепал файлик. ПРислать краткое описание или сразу перейдём к дополнениям?)");
                        _stateMachine.ResetState(chatId);
                        _stateMachine.SetState(chatId, State.None);
                        break;

                }
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

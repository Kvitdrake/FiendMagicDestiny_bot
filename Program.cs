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
            if (message != null && message.Chat != null)
            {
                var chatId = message.Chat.Id;

                var username = message.Chat.Username;
                if (update.Type == UpdateType.Message)
                {
                    if (_stateMachine == null)
                        _stateMachine = new StateMachine();

                    if (message.Text == "/start" && (chatId == 1002093832 || chatId == 184789122 || chatId == 5797888011))
                    {
                        FirstShow(botClient, update, token);

                    }
                    if (chatId == 1002093832 || chatId == 184789122 || chatId == 5797888011)
                    {
                        var replyKeyboardMarkup1 = new ReplyKeyboardMarkup(new[]
                                    {
                                    new KeyboardButton[]
                                        {
                                            MessageResponses.GenderG,
                                            MessageResponses.GenderM
                                        }
                                    });
                        switch (_stateMachine.GetCurrentState(chatId))
                        {

                            case State.None:
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Введи отдельными сообщениями сначала имя, пол человека, а потом просто через пробел все его арканы. \n\r Жду имя)");
                                _stateMachine.SetState(chatId, State.Name);
                                break;
                            case State.Name:
                                _stateMachine.SaveName(chatId, message.Text);
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Замечательно, теперь введи пол человека", replyMarkup: replyKeyboardMarkup1);
                                _stateMachine.SetState(chatId, State.Gender);
                                break;
                            case State.Gender:
                                _stateMachine.SaveGender(chatId, message.Text);
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Отлично. Данные человека у меня есть, присылай арканы (через пробел), пожалуйста)", replyMarkup: new ReplyKeyboardRemove());
                                _stateMachine.SetState(chatId, State.TarotCard);
                                break;
                            case State.TarotCard:
                                try
                                {
                                    await botClient.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, подожди несколько секунд и все будет готово.");
                                    _stateMachine.TransformationString(chatId, message.Text); //
                                    _stateMachine.BuilderList(chatId); //?
                                    await botClient.SendTextMessageAsync(message.Chat.Id, "Всё идет по плану, я уже наклепал файлик. Напиши свое дополнение и всё будет готово.");

                                    _stateMachine.SetState(chatId, State.Add);
                                }
                                catch (FormatException)
                                {
                                    await botClient.SendTextMessageAsync(message.Chat.Id, "Ты прям совсем что-то не то написала, перепроверь, пжл, и пришли заново");
                                }
                                catch
                                {
                                    await botClient.SendTextMessageAsync(message.Chat.Id, "Некорректное число");
                                }

                                break;
                            case State.Add:
                                _stateMachine.SaveAddition(chatId, message.Text);

                                await botClient.SendTextMessageAsync(message.Chat.Id, "Всё готово, лови файл)");
                                await _stateMachine.SendAddition(botClient, chatId);
                                _stateMachine.ResetState(chatId);
                                _stateMachine.SetState(chatId, State.None);

                                break;

                        }
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Упс! Я тебя не знаю, поэтому пользоваться этим ботом ты не можешь.");
                    }
                    return;

                }
            }
        }
        async static Task FirstShow(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(message.Chat.Id, "Твой персональный помощник для рассчёта предназначения. \r\n Для перезапуска бота");
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
        Gender,
        TarotCard, // 
        Add,
        Finish //
    }
}

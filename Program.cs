global using System.Text;
global using Telegram.Bot;
global using Telegram.Bot.Types;
global using Telegram.Bot.Types.Enums;
global using Telegram.Bot.Types.ReplyMarkups;
using FiendMagicDestiny_bot;
using MathNet.Numerics;
using NPOI.HPSF;

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
                                if (message.Text == "Новое предназначение")
                                {
                                    await botClient.SendTextMessageAsync(message.Chat.Id, $"{RandomName()}, жду имя человека)", replyMarkup: new ReplyKeyboardRemove());
                                    _stateMachine.SaveProcessor(chatId, new WordFileProcessor());
                                    _stateMachine.SetState(chatId, State.Name);
                                }
                                if (message.Text == "Новый прогноз на год")
                                {
                                    await botClient.SendTextMessageAsync(message.Chat.Id, "В разработке");
                                    _stateMachine.SetState(chatId, State.None);
                                }
                                break;
                            case State.Name:
                                _stateMachine.SaveName(chatId, message.Text);
                                _stateMachine.SaveFileName(chatId, $"{StateMachine._Name[chatId]}_Предназначение.doc");
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Замечательно, теперь выбери пол", replyMarkup: replyKeyboardMarkup1);
                                _stateMachine.SetState(chatId, State.Gender);
                                break;
                            case State.Gender:
                                _stateMachine.SaveGender(chatId, message.Text);
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Отлично. Данные человека у меня есть, присылай арканы (через пробел), пожалуйста)", replyMarkup: new ReplyKeyboardRemove());
                                _stateMachine.SaveArcManager(chatId, new ArcansManager());
                                _stateMachine.SetState(chatId, State.TarotCard);

                                _stateMachine.SaveProcessor2(chatId, new WordFileProcessor());
                                _stateMachine.SaveFileName2(chatId, $"ДОПОЛНЕНИЕ: {StateMachine._Name[chatId]}_Предназначение.doc");
                                break;
                            case State.TarotCard:
                                try
                                {
                                    await botClient.SendTextMessageAsync(message.Chat.Id, "Пожалуйста, подожди несколько секунд и все будет готово.\r\n⚡️🐎⚡️");
                                    _stateMachine.TransformationString(chatId, message.Text); 
                                    _stateMachine.BuilderList(chatId); 
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
                                var replyKeyboardMarkup2 = new ReplyKeyboardMarkup(new[]
                                    {
                                    new KeyboardButton[]
                                        {
                                            MessageResponses.Add
                                        },
                                    new KeyboardButton[]
                                        {
                                            MessageResponses.AddForYear
                                        }
                                    });
                                await botClient.SendTextMessageAsync(message.Chat.Id, "Всё готово, лови файл)", replyMarkup: replyKeyboardMarkup2);
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
            var replyKeyboardMarkup2 = new ReplyKeyboardMarkup(new[]
                                    {
                                    new KeyboardButton[]
                                        {
                                            MessageResponses.Add
                                        },
                                    new KeyboardButton[]
                                        {
                                            MessageResponses.AddForYear
                                        }
                                    });
            await botClient.SendTextMessageAsync(message.Chat.Id, "Твой персональный помощник для рассчёта предназначения ❤️ ✨ \r\n По вопросам - @soltias ", replyMarkup: replyKeyboardMarkup2);
        }
        private static string RandomName()
        {
            var random = new Random();
            int i = random.Next(1, 4);
            Dictionary<int, string> name = new Dictionary<int, string>()
            {
                {1, "Маша" },
                {2, "Королева белок" },
                {3, "Марганец" },
                {4, "Мартиша" }
            };
                return name[i];
        }
        private static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
    enum State
    {
        None,
        Start,
        Name,
        Date_birth,
        Gender,
        TarotCard,
        Add
    }
}

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
            var client = new TelegramBotClient("TOKEN*");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }
        private static StateMachine _stateMachine;
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message.Chat != null)
            {
                var chatId = message.Chat.Id;
                if (_stateMachine == null)
                    _stateMachine = new StateMachineRobot();

                switch (_stateMachine.GetCurrentState(chatId))
                {
                    case State.None:
                        if (update.Type == UpdateType.Message && _stateMachine.CheckId(update))
                        {
                            if ((message.Text == "/start" || message.Text == "/restart") && _stateMachine.CheckId(update)) //метод, проверяющий айди пользователя и возвращающий либо true, либо false
                                await FirstShow(botClient, update, token);

                            if ((message.Text == "Новое предназначение" || message.Text == "/density"))
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, $"{RandomName()}, жду имя человека)", replyMarkup: new ReplyKeyboardRemove());
                                _stateMachine.SaveProcessor(chatId, new WordFileProcessor());
                                _stateMachine.SetState(chatId, State.Name);
                            }
                            if (message.Text == "Новый прогноз на год" || message.Text == "/yearsdensity" || message.Text == "/pairdensity")
                            {
                                await botClient.SendTextMessageAsync(message.Chat.Id, "В разработке");
                                _stateMachine.SetState(chatId, State.None);
                            }
                        }
                        else
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, "Упс! Я тебя не знаю, поэтому пользоваться этим ботом ты не можешь.");
                        }
                        break;
                    case State.Name:
                        _stateMachine.SaveName(chatId, message.Text);
                        var replyKeyboardMarkup1 = new ReplyKeyboardMarkup(new[]
                            {
                                    new KeyboardButton[]
                                        {
                                            MessageResponses.GenderG,
                                            MessageResponses.GenderM
                                        }
                                    });
                        _stateMachine.SaveFileName(chatId, $"{StateMachineRobot._Name[chatId]}_Предназначение.doc");
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Замечательно, теперь выбери пол", replyMarkup: replyKeyboardMarkup1);
                        _stateMachine.SetState(chatId, State.Gender);
                        break;
                    case State.Gender:
                        _stateMachine.SaveGender(chatId, message.Text);
                        _stateMachine.SaveAddedArc(chatId, new HashSet<short?>());
                        _stateMachine.SaveAddedComb(chatId, new HashSet<string?>());

                        await botClient.SendTextMessageAsync(message.Chat.Id, "Отлично. Данные человека у меня есть, присылай арканы (через пробел), пожалуйста)", replyMarkup: new ReplyKeyboardRemove());
                        _stateMachine.SaveArcManager(chatId, new ArcansManager());
                        _stateMachine.SetState(chatId, State.TarotCard);
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
            await botClient.SendTextMessageAsync(message.Chat.Id, "Твой персональный помощник для рассчёта предназначения \r\n По вопросам - @soltias ", replyMarkup: replyKeyboardMarkup2);
        }
        private static string RandomName()
        {
            var random = new Random();
            int i = random.Next(1, 4);
            Dictionary<int, string> name = new Dictionary<int, string>()
            {
                {1, "Имя_заказчика1" }
                //остальные имена
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

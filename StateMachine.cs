using Fiend.Magic_bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiendMagicDestiny_bot
{
    internal class StateMachine : TaroArcans
    {
        private protected static Dictionary<long, State> userStates;
        private protected static Dictionary<long, string> _Name;
        private protected static Dictionary<long, string> _DateBirth;
        private protected static Dictionary<long, string> _Contact;
        public StateMachine()
        {
            userStates = new Dictionary<long, State>();
            _Name = new Dictionary<long, string>();
            _DateBirth = new Dictionary<long, string>();
            _Contact = new Dictionary<long, string>();
        }
        public State GetCurrentState(long chatId)
        {
            if (!userStates.ContainsKey(chatId))
                return State.None;

            return userStates[chatId];
        }
        public void SetState(long charId, State state)
        {
            userStates[charId] = state;
        }
        public void SaveName(long chatId, string name)
        {
            if (_Name.ContainsKey(chatId))
                _Name[chatId] = name;
            else
                _Name.Add(chatId, name);
        }
        public void SaveDateDitth(long chatId, string datebirth)
        {
            if (_DateBirth.ContainsKey(chatId))
                _DateBirth[chatId] = datebirth;
            else
                _DateBirth.Add(chatId, datebirth);
        }
        public void SaveContact(long chatId, string contact)
        {
            if (_Contact.ContainsKey(chatId))
                _Contact[chatId] = contact;
            else
                _Contact.Add(chatId, contact);
        }
        /*public void SaveTarostring(long chatId, string contact)
        {
            if (_Contact.ContainsKey(chatId))
                _Contact[chatId] = contact;
            else
                _Contact.Add(chatId, contact);
        }*/

        public void TransformationString(long chatId, string tarostring)
        {
            string allArcs = tarostring;
            string[] strArc = allArcs.Split(' ');
            Arcs = Array.ConvertAll(strArc, short.Parse); //сюда try-catch!!!
            foreach (var num in Arcs)
            {
                Console.WriteLine($"{num}");
            }
        }
        public void BuilderList()
        {
            arcans = new List<TaroArcans>()
            {

                    new TaroArcans{Id = 0, Name = "Дурак/Шут", Description = "*описание от тебя*"}, //тут же много тем. в TaroArcs сделай описание списком из других разделов
                    new TaroArcans{Id = 1, Name = "Маг", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 2, Name = "Жрица", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 3, Name = "Императрица", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 4, Name = "Император", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 5, Name = "Иерофант", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 6, Name = "Влюбленные", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 7, Name = "Колесница", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 8, Name = "Правосудие", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 9, Name = "Отшельник", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 10, Name = "Колесо фортуны", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 11, Name = "Сила", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 12, Name = "Повешенный", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 13, Name = "Смерть", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 14, Name = "Умеренность", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 15, Name = "Дьявол", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 16, Name = "Башня", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 17, Name = "Звезда", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 18, Name = "Луна", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 19, Name = "Солнце", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 20, Name = "Суд", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 21, Name = "Мир", Description = "*описание от тебя*"},
                    new TaroArcans{Id = 22, Name = "Дурак/Шут", Description = "*описание от тебя*"}
            };
            var result = from obj in arcans
                         join Id in Arcs on obj.Id equals Id
                         select obj;

            foreach (var obj in result)
            {
                Console.WriteLine($"{obj.Name} || {obj.Description}");
            }
        }
        public void ResetState(long chatId)
        {
            if (userStates.ContainsKey(chatId))
                userStates.Remove(chatId);
            if (_Name.ContainsKey(chatId))
                _Name.Remove(chatId);
            if (_DateBirth.ContainsKey(chatId))
                _DateBirth.Remove(chatId);
            if (_Contact.ContainsKey(chatId))
                _Contact.Remove(chatId);
        }
    }
}

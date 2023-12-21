using Fiend.Magic_bot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FiendMagicDestiny_bot
{
    internal class StateMachine : TaroArcans
    {
        private protected static Dictionary<long, State> userStates;
        private protected static Dictionary<long, string> _Name;
        private protected static Dictionary<long, string> _DateBirth;
        private protected static Dictionary<long, string> _Gender;
        private protected static Dictionary<long, string> _Contact;
        public StateMachine()
        {
            userStates = new Dictionary<long, State>();
            _Name = new Dictionary<long, string>();
            _DateBirth = new Dictionary<long, string>();
            _Gender = new Dictionary<long, string>();
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
        public void SaveGender(long chatId, string gender)
        {
            if (_Gender.ContainsKey(chatId))
            {
                if (gender == "👨Мужчина")
                    gender = "М";
                else if (gender == "👩Женщина")
                    gender = "Ж";
                else
                {
                    throw new Exception();
                }
                _Gender[chatId] = gender;
            }
            else
                _Gender.Add(chatId, gender);
        }

        public void TransformationString(long chatId, string tarostring)
        {
            string allArcs = tarostring;
            string[] strArc = allArcs.Split(new char[] {}, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                Arcs = Array.ConvertAll(strArc, short.Parse);
            }
            catch (FormatException ex) 
            {
                Console.WriteLine("ошибка при преобразовани строки" + ex);
                throw;
            }

            foreach (var num in Arcs)
            {
                if (num >= 23 || num < 0)
                {
                    Console.WriteLine($"Ошибка, число {num} < 0 или > 22");
                    throw new Exception ($"Некорректное число {num}");
                }

                Console.WriteLine($"{num}");
            }
        }
        public void BuilderList()
        {
            Dictionary<int, TaroArcans> Arcans = new Dictionary<int, TaroArcans>
            {
                { 0, new TaroArcans { Name = "Дурак/Шут", Description = "*описание от тебя*" } },
                { 1, new TaroArcans { Name = "Маг", Description = "*описание от тебя*" } },
                { 2, new TaroArcans { Name = "Жрица", Description = "*описание от тебя*" } },
                { 3, new TaroArcans { Name = "Императрица", Description = "*описание от тебя*" } },
                { 4, new TaroArcans { Name = "Император", Description = "*описание от тебя*" } },
                { 5, new TaroArcans { Name = "Иерофант", Description = "*описание от тебя*" } },
                { 6, new TaroArcans { Name = "Влюбленные", Description = "*описание от тебя*" } },
                { 7, new TaroArcans { Name = "Колесница", Description = "*описание от тебя*" } },
                { 8, new TaroArcans { Name = "Правосудие", Description = "*описание от тебя*" } },
                { 9, new TaroArcans { Name = "Отшельник", Description = "*описание от тебя*" } },
                { 10, new TaroArcans { Name = "Колесо фортуны", Description = "*описание от тебя*" } },
                { 11, new TaroArcans { Name = "Сила", Description = "*описание от тебя*" } },
                { 12, new TaroArcans { Name = "Повешенный", Description = "*описание от тебя*" } },
                { 13, new TaroArcans { Name = "Смерть", Description = "*описание от тебя*" } },
                { 14, new TaroArcans { Name = "Умеренность", Description = "*описание от тебя*" } },
                { 15, new TaroArcans { Name = "Дьявол", Description = "*описание от тебя*" } },
                { 16, new TaroArcans { Name = "Башня", Description = "*описание от тебя*" } },
                { 17, new TaroArcans { Name = "Звезда", Description = "*описание от тебя*" } },
                { 18, new TaroArcans { Name = "Луна", Description = "*описание от тебя*" } },
                { 19, new TaroArcans { Name = "Солнце", Description = "*описание от тебя*" } },
                { 20, new TaroArcans { Name = "Суд", Description = "*описание от тебя*" } },
                { 21, new TaroArcans { Name = "Мир", Description = "*описание от тебя*" } },
                { 22, new TaroArcans { Name = "Дурак/Шут", Description = "*описание от тебя*" } }
            };
            foreach (short obj in Arcs)
            {
                if (Arcans.ContainsKey(obj))
                {
                    Console.WriteLine( $"{obj}  -  {Arcans[obj].Name}   ||   {Arcans[obj].Description}");
                }

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
            if (_Gender.ContainsKey(chatId))
                _Gender.Remove(chatId);
        }
    }
}

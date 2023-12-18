using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FiendMagicDestiny_bot
{
    internal class FormTaro : TaroArcans
    {
        public FormTaro() { }
        

        public void BuilderList()
        {
            List<TaroArcans> AllTaroArc = new List<TaroArcans>()
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
            var result = from obj in AllTaroArc
                         join id in Arcs on obj.Id equals id
                         select obj;
            List<TaroArcans> BuiltList = new List<TaroArcans>();
            foreach (var obj in result)
            {
                Console.WriteLine(obj.Name);
                BuiltList.Add(obj);
            }
        }

    }
}

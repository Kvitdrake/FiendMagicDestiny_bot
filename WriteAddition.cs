
namespace FiendMagicDestiny_bot
{
    internal class WriterAddition
    {
        private Dictionary<short, string> Gifts = new Dictionary<short, string>()
        {
            [1] = "Маг - описание*",
            [2] = "Жрица - описание*",
            [3] = "Императрица - описание*",
            [4] = "Император - описание*",
            [5] = "Иерофант - описание*",
            [6] = "Влюблённые - описание*",
            [7] = "Колесница - описание*",
            [8] = "Справедливость - описание*",
            [9] = "Отшельник - описание*",
            [10] = "Колесо Фортуны - описание*",
            [11] = "Сила — описание*",
            [12] = "Повешенный – описание*",
            [13] = "Смерть - описание*",
            [14] = "Умеренность - описание*",
            [15] = "Дьявол - описание*",
            [16] = "Башня - описание*",
            [17] = "Звезда - описание*",
            [18] = "Луна - описание*",
            [19] = "Солнце - описание*",
            [20] = "Суд - описание*",
            [21] = "Мир - описание*",
            [22] = "Шут - описание*"
        };
        private Dictionary<short, string> Karma = new Dictionary<short, string>()
        {
        [1] = "Маг - описание*",
            [2] = "Жрица - описание*",
            [3] = "Императрица - описание*",
            [4] = "Император - описание*",
            [5] = "Иерофант - описание*",
            [6] = "Влюблённые - описание*",
            [7] = "Колесница - описание*",
            [8] = "Справедливость - описание*",
            [9] = "Отшельник - описание*",
            [10] = "Колесо Фортуны - описание*",
            [11] = "Сила — описание*",
            [12] = "Повешенный – описание*",
            [13] = "Смерть - описание*",
            [14] = "Умеренность - описание*",
            [15] = "Дьявол - описание*",
            [16] = "Башня - описание*",
            [17] = "Звезда - описание*",
            [18] = "Луна - описание*",
            [19] = "Солнце - описание*",
            [20] = "Суд - описание*",
            [21] = "Мир - описание*",
            [22] = "Шут - описание*"    
        };
        private Dictionary<short, string> MyDestiny = new Dictionary<short, string>()
        {
            [1] = "Маг - описание*",
            [2] = "Жрица - описание*",
            [3] = "Императрица - описание*",
            [4] = "Император - описание*",
            [5] = "Иерофант - описание*",
            [6] = "Влюблённые - описание*",
            [7] = "Колесница - описание*",
            [8] = "Справедливость - описание*",
            [9] = "Отшельник - описание*",
            [10] = "Колесо Фортуны - описание*",
            [11] = "Сила — описание*",
            [12] = "Повешенный – описание*",
            [13] = "Смерть - описание*",
            [14] = "Умеренность - описание*",
            [15] = "Дьявол - описание*",
            [16] = "Башня - описание*",
            [17] = "Звезда - описание*",
            [18] = "Луна - описание*",
            [19] = "Солнце - описание*",
            [20] = "Суд - описание*",
            [21] = "Мир - описание*",
            [22] = "Шут - описание*"
        };
        public WriterAddition()
        {
        }
        public void WriteMyDestiny(long chatId)
        {
            HashSet<short> addedArcans = new HashSet<short>();
            short count = 1;
            short[] Arc = new short[] { StateMachine.Arcs[0], StateMachine.Arcs[1], StateMachine.Arcs[2] };

            string mainData = $"Моё Предназначение: \r\n";
            StateMachine.WriteData(chatId, StateMachine.fileName[chatId], mainData);

            foreach (var num in Arc)
            {
                if (StateMachine.repeats.ContainsKey(num))
                {
                    short rep = StateMachine.repeats[num];
                    if (!addedArcans.Contains(num))
                    {
                        TaroArcans arcan = StateMachine.arcansManager[chatId].GetArcan(num);
                        string data = (rep != 1) ? $"{count}) {arcan.Name} ({rep}) " : $"{count}) {arcan.Name} ";
                        if (MyDestiny.ContainsKey(num))
                        {
                            data += $"- {GetDensity(num)} \r\n ";
                            StateMachine.WriteData(chatId, StateMachine.fileName[chatId], data);
                            addedArcans.Add(num);
                            count++;
                        }
                    }
                }
            }
        }
        public string GetDensity(short key)
        {
            if (MyDestiny.ContainsKey(key))
            {
                return MyDestiny[key].ToString();
            }
            else
            {
                return null;
            }
        }
        public string GetKarma(short key)
        {
            if (Karma.ContainsKey(key))
            {
                return Karma[key];
            }
            else
            {
                return null;
            }
        }
        public string GetGift(short key)
        {
            if (Gifts.ContainsKey(key))
            {
                return Gifts[key];
            }
            else
            {
                return null;
            }
        }
        public void WriteKarma(long chatId)
        {
            if (Karma.ContainsKey(StateMachine.Arcs[3]))
            {
                string data = $"\r\nКарма Предназначения \r\n {GetKarma(StateMachine.Arcs[3])} \r\n\r\n ";
                StateMachine.WriteData(chatId, StateMachine.fileName[chatId], data);
            }
        }

        public void WriteGift(long chatId)
        {
            if (Gifts.ContainsKey(StateMachine.Arcs[4]))
            {
                string data = $"Дар Предназначения \r\n {GetGift(StateMachine.Arcs[4])} ";
                StateMachine.WriteData(chatId, StateMachine.fileName[chatId], data);
            }
        }
    }
}

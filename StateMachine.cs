using Fiend.Magic_bot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FiendMagicDestiny_bot
{
    internal class StateMachine : FileDestiny
    {
        private protected static Dictionary<long, State> userStates;
        private protected static Dictionary<long, string> _Name;
        private protected static Dictionary<long, string> _DateBirth;
        private protected static Dictionary<long, string> _Gender;
        private protected static Dictionary<long, string> _Addition;
        public StateMachine()
        {
            userStates = new Dictionary<long, State>();
            _Name = new Dictionary<long, string>();
            _DateBirth = new Dictionary<long, string>();
            _Gender = new Dictionary<long, string>();
            _Addition = new Dictionary<long, string>();

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
        public void SaveGender(long chatId, string gender)
        {
            if (_Gender.ContainsKey(chatId))
            {
                _Gender[chatId] = gender;
            }
            else
                _Gender.Add(chatId, gender);
        }
        public void SaveAddition(long chatId, string addition)
        {
            if (_Addition.ContainsKey(chatId))
            {
                _Addition[chatId] = addition;
            }
            else
                _Addition.Add(chatId, addition);
        }
        public void ResetState(long chatId)
        {
            if (userStates.ContainsKey(chatId))
                userStates.Remove(chatId);
            if (_Name.ContainsKey(chatId))
                _Name.Remove(chatId);
            if (_Gender.ContainsKey(chatId))
                _Gender.Remove(chatId);
            if (_Addition.ContainsKey(chatId))
                _Addition.Remove(chatId);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------
        private Dictionary<int, TaroArcans> Arcans = new Dictionary<int, TaroArcans>();
        //private Dictionary<int, int> arcanOrder = new Dictionary<int, int>();
        public string indent = "\r\n\r\n";
        public string tab = "   ";

        public void TransformationString(long chatId, string tarostring)
        {
            string allArcs = tarostring;
            string[] strArc = allArcs.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                TaroArcans.Arcs = Array.ConvertAll(strArc, short.Parse);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("ошибка при преобразовани строки" + ex);
                throw;
            }

            foreach (var num in TaroArcans.Arcs)
            {
                if (num > 23 || num <= 0)
                {
                    Console.WriteLine($"Ошибка, число {num} < 0 или > 22");
                    throw new Exception($"Некорректное число {num}");
                }
                Console.WriteLine($"{num}");
            }
        }
        public void AddArcan(short number, string name, string descriptionM, string descriptionG)
        {
            /*if (!arcanOrder.ContainsKey(number))
            {
                arcanOrder.Add(number, arcanOrder.Count + 1);
            }*/
            TaroArcans arcan = new TaroArcans(name, descriptionM, descriptionG);
            Arcans.Add(number, arcan);
        }

        public void AddCombination(short arcanNumber1, short arcanNumber2, string combinationDescription)
        {
            if (Arcans.ContainsKey(arcanNumber1) && Arcans.ContainsKey(arcanNumber2))
            {
                string combinationKey = $"{arcanNumber1}-{arcanNumber2}";
                Arcans[arcanNumber1].Combinations.Add(combinationKey, combinationDescription);
            }
        }
        public void AddCombination(short arcanNumber1, short arcanNumber2, short arcanNumber3, string combinationDescription)
        {
            if (Arcans.ContainsKey(arcanNumber1) && Arcans.ContainsKey(arcanNumber2) && Arcans.ContainsKey(arcanNumber3))
            {
                string combinationKey = $"{arcanNumber1}-{arcanNumber2}-{arcanNumber3}";
                Arcans[arcanNumber1].Combinations.Add(combinationKey, combinationDescription);
            }
        }
        public async Task SendAddition(ITelegramBotClient botClient, long chatId)
        {
            string fileName = $"{StateMachine._Name[chatId]}_Предназначение.doc";
            string fileName2 = $"ДОПОЛНЕНИЕ: {StateMachine._Name[chatId]}_Предназначение.doc";

            string data = $"Дополнение: \r\n {_Addition[chatId]}";
            WriteToFile(fileName, data);
            WriteToFile(fileName2, data);
            await SendingFile(botClient, chatId, fileName);
            await SendingFile(botClient, chatId, fileName2);
            DeliteFile(fileName);

        }

        public void BuilderList(long chatId)
        {
            string fileName = $"{StateMachine._Name[chatId]}_Предназначение.doc";
            string instructions = "Правила работы с информацией.\r\n\r\n   По дате рождения я рассчитываю 9 арканов человека, соответствующих его дате рождения и влияющих на его личность всю жизнь.\r\n\r\n   Каждый аркан - одна из 9 частей личности, собирающаяся в итоге в уникальность отдельно взятого человека.\r\n\r\n   У каждого аркана есть уровни отработки. Большинство арканов я делю на “плюсовую отработку” и “минусовую”, хотя есть арканы с многоуровневой отработкой.\r\n   Плюсовая - это то, как НАДО отрабатывать аркан, чтобы кармические последствия были только положительными.\r\n\r\n   Минусовая влечёт за собой отрицательные кармические последствия (болезни, повторяющиеся негативные ситуации, токсичные эмоции, сложные отношения с людьми, внезапные потери денег и тп, и тд).\r\n\r\n   “Люди-архетипы аркана” - те, кто является наиболее ярким носителем аркана. Например, у аркана Суд это будет гробовщик или психоаналитик, у Иерофанта - священнослужитель (истинный, не те, что сейчас в церквях), у Императрицы - Мать с большой буквы.\r\n\r\n   ПРОФЕССИЯ.\r\n   Вы можете выбрать ЛЮБУЮ профессию ЛЮБОГО аркана, ниже перечисленного*, НО!\r\n   Вы должны понимать и стремиться к тому, чтобы остальные арканы покрывали выбранную деятельность. Чтобы не было выпадания какого-то аркана, иначе он автоматически уйдет в негатив.\r\n\r\n   Также я не сторонник того, чтобы профессию выбрать по четырем-пяти арканам, а хобби - по оставшимся, поскольку начнется раздвоение деятельности, влияющее негативно на сознание: работу я ненавижу, но и хобби тоже (что-то в этом духе).\r\n\r\n*если иное не указано в тексте.\r\n\r\n\r\n\r\n\r\n\r\n";
            AddArcan(1, "   МАГ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(2, "   ЖРИЦА", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(3, "   ИМПЕРАТРИЦА", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(4, "   ИМПЕРАТОР", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(5, "   ИЕРОФАНТ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(6, "   ВЛЮБЛЁННЫЕ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(7, "   КОЛЕСНИЦА", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(8, "   ПРАВОСУДИЕ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(9, "   ОТШЕЛЬНИК", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(10, "   КОЛЕСО ФОРТУНЫ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(11, "   СИЛА", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(12, "   ПОВЕШЕННЫЙ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(13, "   СМЕРТЬ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(14, "   УМЕРЕННОСТЬ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(15, "   Дьявол", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(16, "   БАШНЯ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(17, "   ЗВЕЗДА", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(18, "   ЛУНА", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(19, "   СОЛНЦЕ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(20, "   СУД", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(21, "   МИР", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");
            AddArcan(22, "   ШУТ", "*описание от тебя аркана для мужчины", "*описание от тебя аркана для женщины");

            AddCombination(1, 8, $"Правосудие и Маг - сильный ум, владение речью, умение грамотно и точно подать текст. Правдолюб, скорее всего, трудно замалчивать чувства. Резкость и критичность. {indent}");
            AddCombination(1, 3, $"Маг и Императрица - влияние женщин на идеи и идеалы. {indent}");
            AddCombination(1, 4, 4, $"Связка 2 Императора и Маг – много ума, дерзости, резкости, власти. Человек с сильной волей, склонный к диктатуре и категоричности. Большие заработки через речь, собственно Джон все основные деньги заработал на своих песнях.  {indent}");
            AddCombination(1, 6, $"Маг с Влюбленными – любовь от ума, много эмоций, манипуляции в любви. Умение создать нужный образ перед публикой.{indent}");
            AddCombination(1, 11, $"Маг и Сила - умение красиво говорить, очаровывать харизмой, силой. {indent}"); ;
            AddCombination(1, 11, 11, $"Связка 2 Силы и Маг - индивидуалист, очень сильная воля, харизма, сила, лидерские качества.{indent}");
            /*mashine.AddCombination(1, 8, "");
            mashine.AddCombination(1, 8, "");
            mashine.AddCombination(1, 8, "");
            mashine.AddCombination(1, 8, "");
            mashine.AddCombination(1, 8, "");
            mashine.AddCombination(1, 8, "");*/

            WriteToFile(fileName, instructions);
            foreach (short obj in TaroArcans.Arcs)
            {
                if (Arcans.ContainsKey(obj))
                {
                    TaroArcans arcan = Arcans[obj];
                    if (_Gender[chatId] == "👩Женщина")
                    {
                        string data = $"{arcan.Name} \r\n {arcan.DescriptionG}\r\n\r\n\r\n";
                        Console.WriteLine($"{arcan.Name}   ||    {arcan.DescriptionG}");
                        WriteToFile(fileName, data);
                    }
                    if (_Gender[chatId] == "👨Мужчина")
                    {
                        string data = $"{arcan.Name} \r\n {arcan.DescriptionM}\r\n\r\n\r\n";
                        Console.WriteLine($"{arcan.Name}   ||    {arcan.DescriptionM}");
                        WriteToFile(fileName, data);

                    }
                    /*if (arcanOrder.ContainsKey(obj) && arcanOrder[obj] == 1)
                    {*/
                    foreach (short obj2 in TaroArcans.Arcs)
                    {


                        string combinationKey = $"{obj}-{obj2}";
                        if (arcan.Combinations.ContainsKey(combinationKey))
                        {
                            string data = $"   {arcan.Combinations[combinationKey]}";
                            WriteToFile(fileName, data);
                            Console.WriteLine($"   {arcan.Combinations[combinationKey]}");

                        }




                    }
                    //}
                }
            }

        }
    }
}

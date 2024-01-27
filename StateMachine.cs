using Fiend.Magic_bot;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using DocumentFormat.OpenXml.Wordprocessing;

namespace FiendMagicDestiny_bot
{
    internal class StateMachine : FileWork
    {
        private protected static Dictionary<long, State> userStates;
        private protected static Dictionary<long, string> _Name;
        private protected static Dictionary<long, string> _DateBirth;
        private protected static Dictionary<long, string> _Gender;
        private protected static Dictionary<long, string> _Addition;
        private protected static short[] Arcs;
        private protected static string fileName;

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
        private Dictionary<short, TaroArcans> Arcans = new Dictionary<short, TaroArcans>();
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
            [22] = "Шут - описание*"};
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
            [22] = "Шут - описание*"};
        
        private string[] boldWords = new string[]
        {
            "ЛЮДИ-НОСИТЕЛИ АРХЕТИПА:", "Тип человека:", "талантливый художник."
        };
        public string indent = "\r\n\r\n";
        public void TransformationString(long chatId, string tarostring)
        {
            string allArcs = tarostring;
            string[] strArc = allArcs.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
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

                if (num > 23 || num <= 0)
                {
                    Console.WriteLine($"Ошибка, число {num} < 0 или > 22");
                    throw new Exception("Некорректное число");

                }
                Console.WriteLine($"{num}");
            }
        }
        public void AddArcan(short number, string name, string descriptionM, string descriptionG)
        {
            if (!Arcans.ContainsKey(number))
            {
                TaroArcans arcan = new TaroArcans(name, descriptionM, descriptionG);
                Arcans.Add(number, arcan);
            }
        }
        public void AddCombination(short arcanNumber1, short arcanNumber2, string combinationDescription)
        {

            if (Arcans.ContainsKey(arcanNumber1) && Arcans.ContainsKey(arcanNumber2))
            {
                short[] sortedArcanNumbers = { arcanNumber1, arcanNumber2 };
                Array.Sort(sortedArcanNumbers);

                string combinationKey = $"{sortedArcanNumbers[0]}-{sortedArcanNumbers[1]}";
                if (!Arcans[arcanNumber1].Combinations.ContainsKey(combinationKey))
                    Arcans[arcanNumber1].Combinations.Add(combinationKey, combinationDescription);
            }
        }
        public void AddCombination(short arcanNumber1, short arcanNumber2, short arcanNumber3, string combinationDescription)
        {
            if (Arcans.ContainsKey(arcanNumber1) && Arcans.ContainsKey(arcanNumber2) && Arcans.ContainsKey(arcanNumber3))
            {
                short[] sortedArcanNumbers = { arcanNumber1, arcanNumber2, arcanNumber3 };
                Array.Sort(sortedArcanNumbers);

                string combinationKey = $"{sortedArcanNumbers[0]}-{sortedArcanNumbers[1]}-{sortedArcanNumbers[2]}";

                if (!Arcans[arcanNumber1].Combinations.ContainsKey(combinationKey))
                {
                    Arcans[arcanNumber1].Combinations.Add(combinationKey, combinationDescription);
                }
            }
        }
        public async Task SendAddition(ITelegramBotClient botClient, long chatId)
        {
            string fileName2 = $"ДОПОЛНЕНИЕ: {StateMachine._Name[chatId]}_Предназначение.docx";

            string data = $"\r\n\r\n\r\n\r\n\r\n\r\n    Дополнение: \r\n {_Addition[chatId]}";
            await WriteToFileAddition(chatId, data);

            await SendingFile(botClient, chatId, fileName);
            await SendingFile(botClient, chatId, fileName2);
            DeleteFile(fileName, chatId);
            DeleteFile(fileName, chatId);
        }
        public async Task BuilderListAsync(long chatId)
        {

            fileName = $"{StateMachine._Name[chatId]}_Предназначение.docx";
            WriteInstructions(fileName, chatId);
            WriteArcs();
            WriteComb();
            HashSet<string> addedCombinations = new HashSet<string>();
            HashSet<short> addedArcans = new HashSet<short>();

            //запись инструкции
            Dictionary<short, short> repeats = CountingReps(Arcs);


            foreach (short obj in Arcs)
            {
                if (repeats.ContainsKey(obj))
                {
                    short rep = repeats[obj];
                    if (Arcans.ContainsKey(obj) && !addedArcans.Contains(obj))
                    {
                        
                        TaroArcans arcan = Arcans[obj];
                        string desc = (_Gender[chatId] == "👩Женщина") ? arcan.DescriptionG : arcan.DescriptionM;
                        string data = (rep != 1) ? $"{arcan.Name} ({rep}) \r\n {desc}\r\n\r\n\r\n" : $"{arcan.Name} \r\n {desc}\r\n\r\n\r\n";
                        WriteData(chatId, data);

                        addedArcans.Add(obj);

                        bool isFirstCom = true;
                        foreach (short obj2 in Arcs)
                        {
                            if ((repeats[obj] > 1 || (repeats[obj] == 1 && obj2 != obj)) && repeats.ContainsKey(obj2))
                            {
                                if (isFirstCom)
                                {
                                    string combinationKey = $"{obj}-{obj2}";
                                    if (arcan.Combinations.ContainsKey(combinationKey) && !addedCombinations.Contains(combinationKey))
                                    {
                                        string dataAdd = $"   {arcan.Combinations[combinationKey]}";
                                        WriteData(chatId, dataAdd);

                                        // Добавление сочетания в HashSet, чтобы избежать повторного добавления
                                        addedCombinations.Add(combinationKey);
                                        isFirstCom = false;
                                    }
                                }
                            }
                            foreach (short obj3 in Arcs)
                            {
                                if ((repeats[obj] > 1 || (repeats[obj] == 1 && obj2 != obj)) && repeats.ContainsKey(obj2)
                                    && (repeats[obj] > 1 || repeats[obj2] > 1 || (repeats[obj] == 1 && obj3 != obj && obj3 != obj2)) && repeats.ContainsKey(obj3))
                                {
                                    string combinationKey = $"{obj}-{obj2}-{obj3}";
                                    if (arcan.Combinations.ContainsKey(combinationKey) && !addedCombinations.Contains(combinationKey))
                                    {
                                        string dataAdd = $"   {arcan.Combinations[combinationKey]}";
                                        WriteData(chatId, dataAdd);
                                        // Добавление сочетания в HashSet, чтобы избежать повторного добавления
                                        addedCombinations.Add(combinationKey);

                                    }
                                }
                            }
                        }
                        try
                        {
                            await SendFormattedText(chatId, fileName);
                        }
                        catch
                        {
                            continue;
                        }

                    }
                }
            }
            WriteKarma(chatId);
            WriteGift(chatId);

            
        }
        public void WriteArcs()
        {
            AddArcan(1, "   МАГ", $"{Desc.Mag} МУЖЧИНА – МАГ: описание",
                                  $"{Desc.Mag} ЖЕНЩИНА – МАГ:описание");
            AddArcan(2, "   ЖРИЦА", $"{Desc.Pappet} МУЖЧИНА – ЖРИЦА:\описание",
                                    $"{Desc.Pappet} ЖЕНЩИНА – ЖРИЦА:описание.");
            AddArcan(3, "   ИМПЕРАТРИЦА", $"{Desc.Empress} МУЖЧИНА – ИМПЕРАТРИЦА:описание",
                                          $"{Desc.Empress} ЖЕНЩИНА – ИМПЕРАТРИЦА:описание");
            AddArcan(4, "   ИМПЕРАТОР", $"{Desc.Emperor} МУЖЧИНА – ИМПЕРАТОР:описание",
                                        $"{Desc.Emperor} ЖЕНЩИНА – ИМПЕРАТОР:описание");
            AddArcan(5, "   ИЕРОФАНТ", $"{Desc.Hierophant} МУЖЧИНА – ИЕРОФАНТ:описание",
                                       $"{Desc.Hierophant} ЖЕНЩИНА – ИЕРОФАНТ:описание");
            AddArcan(6, "   ВЛЮБЛЁННЫЕ", $"{Desc.Lovers} ",
                                         $"{Desc.Lovers} ");
            AddArcan(7, "   КОЛЕСНИЦА", $"{Desc.Chariot}",
                                        $"{Desc.Chariot}");
            AddArcan(8, "   ПРАВОСУДИЕ", $"{Desc.Justice}",
                                         $"{Desc.Justice}");
            AddArcan(9, "   ОТШЕЛЬНИК", $"{Desc.Hermit}",
                                        $"{Desc.Hermit}");
            AddArcan(10, "   КОЛЕСО ФОРТУНЫ", $"{Desc.WheelOfFortune}",
                                              $"{Desc.WheelOfFortune}");
            AddArcan(11, "   СИЛА", $"{Desc.Strength}",
                                    $"{Desc.Strength}");
            AddArcan(12, "   ПОВЕШЕННЫЙ", $"{Desc.HangedMan}",
                                          $"{Desc.HangedMan}");
            AddArcan(13, "   СМЕРТЬ", $"{Desc.Death}",
                                      $"{Desc.Death}");
            AddArcan(14, "   УМЕРЕННОСТЬ", $"{Desc.Temperance}",
                                           $"{Desc.Temperance}");
            AddArcan(15, "   Дьявол", $"{Desc.Devil}",
                                      $"{Desc.Devil}");
            AddArcan(16, "   БАШНЯ", $"{Desc.Tower}",
                                     $"{Desc.Tower}");
            AddArcan(17, "   ЗВЕЗДА", $"*описание от тебя аркана для мужчины",
                                      $"*описание от тебя аркана для женщины");
            AddArcan(18, "   ЛУНА", $"*описание от тебя аркана для мужчины",
                                    $"*описание от тебя аркана для женщины");
            AddArcan(19, "   СОЛНЦЕ", $"*описание от тебя аркана для мужчины",
                                      $"*описание от тебя аркана для женщины");
            AddArcan(20, "   СУД", $"*описание от тебя аркана для мужчины",
                                   $"*описание от тебя аркана для женщины");
            AddArcan(21, "   МИР", $"*описание от тебя аркана для мужчины",
                                   $"*описание от тебя аркана для женщины");
            AddArcan(22, "   ШУТ", $"*описание от тебя аркана для мужчины",
                                   $"*описание от тебя аркана для женщины");
        }
        public void WriteComb()
        {
            AddCombination(1, 8, $"Правосудие и Маг - *описание*");
            AddCombination(1, 3, $"Маг и Императрица - *описание*");
            AddCombination(1, 4, 4, $"Связка 2 Императора и Маг – *описание*{indent}");
            AddCombination(1, 6, $"Маг с Влюбленными – *описание*{indent}");
            AddCombination(1, 11, $"*описание*{indent}");
            AddCombination(1, 13, $"Маг и Сила - *описание*{indent}");
            AddCombination(1, 13, 12, $"*описание* {indent}");
            AddCombination(1, 11, 11, $"*описание*{indent}");
            AddCombination(1, 8, 8, $"2 Правосудия и Маг – *описание* {indent}");
        //и так много раз
        //переделывается в целях экономии и увеличения производительности
        
        }
        public void WriteKarma(long chatId)
        {
            if (Karma.ContainsKey(Arcs[3]))
            {
                string data = $"Карма Презназначения \r\n\r\n {Karma[Arcs[3]]} ";
                WriteToFile(chatId, data);
            }
        }
        public void WriteGift(long chatId)
        {
            if (Gifts.ContainsKey(Arcs[4]))
            {
                string data = $"Дар Презназначения \r\n\r\n {Gifts[Arcs[4]]} ";
                WriteToFile(chatId, data);
            }
        }
        public static Dictionary<short, short> CountingReps(short[] nums)
        {
            Dictionary<short, short> countMap = new Dictionary<short, short>();
            foreach(short num in nums)
            {
                if (countMap.ContainsKey(num))
                {
                    countMap[num]++;
                }
                else
                {
                    countMap[num] = 1;
                }   
            }
            return countMap;
        }
        public void WriteInstructions(string fileName, long chatId)
        {
            string instructions = $"Правила работы с информацией.";
            WriteToFile(chatId, instructions);
            
        }
        public void WriteData(long chatId, string data)
        {
            Console.WriteLine(data);
            WriteToFile(chatId, data);
        }

    }
}

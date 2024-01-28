using Fiend.Magic_bot;

namespace FiendMagicDestiny_bot.StateMachine
{
    internal class StateMachineRobot : StateMashineBody
    {
        public static short[] Arcs;
        public StateMachineRobot()
        {
            new StateMashineBody();
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
        public void SaveProcessor(long chatId, WordFileProcessor name)
        {
            if (processor.ContainsKey(chatId))
                processor[chatId] = name;
            else
                processor.Add(chatId, name);
        }
        public void SaveProcessor2(long chatId, WordFileProcessor name)
        {
            if (processor2.ContainsKey(chatId))
                processor2[chatId] = name;
            else
                processor2.Add(chatId, name);
        }
        public void SaveArcManager(long chatId, ArcansManager name)
        {
            if (arcansManager.ContainsKey(chatId))
                arcansManager[chatId] = name;
            else
                arcansManager.Add(chatId, name);
        }
        public void SaveFileName(long chatId, string name)
        {
            if (fileName.ContainsKey(chatId))
                fileName[chatId] = name;
            else
                fileName.Add(chatId, name);
        }
        public void SaveAddedArc(long chatId, HashSet<short?> shorts)
        {
            if (addedArcans.ContainsKey(chatId))
                addedArcans[chatId] = shorts;
            else
                addedArcans.Add(chatId, shorts);

        }
        public void SaveAddedComb(long chatId, HashSet<string?> strings)
        {
            if (addedCombinations.ContainsKey(chatId))
                addedCombinations[chatId] = strings;
            else
                addedCombinations.Add(chatId, strings);
        }
        /*public void SaveFileName2(long chatId, string name)
        {
            if (fileName2.ContainsKey(chatId))
                fileName2[chatId] = name;
            else
                fileName2.Add(chatId, name);
        }*/

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
            if (fileName.ContainsKey(chatId))
                fileName.Remove(chatId);
            /*if (fileName2.ContainsKey(chatId))
                fileName2.Remove(chatId);*/
            if (processor.ContainsKey(chatId))
                processor.Remove(chatId);
            if (processor2.ContainsKey(chatId))
                processor2.Remove(chatId);
            if (arcansManager.ContainsKey(chatId))
                arcansManager.Remove(chatId);
            if (addedArcans .ContainsKey(chatId))
                addedArcans.Remove(chatId);
            if (addedCombinations.ContainsKey(chatId))
                addedCombinations.Remove(chatId);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------
        public static Dictionary<short, short> repeats = new Dictionary<short, short>();

        private static string[] boldWords = new string[]
        {
            "ЛЮДИ-НОСИТЕЛИ", "АРХЕТИПА:"
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
                //Console.WriteLine("ошибка при преобразовани строки" + ex);
                throw;
            }

            foreach (var num in Arcs)
            {

                if (num > 23 || num <= 0)
                {
                    //Console.WriteLine($"Ошибка, число {num} < 0 или > 22");
                    throw new Exception("Некорректное число");

                }
                //Console.WriteLine($"{num}");
            }
        }
        public async Task SendAddition(ITelegramBotClient botClient, long chatId)
        {
            //fileName2[chatId] = $"ДОПОЛНЕНИЕ: {StateMachine._Name[chatId]}_Предназначение.doc";
            string data = $"\r\n\r\n\r\n    Дополнение: \r\n {_Addition[chatId]}"; //где-то теряется имя нормальное и не находится файл по пути
            processor[chatId].delParagraph = true;
            //processor2[chatId].delParagraph = true;

            WriteData(chatId, fileName[chatId], data);
            //WriteData(fileName2[chatId], data, processor2[chatId]);

            processor[chatId].SaveAndClose(fileName[chatId]);
            //processor2[chatId].SaveAndClose(fileName2[chatId]);

            await processor[chatId].SendingFile(botClient, chatId, fileName[chatId]);
            //await processor2[chatId].SendingFile(botClient, chatId, fileName2[chatId]);

            processor[chatId].DeleteFile(fileName[chatId]);
            //processor2[chatId].DeleteFile(fileName2[chatId]);

        }
        public void BuilderList(long chatId)
        {
            repeats = CountingReps(Arcs);
            WriteInstructions(chatId);
            arcansManager[chatId].WriteComb();
            foreach (short obj in Arcs)
            {
                if (repeats.ContainsKey(obj))
                {
                    short rep = repeats[obj];
                    if (!addedArcans[chatId] .Contains(obj))
                    {
                        TaroArcans arcan = arcansManager[chatId].GetArcan(obj);
                        string desc = (_Gender[chatId] == "👩Женщина") ? arcan.DescriptionG : arcan.DescriptionM;
                        string data = (rep != 1) ? $"\r\n\r\n\r\n{arcan.Name} ({rep}) \r\n {desc}\r\n\r\n\r\n" : $"\r\n\r\n\r\n{arcan.Name} \r\n {desc}\r\n\r\n\r\n";
                        WriteData(chatId, fileName[chatId], data);
                        addedArcans[chatId].Add(obj);

                        foreach (short obj2 in Arcs)
                        {
                            CombinationHandler(obj, obj2, chatId);
                            foreach (short obj3 in Arcs)
                            {
                                CombinationHandler(obj, obj2, obj3, chatId);
                                foreach (short obj4 in Arcs)
                                {
                                    CombinationHandler(obj, obj2, obj3, obj4, chatId);
                                }
                            }
                        }
                    }

                }
            }
            WriterAddition writer = new WriterAddition();
            writer.WriteMyDestiny(chatId);
            writer.WriteKarma(chatId);
            writer.WriteGift(chatId);
        }


        public static Dictionary<short, short> CountingReps(short[] nums)
        {
            Dictionary<short, short> countMap = new Dictionary<short, short>();
            foreach (short num in nums)
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
        public void CombinationHandler(short obj, short obj2, long chatId)
        {
            if ((repeats[obj] > 1 || (repeats[obj] == 1 && obj2 != obj)) && repeats.ContainsKey(obj2))
            {
                string combinationKey = $"{obj}-{obj2}";
                if (ArcansManager.Arcans[obj].Combinations.ContainsKey(combinationKey) && !addedCombinations[chatId].Contains(combinationKey))
                {
                    string dataAdd = $"   {ArcansManager.Arcans[obj].Combinations[combinationKey]}";
                    WriteData(chatId, fileName[chatId], dataAdd);
                    addedCombinations[chatId].Add(combinationKey);
                    // = false;
                }
            }
        }
        public void CombinationHandler(short obj, short obj2, short obj3, long chatId)
        {
            if ((repeats[obj] > 1 || (repeats[obj] == 1 && obj2 != obj)) && repeats.ContainsKey(obj2)
                                                                   && ((repeats[obj] > 1 && (obj2 == obj || obj == obj3)) || (repeats[obj2] > 1 && (obj2 == obj || obj2 == obj3)) || (repeats[obj] == 1 && obj3 != obj && obj3 != obj2 && obj != obj2)) && repeats.ContainsKey(obj3))
            {
                string combinationKey = $"{obj}-{obj2}-{obj3}";
                if (ArcansManager.Arcans[obj].Combinations.ContainsKey(combinationKey) && !addedCombinations[chatId].Contains(combinationKey))
                {
                    string dataAdd = $"   {ArcansManager.Arcans[obj].Combinations[combinationKey]}";
                    WriteData(chatId, fileName[chatId], dataAdd);
                    addedCombinations[chatId].Add(combinationKey);
                }
            }
        }
        public void CombinationHandler(short obj, short obj2, short obj3, short obj4, long chatId)
        {
            if ((repeats[obj] > 1 || (repeats[obj] == 1 && obj2 != obj)) && repeats.ContainsKey(obj2)
                                                         && ((repeats[obj] > 1 && (obj2 == obj || obj == obj3)) || (repeats[obj2] > 1 && (obj2 == obj || obj2 == obj3)) || (repeats[obj] == 1 && obj3 != obj && obj3 != obj2 && obj != obj2)) && repeats.ContainsKey(obj3)
                                    && (((repeats[obj] > 1 && repeats[obj] < 4) && (obj2 == obj || obj == obj3 || obj == obj4)) || ((repeats[obj2] > 1 && repeats[obj2] < 4) && (obj2 == obj || obj2 == obj3 || obj2 == obj4)) || ((repeats[obj3] > 1 && repeats[obj3] < 4) && (obj3 == obj || obj2 == obj3 || obj3 == obj4)) || (repeats[obj] == 1 && obj4 != obj && obj3 != obj2 && obj4 != obj2) || repeats[obj] == 4) && repeats.ContainsKey(obj4))
            {
                string combinationKey = $"{obj}-{obj2}-{obj3}-{obj4}";
                if (ArcansManager.Arcans[obj].Combinations.ContainsKey(combinationKey) && !addedCombinations[chatId].Contains(combinationKey))
                {
                    string dataAdd = $"   {ArcansManager.Arcans[obj].Combinations[combinationKey]}";
                    WriteData(chatId, fileName[chatId], dataAdd);
                    addedCombinations[chatId].Add(combinationKey);
                }
            }
        }
        public void WriteInstructions(long chatId)
        {
            string instructions = $"Правила работы с информацией.";
            processor[chatId].WriteToFile(fileName[chatId], instructions);
            processor[chatId].AddFormattedText(instructions, boldWords);

        }
        public static void WriteData(long chatId, string fileName, string data)// в будущем сделай интерфейсом
        {
            processor[chatId].WriteToFile(fileName, data);
            processor[chatId].AddFormattedText(data, boldWords);
        }
        public static void WriteData(string fileName, string data, WordFileProcessor processor)// в будущем сделай интерфейсом
        {
            processor.WriteToFile(fileName, data);
            processor.AddFormattedText(data, boldWords);
        }
    }
}

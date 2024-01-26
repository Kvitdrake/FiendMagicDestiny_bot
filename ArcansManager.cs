
namespace FiendMagicDestiny_bot
{
    internal class ArcansManager
    {
        private Dictionary<short, TaroArcans> Arcans = new Dictionary<short, TaroArcans>()
        {
            {1, new TaroArcans ("МАГ", $"{Desc.Mag} \r\n\r\nМУЖЧИНА – МАГ:\r\n*описание*\r\n",
                               $"{Desc.Mag} \r\n\r\nЖЕНЩИНА – МАГ:\r\n*описание*\r\n") },
            //и так до самого конца
        { 22, new TaroArcans("ШУТ", $"{Desc.Fool} \r\n\r\nМУЖЧИНА – ШУТ:*описание*",
                                   $"{Desc.Fool} \r\n\r\nЖЕНЩИНА – ШУТ:*описание*")}
        };
        public TaroArcans GetArcan(short number)
        {
            if(Arcans.ContainsKey(number))
            {
               return Arcans[number];
            }
            else
            {
                return null;
            }
        }
        public Dictionary<string, string> Combinations = new Dictionary<string, string>();
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
        public void AddCombination(short arcanNumber1, short arcanNumber2, short arcanNumber3, short arcanNumber4, string combinationDescription)
        {
            if (Arcans.ContainsKey(arcanNumber1) && Arcans.ContainsKey(arcanNumber2) && Arcans.ContainsKey(arcanNumber3) && Arcans.ContainsKey(arcanNumber4))
            {
                short[] sortedArcanNumbers = { arcanNumber1, arcanNumber2, arcanNumber3, arcanNumber4 };
                Array.Sort(sortedArcanNumbers);

                string combinationKey = $"{sortedArcanNumbers[0]}-{sortedArcanNumbers[1]}-{sortedArcanNumbers[2]}-{sortedArcanNumbers[3]}";

                if (!Arcans[arcanNumber1].Combinations.ContainsKey(combinationKey))
                {
                    Arcans[arcanNumber1].Combinations.Add(combinationKey, combinationDescription);
                }
            }
        }

        public string indent = "\r\n\r\n";
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
        public string GetCombination(string combination)
        {
            if (Combinations.ContainsKey(combination))
            {
                return Combinations[combination];
            }
            else
            {
                return null;
            }
        }
    }
}

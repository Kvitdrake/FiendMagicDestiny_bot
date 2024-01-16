namespace FiendMagicDestiny_bot
{
    internal class TaroArcans
    {
        public string Name { get; set; }
        public string DescriptionM { get; set; }
        public string DescriptionG { get; set; }
        public Dictionary<string, string> Combinations { get; set; }
        public TaroArcans(string name, string descriptionM, string descriptionG)
        {
            Name = name;
            DescriptionM = descriptionM;
            DescriptionG = descriptionG;
            Combinations = new Dictionary<string, string>();
        }
    }
}
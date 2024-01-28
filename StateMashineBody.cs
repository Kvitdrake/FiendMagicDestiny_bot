using Fiend.Magic_bot;

namespace FiendMagicDestiny_bot.StateMachine
{
    internal class StateMashineBody
    {
        private protected static Dictionary<long, State> userStates = new Dictionary<long, State>();
        public static Dictionary<long, string> _Name = new Dictionary<long, string>();
        private protected static Dictionary<long, string> _DateBirth = new Dictionary<long, string>();
        private protected static Dictionary<long, string> _Gender = new Dictionary<long, string>();
        private protected static Dictionary<long, string> _Addition = new Dictionary<long, string>();
        public static Dictionary<long, string> fileName = new Dictionary<long, string>();
        private protected static Dictionary<long, WordFileProcessor> processor = new Dictionary<long, WordFileProcessor>();
        private protected static Dictionary<long, WordFileProcessor> processor2 = new Dictionary<long, WordFileProcessor>();
        public static Dictionary<long, ArcansManager> arcansManager = new Dictionary<long, ArcansManager>();
        public static Dictionary<long, HashSet<short?>> addedArcans = new Dictionary<long, HashSet<short?>>();
        public static Dictionary<long, HashSet<string?>> addedCombinations = new Dictionary<long, HashSet<string?>>();


        public StateMashineBody()
        {
            InitializeDictionary(userStates);
            InitializeDictionary(_Name);
            InitializeDictionary(_DateBirth);
            InitializeDictionary(_Gender);
            InitializeDictionary(_Addition);
            InitializeDictionary(fileName);
            InitializeDictionary(processor);
            InitializeDictionary(processor2);
            InitializeDictionary(arcansManager);
            InitializeDictionary(addedArcans);
            InitializeDictionary(addedCombinations);

        }

        private void InitializeDictionary<T>(Dictionary<long, T> dictionary)
        {
            dictionary = new Dictionary<long, T>();
        }
    }
}

namespace FiendMagicDestiny_bot
{
    public class Desc
    {
        private protected string desc {  get; set; }
        public Desc(string desc)
        {
            this.desc = desc;
        }
        public static Desc Mag = new Desc("*описание*");
        public static Desc Pappet = new Desc("*описание*");
        public static Desc Empress = new Desc("*описание*");
        public static Desc Emperor = new Desc("*описание*");
        public static Desc Hierophant = new Desc("*описание*");
        public static Desc Lovers = new Desc("*описание*");
        public static Desc Chariot = new Desc("*описание*");
        public static Desc Justice = new Desc("*описание*");
        public static Desc Hermit = new Desc("*описание*");
        public static Desc WheelOfFortune = new Desc("*описание*");
        public static Desc Strength = new Desc("*описание*");
        public static Desc HangedMan = new Desc("*описание*");
        public static Desc Death = new Desc("*описание*");
        public static Desc Temperance = new Desc("*описание*");
        public static Desc Devil = new Desc("*описание*");
        public static Desc Tower = new Desc("*описание*");
        public static Desc Star = new Desc("*описание*");
        public static Desc Moon = new Desc("*описание*");
        public static Desc Sun = new Desc("*описание*");
        public static Desc Judgement = new Desc("*описание*");
        public static Desc World = new Desc("*описание*");
        public static Desc Fool = new Desc("*описание*");
        public override string ToString()
        {
            return desc;
        }
    }
}

namespace FiendMagicDestiny_bot
{
     class MessageResponses
    {
        public static KeyboardButton Add => new KeyboardButton("Новое предназначение");
        public static KeyboardButton AddForYear => new KeyboardButton("Новый прогноз на год");
        public static KeyboardButton Edit => new KeyboardButton("Изменить");
        public static KeyboardButton Save => new KeyboardButton("Сохранить");
        public static KeyboardButton GenderM => new KeyboardButton("👨Мужчина");
        public static KeyboardButton GenderG => new KeyboardButton("👩Женщина");
        public static KeyboardButton NotSave => new KeyboardButton("Не сохранять");

        public static KeyboardButton Test => new KeyboardButton("Тестовый режим");

        public static KeyboardButton Back => new KeyboardButton("Назад");
    }
}

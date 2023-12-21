using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiendMagicDestiny_bot
{
     class MessageResponses
    {
        public static KeyboardButton Add => new KeyboardButton("Добавить новое предназначение");
        public static KeyboardButton Edit => new KeyboardButton("Изменить");
        public static KeyboardButton Save => new KeyboardButton("Сохранить");
        public static KeyboardButton GenderM => new KeyboardButton("👨Мужчина");
        public static KeyboardButton GenderG => new KeyboardButton("👩Женщина");
        public static KeyboardButton NotSave => new KeyboardButton("Не сохранять");

        public static KeyboardButton Back => new KeyboardButton("Назад");
    }
}

using Fiend.Magic_bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiendMagicDestiny_bot
{
    internal class StateMachine : TaroArcans
    {
        private protected static Dictionary<long, State> userStates;
        private protected static Dictionary<long, string> _Name;
        private protected static Dictionary<long, string> _DateBirth;
        private protected static Dictionary<long, string> _Contact;
        public StateMachine()
        {
            userStates = new Dictionary<long, State>();
            _Name = new Dictionary<long, string>();
            _DateBirth = new Dictionary<long, string>();
            _Contact = new Dictionary<long, string>();
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
        public void SaveDateDitth(long chatId, string datebirth)
        {
            if (_DateBirth.ContainsKey(chatId))
                _DateBirth[chatId] = datebirth;
            else
                _DateBirth.Add(chatId, datebirth);
        }
        public void SaveContact(long chatId, string contact)
        {
            if (_Contact.ContainsKey(chatId))
                _Contact[chatId] = contact;
            else
                _Contact.Add(chatId, contact);
        }
        /*public void SaveTarostring(long chatId, string contact)
        {
            if (_Contact.ContainsKey(chatId))
                _Contact[chatId] = contact;
            else
                _Contact.Add(chatId, contact);
        }*/
        
        public void TransformationString(long chatId, string tarostring)
        {

            string allArcs = tarostring;
            string[] strArc = allArcs.Split(' ');
            Arcs = Array.ConvertAll(strArc, short.Parse); //сюда try-catch!!!
            foreach (var num in Arcs)
            {
                Console.WriteLine($"<{num}>");
            }
        }
        public void ResetState(long chatId)
        {
            if (userStates.ContainsKey(chatId))
                userStates.Remove(chatId);
            if (_Name.ContainsKey(chatId))
                _Name.Remove(chatId);
            if (_DateBirth.ContainsKey(chatId))
                _DateBirth.Remove(chatId);
            if (_Contact.ContainsKey(chatId))
                _Contact.Remove(chatId);
        }
    }
}

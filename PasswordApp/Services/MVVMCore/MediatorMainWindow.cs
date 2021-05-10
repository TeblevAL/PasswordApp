using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordApp.Services.MVVMCore
{
    public static class MediatorMainWindow
    {
        public delegate void ViewModel();

        private static readonly IDictionary<string, List<ViewModel>> dictionary =
           new Dictionary<string, List<ViewModel>>();

        public static void Subscribe(string token, ViewModel callback)
        {
            if (!dictionary.ContainsKey(token))
            {
                var list = new List<ViewModel>
                {
                    callback
                };
                dictionary.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach (var item in dictionary[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    dictionary[token].Add(callback);
            }
        }

        public static void Unsubscribe(string token, ViewModel callback)
        {
            if (dictionary.ContainsKey(token))
                dictionary[token].Remove(callback);
        }

        public static void Notify(string token)
        {
            if (dictionary.ContainsKey(token))
                foreach (var callback in dictionary[token])
                    callback();
        }
    }
}

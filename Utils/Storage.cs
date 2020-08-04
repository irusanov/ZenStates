using System.Collections;

namespace ZenStates.Utils
{
    public static class Storage
    {
        public static readonly Hashtable Store = new Hashtable();

        public static void Add(string key, object val)
        {
            if (Store.ContainsKey(key))
            {
                Remove(key);
            }
            Store.Add(key, val);
        }

        public static T Get<T>(object key)
        {
            return (T)Store[key];
        }

        public static void Remove(string key)
        {
            Store.Remove(key);
        }
    }
}

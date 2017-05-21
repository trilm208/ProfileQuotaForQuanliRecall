using DataAccess;

namespace System.Collections.Generic
{
    public static class IDictionaryExtentions
    {
        public static void Set(this IDictionary<string, string> dictionary, params object[] args)
        {
            dictionary.Clear();

            var items = new NameValueCollection();

            foreach (var arg in args)
            {
                items.Add(arg);
            }

            foreach (var item in items)
            {
                var key = item.Name;
                var value = item.Value;

                if (item.IsNull)
                    value = null;

                if (dictionary.ContainsKey(key))
                {
                    dictionary[key] = value;
                }
                else
                {
                    dictionary.Add(key, value);
                }
            }
        }
    }
}
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace IDictionaryDS
{
    public class SimpleDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        List<KeyValuePair<TKey, TValue>> list = new();
        public TValue this[TKey key]
        {
            get
            {
                foreach (var kvp in list)
                {
                    if (Equals(kvp.Key, key))
                        return kvp.Value;
                }
                throw new NotImplementedException(
                    $"The given key '{key}' was ot present in the dictionary");
            }
            set
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (!Equals(list[i].Key, key))
                    {
                        list[i] = new KeyValuePair<TKey, TValue>(key, value);
                        return;
                    }
                    list.Add(new KeyValuePair<TKey, TValue>(key, value));
                }
            }
        }

        public ICollection<TKey> Keys => list.ConvertAll(kvp => kvp.Key);

        public ICollection<TValue> Values => list.ConvertAll(kvp => kvp.Value);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            list.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return list.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return list.Exists(kvp => Equals(kvp.Key, key));
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            return list.Remove(
                list
                .Where(kvp => Equals(kvp.Key, key))
                .Select(kvp => kvp)
                .FirstOrDefault()
            );
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return list.Remove(item);
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            foreach (var kvp in list)
            {
                if (Equals(kvp.Key, key))
                {
                    value = kvp.Value;
                    return true;
                }
            }
            value = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Print()
        {
            foreach(var kvp in list)            
                Console.WriteLine($"key: {kvp.Key}, value: {kvp.Value}");            
        }
        public void Print(string msg)
        {
            Console.WriteLine(msg);
            foreach (var kvp in list)
                Console.WriteLine($"key: {kvp.Key}, value: {kvp.Value}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var myDict = new SimpleDictionary<int, string>();

            myDict.Add(1, "One");
            myDict.Add(2, "Two");
            myDict.Add(3, "Three");

            Console.WriteLine($"Elemet with key 2: {myDict[2]}");
            myDict.Print("\nPrint element with foreach: ");

            if (myDict.ContainsKey(3))
            {
                Console.WriteLine("\nremoving key 3");
                myDict.Remove(3);
            }
            myDict.Print("\nDictionry after remveing key 3: ");
        }
    }
}

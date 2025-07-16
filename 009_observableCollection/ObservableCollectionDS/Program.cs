using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Cryptography;

internal class Program
{
    static void AddItemsToCollection(ObservableCollection<string> list)
    {
        list.Add("Alice");
        list.Add("Bob");
        list.Add("James");
        list.Add("Jack");
    }
    static void PrintItemsInList(ObservableCollection<string> list)
    {
        foreach (string str in list)
        {
            Console.WriteLine(str);
        }
    }
    private static void Main(string[] args)
    {
        Console.Clear();
        ObservableCollection<string> list = new();

        list.CollectionChanged += List_CollectionChanged;
        AddItemsToCollection(list);

        list.RemoveAt(2);
        list.Move(0, list.Count - 1);
        list[1] = "alae";

        Console.WriteLine("\nList of Items");
        PrintItemsInList(list);
    }

    static void ItemAdded(NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems == null)
            return;
        foreach (string str in e.NewItems)
            Console.WriteLine($"ItemAdded: {str}");
    }

    static void ItemDeleted(NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems == null)
            return;
        foreach (string str in e.OldItems)
            Console.WriteLine($"Item Deleted '{str}'");
    }

    static void ItemReplaced(NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems == null || e.OldItems == null)
            return;
        for(int i = 0; i < e.NewItems.Count; i++)
            Console.WriteLine($"Old Item '{e.OldItems[i]}' Replaced by '{e.NewItems[i]}'");
    }

    static void ItemMove(NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems == null)
            return;
        foreach (string str in e.OldItems)
        Console.WriteLine($"Item '{str}' moved form index {e.OldStartingIndex} to {e.NewStartingIndex}");
    }

    private static void List_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                {
                    ItemAdded(e);
                    break;
                }
            case NotifyCollectionChangedAction.Remove:
                {
                    ItemDeleted(e);
                    break;
                }
            case NotifyCollectionChangedAction.Replace:
                {
                    ItemReplaced(e);
                    break;
                }
            case NotifyCollectionChangedAction.Move: {
                    ItemMove(e);
                    break;
                }
        }
    }
}
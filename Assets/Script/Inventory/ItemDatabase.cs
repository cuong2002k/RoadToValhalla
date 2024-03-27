using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public static class ItemDatabase
{
    private static Dictionary<string, BaseItem> ItemDatabae = null;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void Initialized()
    {
        // try
        // {
        //     ItemDatabae = new Dictionary<string, BaseItem>();
        //     var baseItems = Resources.LoadAll<BaseItem>("");
        //     foreach (BaseItem item in baseItems)
        //     {
        //         ItemDatabae.Add(item.ID, item);
        //     }
        // }
        // catch (IOException ex)
        // {

        // }
    }
    public static BaseItem GetItemWithID(string id)
    {

        if (ItemDatabae.ContainsKey(id))
        {
            return ItemDatabae[id];
        }

        return null;
    }
}

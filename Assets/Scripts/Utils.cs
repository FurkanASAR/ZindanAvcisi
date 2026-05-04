using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static T GetRandomInList<T>(List<T> list)
    {

        if (list == null || list.Count == 0)
        {
            {
                Debug.Log("Utils GetRandomInList: List is empty!");
                return default;
            }
        }
        int listIndex = UnityEngine.Random.Range(0, list.Count);

        return list[listIndex];
    }
    public static T GetAndRemoveRandomInList<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            {
                Debug.Log("Utils GetAndRemoveRandomInList: List is empty!");
                return default;
            }
        }
        int listIndex = UnityEngine.Random.Range(0, list.Count);
        T item = list[listIndex];
        list.RemoveAt(listIndex);

        return item;
    }
}

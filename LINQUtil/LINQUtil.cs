using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class LINQUtil
{

    public static string ToOneLineString<T>(this IEnumerable<T> list, string sep = ", ")
    {
        var result = new StringBuilder();
        foreach (var element in list)
        {
            if (result.Length > 0)
                result.Append(sep);

            result.Append(element);
        }

        return result.ToString();
    }

    public static IList<T> RandomSome<T>(this IList<T> list, int count)
    {

        if (list == null || list.Count <= 0 || count > list.Count)
        {
            throw new Exception("RandomSome() Error");
        }

        if (count == list.Count)
        {
            return list;
        }

        IList<T> res = new List<T>(count);
        while (res.Count < count)
        {
            T item = list.RandomOne();
            if (res.Contains(item))
                continue;
            res.Add(item);
        }

        return res;

    }

    public static T RandomOne<T>(this IList<T> list, int maxIndex = -1)
    {
        if (list == null || list.Count <= 0)
        {
            throw new Exception("RandomOne() Error");
        }
        else
        {
            if (maxIndex < 0)
            {
                int index = UnityEngine.Random.Range(0, list.Count);
                return list[index];
            }
            else
            {
                int index = UnityEngine.Random.Range(0, Math.Min(maxIndex, list.Count));
                return list[index];
            }
        }
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var temp = list[i];
            int randomIndex = UnityEngine.Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

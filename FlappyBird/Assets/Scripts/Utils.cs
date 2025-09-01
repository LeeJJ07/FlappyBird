using System;
using System.Collections.Generic;
using System.Linq;

public static class Utils
{
    /// <summary>
    /// Shuffle a list using Fisher-Yates algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this List<T> list)
    {
        var temp = list.OrderBy(item => Guid.NewGuid()).ToList();
        list.Clear();
        list.AddRange(temp);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    /// <summary>
    /// ¸®½ºÆ® ·£´ýÀ¸·Î ¼¯±â
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

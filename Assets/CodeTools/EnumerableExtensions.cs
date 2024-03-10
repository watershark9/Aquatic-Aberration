using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class EnumerableExtensions
{
    public static T GetRandomElement<T>(this IEnumerable<T> elements)
    {
        var index = Random.Range(0, elements.Count());
        return elements.ElementAt(index);
    }
}

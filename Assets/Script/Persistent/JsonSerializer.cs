using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonSerializer : ISerializer
{
    public string Serialize<T>(T obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public T DeSerialize<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializer
{
    public string Serialize<T>(T obj);
    public T DeSerialize<T>(string json);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
public class BinarySerializer : ISerializer
{
    public T DeSerialize<T>(string json)
    {
        byte[] bytes = Convert.FromBase64String(json);
        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            return (T)binaryFormatter.Deserialize(memoryStream);
        }
    }

    public string Serialize<T>(T obj)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(memoryStream, obj);
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}

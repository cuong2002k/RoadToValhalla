using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PlayerGameData : ISaveData
{
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    public Vector3 position;

    public PlayerGameData()
    {
        position = Vector3.zero;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PlayerGameData : ISaveData
{
    public string id { get; set; } = System.Guid.NewGuid().ToString();
    public Vector3 position;
    public Vector3 respawnPosition;

    public PlayerGameData()
    {
        position = new Vector3(37, 0, 44.6f);
        respawnPosition = new Vector3(37, 0, 44.6f);
    }

}

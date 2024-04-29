using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PlayerGameData
{
    public string id { get; set; }
    public Vector3Serializer position;
    public Vector3Serializer respawnPosition;

    public PlayerGameData()
    {
        position = new Vector3Serializer(37f, 0f, 44.6f);
        respawnPosition = new Vector3Serializer(37, 0, 44.6f);
    }
    

}

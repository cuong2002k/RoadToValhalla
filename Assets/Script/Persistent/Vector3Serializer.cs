using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Vector3Serializer
{
    public float x;
    public float y;
    public float z;
    public float w;

    public Vector3Serializer(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Vector3Serializer(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public Vector3Serializer(Vector3 vector3)
    {
        this.x = vector3.x;
        this.y = vector3.y;
        this.z = vector3.z;
    }


    public Vector3 GetVector3() => new Vector3(x, y, z);

    public Quaternion GetQuaternion() => new Quaternion(x, y, z, w);
}

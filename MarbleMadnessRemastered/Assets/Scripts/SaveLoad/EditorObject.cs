using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
public class EditorObject {

    public float PosX;
    public float PosY;
    public float PosZ;

    public float Rotation;
    public string PrefabName;

    public EditorObject(Vector3 _Position, float _Rotation, string _PrefabName)
    {
        PosX = _Position.x;
        PosY = _Position.y;
        PosZ = _Position.z;

        Rotation = _Rotation;

        PrefabName = _PrefabName;
    }

}

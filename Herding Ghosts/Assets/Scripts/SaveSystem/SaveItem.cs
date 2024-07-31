using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveItem : MonoBehaviour
{
    public string gameObjectName = "CHANGENAME";
    
    public Dictionary<string, int> numbersToSave = new Dictionary<string, int>();

    public Dictionary<string, string> stringsToSave = new Dictionary<string, string>();

    public Dictionary<string, Vector3> Vector3ToSave = new Dictionary<string, Vector3>();
}

public class SaveHelper
{
    public string gameObjectName = "CHANGENAME";

    public Dictionary<string, int> numbersToSave = new Dictionary<string, int>();

    public Dictionary<string, string> stringsToSave = new Dictionary<string, string>();

    public Dictionary<string, Vector3> Vector3ToSave = new Dictionary<string, Vector3>();
}



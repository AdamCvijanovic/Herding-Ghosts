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

    public Dictionary<Vector3, string> Vector3ToSave = new Dictionary<Vector3, string>();
}

public class SaveHelper
{
    public string gameObjectName = "CHANGENAME";

    public Dictionary<string, int> numbersToSave = new Dictionary<string, int>();

    public Dictionary<string, string> stringsToSave = new Dictionary<string, string>();

    public Dictionary<Vector3, string> Vector3ToSave = new Dictionary<Vector3, string>();
}



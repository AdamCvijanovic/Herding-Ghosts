using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Items/Food", order = 2)]

public class FoodScriptableObject : ScriptableObject
{
    public GameObject itemPrefab;
}

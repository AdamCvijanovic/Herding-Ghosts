using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode,Serializable]
public class Saveable : MonoBehaviour
{
    [SerializeField, ]
    public string id = "";

    public bool IsSaveable = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        if (Guid.TryParse(id,out Guid result))
        {
            if (result == Guid.Empty)
            {
                id = Guid.NewGuid().ToString();

                EditorUtility.SetDirty(this.gameObject);
            }
        }

        else
        {
            id = Guid.NewGuid().ToString();

            EditorUtility.SetDirty(this.gameObject);

            Debug.Log(id);

        }
    }
    #endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetSelector : MonoBehaviour
{
    FurnitureManager manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        manager = GameObject.FindGameObjectWithTag("FurnitureManager").GetComponent<FurnitureManager>();
    }

    public void ChangePreset(int preset)
    {
        manager.ChangePreset(preset);

    }
}

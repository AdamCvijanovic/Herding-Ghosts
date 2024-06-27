using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    public static SaveSystem ManagerState;
    public List<SaveItem> AllSaveItems;


    private int currentSaveSelection = 0;

    private string m_saveLocation;



    private void Awake()
    {
        if (ManagerState != null)
        {
            Destroy(gameObject);
            return;
        }

        ManagerState = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_saveLocation = Application.persistentDataPath + "/save" + currentSaveSelection + ".json";
        LoadItems();
    }


    public void SaveItems()
    {
        SaveItem[] savedItems = Resources.FindObjectsOfTypeAll<SaveItem>();

        List<SaveHelper> finalSaveditems = new List<SaveHelper>();

        
        foreach(var item in savedItems)
        {
            var helper = new SaveHelper();

            helper.gameObjectName = item.gameObjectName;
            helper.numbersToSave = item.numbersToSave;
            helper.stringsToSave = item.stringsToSave;

            finalSaveditems.Add(helper);
        }

        var jsonString = JsonConvert.SerializeObject(finalSaveditems, Formatting.Indented);

        File.WriteAllText(m_saveLocation, jsonString);
    }


    public void LoadItems()
    {
        if (File.Exists(m_saveLocation))
        {
            SaveItem[] savedItems = Resources.FindObjectsOfTypeAll<SaveItem>();

            string jsonData = File.ReadAllText(m_saveLocation);

            Debug.Log(savedItems[0].gameObjectName);
            var helperItems = JsonConvert.DeserializeObject<List<SaveHelper>>(jsonData);

            foreach (var helperItem in helperItems)
            {
                var sceneItem = savedItems.First(x => x.gameObjectName == helperItem.gameObjectName);
                sceneItem.gameObjectName = helperItem.gameObjectName;
                sceneItem.numbersToSave = helperItem.numbersToSave;
                sceneItem.stringsToSave = helperItem.stringsToSave;
            }
        }

    }

    public void SetSaveSlot(int saveSlot)
    {
        currentSaveSelection = saveSlot;
        m_saveLocation = Application.persistentDataPath + "/save" + currentSaveSelection + ".json";
    }
}

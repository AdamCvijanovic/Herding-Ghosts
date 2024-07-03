using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{

    public static SaveSystem ManagerState;
    public List<SaveHelper> finalSaveditems;


    public int currentSaveSelection = -1;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_saveLocation = Application.persistentDataPath + "/save" + currentSaveSelection + ".json";
        LoadItems();
    }

    public void SetSaveItems()
    {
        SaveItem[] savedItems = Resources.FindObjectsOfTypeAll<SaveItem>();

        finalSaveditems = new List<SaveHelper>();


        foreach (var item in savedItems)
        {
            var helper = new SaveHelper();

            helper.gameObjectName = item.gameObjectName;
            helper.numbersToSave = item.numbersToSave;
            helper.stringsToSave = item.stringsToSave;

            finalSaveditems.Add(helper);
        }
    }


    public void SaveItems()
    {

        var jsonString = JsonConvert.SerializeObject(finalSaveditems, Formatting.Indented);

        File.WriteAllText(m_saveLocation, jsonString);

        finalSaveditems.Clear();

    }


    public void LoadItems()
    {
        Debug.Log(currentSaveSelection);
        if (File.Exists(m_saveLocation) && currentSaveSelection != -1)
        {
            List<SaveItem> savedItems = new List<SaveItem>(Resources.FindObjectsOfTypeAll<SaveItem>());

            string jsonData = File.ReadAllText(m_saveLocation);

            var helperItems = JsonConvert.DeserializeObject<List<SaveHelper>>(jsonData);
            
            foreach (var helperItem in helperItems)
            {
                var sceneItem = savedItems.Find(x => x.gameObjectName == helperItem.gameObjectName);
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

    public bool CheckAvailable(int slot)
    {
        if(File.Exists(Application.persistentDataPath + "/save" + slot + ".json"))
        {
            return true;
        }

        else
        {

            return false;
        }


    }
}

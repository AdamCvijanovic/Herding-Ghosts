using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalSave
{

    public string timeSaved = "No Time";
    public List<SaveHelper> finalSaveditems;
}

public class SaveSystem : MonoBehaviour
{

    public static SaveSystem ManagerState;

    public FinalSave finalSave = new FinalSave();


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

        finalSave.finalSaveditems = new List<SaveHelper>();


        foreach (var item in savedItems)
        {
            var helper = new SaveHelper();

            helper.gameObjectName = item.gameObjectName;
            helper.numbersToSave = item.numbersToSave;
            helper.stringsToSave = item.stringsToSave;

            finalSave.finalSaveditems.Add(helper);
        }

        Debug.Log(finalSave.finalSaveditems.Count + "save items");
    }


    public void SaveItems()
    {
        finalSave.timeSaved = DateTime.Now.ToString("MMM dd yyyy hh:mm");

        var jsonString = JsonConvert.SerializeObject(finalSave, Formatting.Indented);

        File.WriteAllText(m_saveLocation, jsonString);

        finalSave.finalSaveditems.Clear();

    }


    public void LoadItems()
    {
        if (File.Exists(m_saveLocation) && currentSaveSelection != -1 && SceneManager.GetActiveScene().name != "NightToDayCutscene")
        {
            List<SaveItem> savedItems = new List<SaveItem>(Resources.FindObjectsOfTypeAll<SaveItem>());

            string jsonData = File.ReadAllText(m_saveLocation);

            var helperItems = JsonConvert.DeserializeObject<FinalSave>(jsonData);

            foreach (var helperItem in helperItems.finalSaveditems)
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
        if (File.Exists(Application.persistentDataPath + "/save" + slot + ".json"))
        {
            return true;
        }

        else
        {

            return false;
        }


    }

    public string GetSaveTime(int slot)
    {
        var saveLocation = Application.persistentDataPath + "/save" + slot + ".json";
        if (File.Exists(saveLocation))
        {
            var jsonData = File.ReadAllText(saveLocation);
            var helperItems = JsonConvert.DeserializeObject<FinalSave>(jsonData);
            return helperItems.timeSaved;
        }

        else
            return "SAVE NAME NOT AVAILABLE";

    }
}

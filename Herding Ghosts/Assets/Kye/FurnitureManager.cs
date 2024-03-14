using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FurnitureManager : MonoBehaviour
{

    [System.Serializable]
    public class RoomData
    {
        public int roomLevel = 0;
        public int currentPreset = 0;
        public List<FurnitureData> furnitureList = new List<FurnitureData>();
    }


    [System.Serializable]
    public class FurnitureData
    {
        public int tracker;
        public int presetNumber;
        public Vector3 position;
        public Quaternion rotation;
    }

    //List<GameObject>[] roomPresets = new List<GameObject>[4];
    public GameObject[] presets = new GameObject[4];
    public GameObject[] rooms = new GameObject[1];

    public int currentPreset = 0;
    public int currentRoom = -1;

    List<GameObject> m_createdFurniture = new List<GameObject>();
    int createdStart = 0;
    int createdEnd = 3;

    List<GameObject> m_availableFurniture = new List<GameObject>();
    public GameObject[] m_furnitureSelector;

    public bool holdingObject = false;

    private string saveLocation;
    public GameObject UI;
    public Sprite emptySprite;
    // Start is called before the first frame update
    void Awake()
    {
    }

    private void Start()
    {
        saveLocation = Application.persistentDataPath + "/furnituredata.json";
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(saveLocation))
        {

            string jsonData = File.ReadAllText(saveLocation);

            var roomData = JsonUtility.FromJson<RoomData>(jsonData);

            //rooms[roomData.roomLevel].SetActive(true);


            foreach (FurnitureData furnitureData in roomData.furnitureList)
            {
                var item = Instantiate(m_furnitureSelector[furnitureData.tracker], furnitureData.position, furnitureData.rotation);

                item.GetComponent<FurnitureController>().PlaceFurniture(new UnityEngine.InputSystem.InputAction.CallbackContext());
                item.transform.parent = presets[furnitureData.presetNumber].transform;
                item.GetComponent<FurnitureController>().tracker = furnitureData.tracker;
                m_createdFurniture.Add(item);
            }

            for(int i = 0; i < presets.Length; i++)
            {
                if (i != roomData.currentPreset)
                    presets[i].SetActive(false);
            }

            for (int i = 0; i < rooms.Length; i++)
            {
                if (i != roomData.roomLevel)
                    rooms[i].SetActive(false);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void AddFurniture(GameObject furniture)
    {
        GameObject[] tempList = new GameObject[m_furnitureSelector.Length + 1];
        m_furnitureSelector.CopyTo(tempList, 0);
        tempList[m_furnitureSelector.Length] = furniture;
        m_furnitureSelector = tempList;
    }

    public void FurnitureAppear()
    {
        UI.SetActive(true);
        var position = transform.position + new Vector3(-5000, -3, 0);
        var itemPosition = 0;

        for (int i = createdStart; i < createdEnd; i++)
        {

            if (i < m_furnitureSelector.Length)
            {
                var item = Instantiate(m_furnitureSelector[i], position, Quaternion.identity, UI.transform);
                item.GetComponent<FurnitureController>().tracker = i;

                m_availableFurniture.Add(item);

                position += new Vector3(2, 0, 0);

                UI.transform.GetChild(0).GetChild(itemPosition).GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                
            }

            else
            {
                UI.transform.GetChild(0).GetChild(itemPosition).GetComponent<Image>().sprite = emptySprite;
            }
            
            itemPosition++;
        }

    }

    public void ClearFurniture()
    {
        UI.SetActive(false);
        foreach (GameObject furniture in m_availableFurniture)
        {
            if(!furniture.GetComponent<FurnitureController>().selected )
                Destroy(furniture);
        }
        m_availableFurniture.Clear();
    }

    public void CreateFurnitureInScene(GameObject furniture)
    {
        m_createdFurniture.Add(furniture);
    
        Debug.Log(m_createdFurniture.Count);
    }

    public void RemoveFurnitureInScene(GameObject furniture)
    {
        
        m_createdFurniture.Remove(furniture);
        Debug.Log("Removed");
    }

    public void RemoveAllFurniture()
    {
        foreach (GameObject furniture in m_createdFurniture)
        {
                Destroy(furniture);
        }
        m_createdFurniture.Clear();
        Debug.Log("Removed All");
    }

    public void ChangePreset(int newPresetNumber)
    {
        presets[currentPreset].SetActive(false);

        currentPreset = newPresetNumber;
        //m_createdFurniture = roomPresets[currentPreset];
        
        presets[currentPreset].SetActive(true);
    }

    private void OnEnable()
    {
       

       
    }

    private void OnDisable()
    {
        var roomData = new RoomData();

        roomData.roomLevel = 1;

        roomData.currentPreset = currentPreset;

        for (int i = 0; i < presets.Length; i++)
        {
            foreach (Transform child in presets[i].transform)
            {
                var data = new FurnitureData();
                data.position = child.transform.position;
                data.rotation = child.transform.rotation;
                data.presetNumber = i;
                data.tracker = child.transform.gameObject.GetComponent<FurnitureController>().tracker;

                roomData.furnitureList.Add(data);

            }
        }

        var jsonString = JsonUtility.ToJson(roomData);
        File.WriteAllText(saveLocation, jsonString);
    }

    public void SavePresets()
    {
        //roomPresets[currentPreset] = m_createdFurniture;
    }

    public void FurnitureSelectorClick(int number)
    {
        m_availableFurniture[number].GetComponent<FurnitureController>().PickUpFurniture();
    }

    public void FurnitureSelectorForward()
    {
        createdStart += 3;
        createdEnd += 3;

        if (createdStart > m_furnitureSelector.Length)
        {
            createdStart = 0;
            createdEnd = 3;
        }


        foreach (var item in m_availableFurniture)
        {
            Destroy(item);
        }
        m_availableFurniture.Clear();


        FurnitureAppear();
    }

}

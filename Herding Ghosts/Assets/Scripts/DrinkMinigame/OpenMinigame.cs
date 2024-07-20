using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMinigame : MonoBehaviour
{
    public GameObject start;
    public GameObject m_minigameCanvas;
    public GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        m_minigameCanvas = Instantiate(start);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableMinigame()
    {
        m_minigameCanvas.SetActive(true);
        ui.SetActive(false);
    }

    public void DisableMinigame()
    {

        Destroy(m_minigameCanvas);
        m_minigameCanvas = null;

        m_minigameCanvas = GameObject.Instantiate(start);

        ui.SetActive(true);
    }
}

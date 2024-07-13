using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedScript : MonoBehaviour
{
    public LevelManager levelManager;

    public Interactable interactable;
    public bool bedActive;

    public bool sleepActive = false;

    //audio
    public AudioSource audioBedCharm;

    public GameObject musicGameObject;
    public AudioSource backgroundMusic;

    private float fadeDuration;
    private float initialLowPassCutoff;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        musicGameObject = GameObject.Find("BackgroundMusic");
        backgroundMusic = musicGameObject.GetComponent<AudioSource>();
        fadeDuration = 0.5f;
        initialLowPassCutoff = backgroundMusic.GetComponent<AudioLowPassFilter>().cutoffFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (bedActive == true)
        {
        }
    }

    public void DeActivateBed()
    {
        interactable.enabled = false;
        bedActive = false;
    }

    public void ActivateBed()
    {
        interactable.enabled = true;
        bedActive = true;
    }

    public void Sleep()
    {
        if (!sleepActive)
        {
            audioBedCharm.Play();
            Debug.Log("Sleep");
            levelManager.EndDay();
            SaveSystem.ManagerState.SetSaveItems();
            sleepActive = true;
            AudioLowPassFilter lowPassFilter = backgroundMusic.GetComponent<AudioLowPassFilter>();
            lowPassFilter.enabled = true;

            StartCoroutine (LowPassFilterFade(lowPassFilter, 500f));
        }
    }

    IEnumerator LowPassFilterFade(AudioLowPassFilter lowPassFilter, float targetCutoff)
    {
        float startTime = Time.time;
        float startCutoff = initialLowPassCutoff;

        while (Time.time - startTime < fadeDuration)
        {
            float timePassed = Time.time - startTime;
            float t = timePassed / fadeDuration;

            float newCutoff = Mathf.Lerp(startCutoff, targetCutoff, t);

            lowPassFilter.cutoffFrequency = newCutoff;

            yield return null;
        }
    }
}

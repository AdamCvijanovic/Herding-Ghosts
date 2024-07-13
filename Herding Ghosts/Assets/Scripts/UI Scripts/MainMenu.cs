using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string mainScene;
  //public Manager manager;

 public void PlayGame ()
 {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Brackeys is God
    //MobileGameManager.instance
    Time.timeScale = 1f;
 }

    public void PlayCertainScene()
    {
        SceneManager.LoadScene(mainScene);
    }

  public void BackToMM ()
 {
    SceneManager.LoadScene(0);
    Time.timeScale = 1f;
 }

   public void ToBrendanPrototype ()
 {
    SceneManager.LoadScene(2);
    Time.timeScale = 1f;
 }

 public void QuitGame ()
 {
   Debug.Log ("QUIT!");
   Application.Quit();
 }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   public CanvasGroup OptionPanel;

   public void PlayGame()
   {
    SceneManager.LoadScene("CutScene");
   }
  
   public void Credits()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +2);
   }

   public void Quit()
   {
    Application.Quit();
   }

   public void CreditsBack()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);
   } 
   
   public void GetToMenu()
   {
     SceneManager.LoadScene("Menu");
   }

   
}

//  INFO
//  Emi
//  This script will try to implement everything the UI MENU needs to work and some useful info about it below

//  *Inside Canvas Panel (Background) you can locate the BG sprite and its properties 
//  *Inside Canvas Text (Text) you can update the font type. will be placeholder text for now
//  *Menu making reference https://www.youtube.com/watch?v=zc8ac_qUXQY

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    //  Las acciones de referenciado se hacen desde el mismo unity arrastrando los gameobj con sus scripts
    public void Play(){
        //  Scene manager is used from the library of unityengine
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //  this loads the next lvl in queue
        //  This snippet was extracted from (8:40) of the video
        //  The queue is edited in the build settings (9:14)
        
        //  We can also:
        Debug.Log("Changed Scene");
        SceneManager.LoadScene("Swiper");
    }
    public void OpenSettings(){
        Debug.Log("Opened Settings Menu");
        //  (si, solo hace un log, el resto de acciones están en unity)
    }
    public void Exit(){
        Debug.Log("GAME CLOSED");
        Application.Quit(); 
    }

}
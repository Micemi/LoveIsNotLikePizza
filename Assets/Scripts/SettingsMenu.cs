//  INFO
//  Emi
//  Implements the settings menu actions and values

//  *Menu making reference https://www.youtube.com/watch?v=zc8ac_qUXQY
//  *Settings making reference https://www.youtube.com/watch?v=YOaYQrN1oYQ (IMPORTANT!)
//  *An audio mixer tutorial https://www.youtube.com/watch?v=vOaQp2x-io0

using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {
    [SerializeField]
    private AudioMixer masterAudio;
    private string ExposedVarNameMaster = "MasterVolume";
    private string ExposedVarNameMusic = "MusicVolume";
    private string ExposedVarNameSFX = "SFXVolume";
    
    [SerializeField]    //  reference all sliders fire component on menu
    private GameObject[] fireComp;


    
    //  Master is the father of the rest of sound variables
    public void SetMasterVolume(float volume){
        //  Debug.Log(volume);
        masterAudio.SetFloat(this.ExposedVarNameMaster, volume);
        ChangeColor(volume, 0);
        //Debug.Log("Dentro de SetMasterVol");
    }

    public void SetMusicVolume(float volume){
        masterAudio.SetFloat(this.ExposedVarNameMusic, volume);
        ChangeColor(volume, 1);
    }
    public void SetSFXVolume(float volume){
        masterAudio.SetFloat(this.ExposedVarNameSFX, volume);
        ChangeColor(volume, 2);
    }

    private void ChangeColor(float vol, int objNum){
        //Debug.Log("Dentro de ChangeColor");
        if (vol < -33.33f)   // go cold
        {
            fireComp[objNum].GetComponent<ChangeFire>().GoCold();
        }
        else if(vol < -6.66f){  //  warm
            fireComp[objNum].GetComponent<ChangeFire>().GoWarm();
        }
        else{   // hottt
            fireComp[objNum].GetComponent<ChangeFire>().GoHOTTT();
        }
        //  Later we can make it more dynamic and neat, i know it isnt the best
    }

    //  Poner la imagen de fuego en el script de cada menu ya que es el controller y luego en los sliders


    //  Go Back Button
    public void Back(){
        Debug.Log("Back to Menu");
        //  the rest of the logic is in unity button properties

    }

}
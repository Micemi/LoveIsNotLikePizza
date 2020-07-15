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
    


    //  Master is the father of the rest of sound variables
    public void SetMasterVolume(float volume){
        //  Debug.Log(volume);
        masterAudio.SetFloat(this.ExposedVarNameMaster, volume);
    }
    public void SetMusicVolume(float volume){
        masterAudio.SetFloat(this.ExposedVarNameMusic, volume);
    }
    public void SetSFXVolume(float volume){
        masterAudio.SetFloat(this.ExposedVarNameSFX, volume);
    }

    

    //  Go Back Button
    public void Back(){
        Debug.Log("Back to Menu");
        //  the rest of the logic is in unity button properties

    }

}
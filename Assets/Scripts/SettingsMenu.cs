//  INFO
//  Emi
//  Implements the settings menu actions and values

//  *Menu making reference https://www.youtube.com/watch?v=zc8ac_qUXQY
//  *Settings making reference https://www.youtube.com/watch?v=YOaYQrN1oYQ (IMPORTANT!)

using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {
    [SerializeField]
    private AudioMixer masterAudio;
    private string ExposedVarNameMaster = "MasterVolume";
    /*
    private musicAudio
    private SFXAudio
    */
    public void SetMasterVolume(float volume){
        //  Debug.Log(volume);
        masterAudio.SetFloat(this.ExposedVarNameMaster, volume);
    }

    public void Back(){
        Debug.Log("Back to Menu");
        //  the rest is in unity button properties
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileBtn : MonoBehaviour
{
    
    
    

    
    public void ProfileScene()
    {
      Debug.Log("Muestra tu Perfil");
      SceneManager.LoadScene ("ProfileScene"); 
    }

}

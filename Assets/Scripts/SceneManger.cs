using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
    //public static SceneManger instance;
    // Start is called before the first frame update
    /*void Awake()
    {
        instance=this;
    }*/
    public void MoveToNextScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }
}

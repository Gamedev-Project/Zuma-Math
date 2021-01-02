using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManger : MonoBehaviour
{
    private SceneManger sceneManger;
    public Animator transition;
    public float TransitionTime=1f;
    public GameObject InstructionsPanel;
    public GameObject SettingsPanel;
    public GameObject PausePanel=null;
    public Slider AudioSlider;
    public Slider DifficultySlider;
    static private float AudioSliderVolume;
    static private float DifficultyLevel;

    void Update(){
        if(SceneManager.GetActiveScene().buildIndex>=1){
            if(BallDestroy.instance.getLevelFinished()){
                StartCoroutine(NextLevel(SceneManager.GetActiveScene().buildIndex+1));
            }
        }
        this.GetComponent<AudioSource>().volume=AudioSlider.value;
        DifficultySlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
    }
    void Start(){
        if(SceneManager.GetActiveScene().buildIndex!=0){
        AudioSlider.value=AudioSliderVolume;
        }
        DifficultySlider.value=DifficultyLevel;
    }
    public void Play(){
        StartCoroutine(NextLevel(SceneManager.GetActiveScene().buildIndex+1));
    }
    public void OpenCloseInstructions(){
        if(InstructionsPanel!=null){
            InstructionsPanel.SetActive(!InstructionsPanel.activeSelf);
        }
        if(PausePanel!=null){
            PausePanel.SetActive(!PausePanel.activeSelf);
        }
    }
    public void OpenCloseSettings(){
        if(SettingsPanel!=null){
            SettingsPanel.SetActive(!SettingsPanel.activeSelf);
        }
        if(PausePanel!=null){
            PausePanel.SetActive(!PausePanel.activeSelf);
        }
    }
    IEnumerator NextLevel(int Level){
        AudioSliderVolume=AudioSlider.value;
        DifficultyLevel=DifficultySlider.value;

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(Level);
    }
    public void ValueChangeCheck()
	{
        
		switch(DifficultySlider.value){
            case 0:
                DifficultySlider.GetComponentInChildren<TextMeshProUGUI>().SetText("Easy");
                break;
            case 1:
                DifficultySlider.GetComponentInChildren<TextMeshProUGUI>().SetText("Medium");
                break;
            case 2:
                DifficultySlider.GetComponentInChildren<TextMeshProUGUI>().SetText("Hard");
                break;
        }
	}
    public void PauseGame(){
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        Zuma.instance.Pause=true;
    }
    public void ResumeGame(){
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        Zuma.instance.Pause=false;
    }
}

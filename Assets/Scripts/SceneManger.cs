using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManger : MonoBehaviour
{
    public static SceneManger instance;
    public Animator transition;
    public float TransitionTime=1f;
    public GameObject InstructionsPanel;
    public GameObject SettingsPanel;
    public GameObject PausePanel=null;
    public Slider AudioSlider;
    public Slider DifficultySlider;
    public GameObject Lives;
    public GameObject Score;
    static private float AudioSliderVolume;
    static private float DifficultyLevel;
    private bool GAMEOVER=false;
    private int StrikePoints=0;

    void Awake(){
        instance=this;
    }
    void Start(){
        if(SceneManager.GetActiveScene().buildIndex!=0){
            AudioSlider.value=AudioSliderVolume;
            DifficultySlider.interactable=false;
            DifficultySlider.GetComponentInChildren<TextMeshProUGUI>().SetText("Go to Main Menu to change Difficulty!");
        }
        else{
            DifficultySlider.value=0;
        }
        DifficultySlider.value=DifficultyLevel;
    }
    void Update(){
        if(SceneManager.GetActiveScene().buildIndex>=1){
            if(BallDestroy.instance.getGameOver()){
                GAMEOVER=true;
                SetGameOver();
                return;
                //StartCoroutine(NextLevel(SceneManager.GetActiveScene().buildIndex+1));
            }
            if(BallDestroy.instance.getLevelFinished()){
                StartCoroutine(NextLevel(SceneManager.GetActiveScene().buildIndex+1));
            }
        }
        this.GetComponent<AudioSource>().volume=AudioSlider.value;
        DifficultySlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
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
        if(!GAMEOVER){
            Time.timeScale = 1;
            PausePanel.SetActive(false);
            Zuma.instance.Pause=false;
        }
    }
    private void SetGameOver(){
        PauseGame();
        PausePanel.GetComponentInChildren<TextMeshProUGUI>().SetText("Game Over!");
    }
    public void RestartGame(){
        GAMEOVER=false;
        ResumeGame();
        StartCoroutine(NextLevel(SceneManager.GetActiveScene().buildIndex));
    }
    public void DecreseLive(){
        StrikePoints=0;
        Image[] Component= Lives.GetComponentsInChildren<Image>();
        foreach(Image i in Component){
            if(i.enabled==false){
                continue;
            }
            else{
                i.enabled=false;
                int count=0;
                foreach(Image j in Component){
                    if(j.enabled==false){
                        count++;
                    }  
                }
                if(count==3){
                    GAMEOVER=true;
                    SetGameOver();
                }
                break;
            } 
        }
    }
    public void AddPoints(){
        StrikePoints++;
        string str=Score.GetComponent<TextMeshProUGUI>().text.Split(':')[1];
        float temp=float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
        Debug.Log(temp);
        if(StrikePoints%3==0){
            temp+=25*(getDiff()+1)*EquationMaker.instance.getLevelnum()*2;
        }
        else{
            temp+=25*(getDiff()+1)*EquationMaker.instance.getLevelnum();
        }
        Debug.Log(temp);
        Score.GetComponent<TextMeshProUGUI>().SetText("Score:"+temp);
    }
    public float getDiff(){
        return DifficultyLevel;
    }
    public void MainMenu(){
        GAMEOVER=false;
        ResumeGame();
        StartCoroutine(NextLevel(0));
    }
}

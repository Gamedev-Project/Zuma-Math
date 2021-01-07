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
    public AudioClip GameOverAudio;
    public GameObject NameInputField;
    public GameObject ParaohDialog=null;
    static private float AudioSliderVolume;
    static private float DifficultyLevel;
    static private string PlayerName;
    static private float ScoreNum;
    static private float ScorePerLevel;
    private bool GAMEOVER=false;
    private int StrikePoints=0;
    private bool MovementComplete=true;

    void Awake(){
        instance=this;
    }
    void Start(){
        if(SceneManager.GetActiveScene().buildIndex!=0){
            AudioSlider.value=AudioSliderVolume;
            DifficultySlider.interactable=false;
            DifficultySlider.GetComponentInChildren<UnityEngine.UI.Text>().text="<color=#feae34>For Name and Difficulty Change, Go to Main Menu!</color>";
            ScorePerLevel=ScoreNum;
            if(ScoreNum!=null){
                Score.GetComponent<TextMeshProUGUI>().SetText("Score:"+ScoreNum);
            }
        }
        else{
            DifficultySlider.value=0;
            ScoreNum=0;
        }
        DifficultySlider.value=DifficultyLevel;
        NameInputField.GetComponent<TMP_InputField>().characterLimit=13;

    }
    void Update(){
        if(SceneManager.GetActiveScene().buildIndex>=1){
            if(BallDestroy.instance.getGameOver()){
            if(MovementComplete){
                MovementComplete=false;
                GAMEOVER=true;
                SetGameOver();
                Time.timeScale = 1;
            }
                //return;
                //StartCoroutine(NextLevel(SceneManager.GetActiveScene().buildIndex+1));
            }
            else if(BallDestroy.instance.getLevelFinished()){
                if(SceneManager.GetActiveScene().buildIndex==5){
                StartCoroutine(NextLevel(0));
                }
                else{
                    StartCoroutine(NextLevel(SceneManager.GetActiveScene().buildIndex+1));
                }
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
        PlayerName=NameInputField.GetComponent<TMP_InputField>().text;

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(Level);
    }
    public void ValueChangeCheck()
	{
        
		switch(DifficultySlider.value){
            case 0:
                DifficultySlider.GetComponentInChildren<UnityEngine.UI.Text>().text="<color=#feae34>Easy</color>";
                break;
            case 1:
                DifficultySlider.GetComponentInChildren<UnityEngine.UI.Text>().text="<color=#feae34>Medium</color>";
                break;
            case 2:
                DifficultySlider.GetComponentInChildren<UnityEngine.UI.Text>().text="<color=#feae34>Hard</color>";
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
        this.GetComponent<AudioSource>().clip=GameOverAudio;
        this.GetComponent<AudioSource>().Play();
    }
    public void RestartGame(){
        ScoreNum=ScorePerLevel;
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
        StartCoroutine(PopDialog(false));
    }
    public void AddPoints(){
        StrikePoints++;
        string str=Score.GetComponent<TextMeshProUGUI>().text.Split(':')[1];
        float temp;
        if(ScoreNum!=null){
            temp=ScoreNum;
        }
        else{
            temp=float.Parse(str, CultureInfo.InvariantCulture.NumberFormat);
        }
        if(StrikePoints%3==0){
            temp+=25*(getDiff()+1)*EquationMaker.instance.getLevelnum()*2;
            LifeBonus();
        }
        else{
            temp+=25*(getDiff()+1)*EquationMaker.instance.getLevelnum();
        }
        Score.GetComponent<TextMeshProUGUI>().SetText("Score:"+temp);
        ScoreNum=temp;
        StartCoroutine(PopDialog(true));
    }
    public float getDiff(){
        return DifficultyLevel;
    }
    public void MainMenu(){
        GAMEOVER=false;
        ResumeGame();
        StartCoroutine(NextLevel(0));
    }
    public void OnValueChanged(){
        if(SceneManager.GetActiveScene().buildIndex!=0){
            if(PlayerName!=null){
                NameInputField.GetComponent<TMP_InputField>().text=PlayerName;
            }
            else{
                NameInputField.GetComponent<TMP_InputField>().text="";
            }
        }
    }
    IEnumerator PopDialog(bool Mood){
        int x=Random.Range(1,4);
        string temp;
        if(PlayerName==null){
            temp="";
        }
        else{
            temp=PlayerName;
        }
        if(Mood){
        switch(x){
            case 1:
                ParaohDialog.GetComponent<TextMeshProUGUI>().text="Good job "+temp+". Youre Awesome!";
                break;
            case 2:
                ParaohDialog.GetComponent<TextMeshProUGUI>().text="Wow, thats some Serious Math Skills "+temp+"!";
                break;
            case 3:
                ParaohDialog.GetComponent<TextMeshProUGUI>().text="Youre one more step to victory "+temp+"!";
                break;
        }
            if(Mood&&StrikePoints%3==0){
                ParaohDialog.GetComponent<TextMeshProUGUI>().text="You got a Life and points Bonus "+temp+" !";
            }
            ParaohDialog.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            ParaohDialog.SetActive(false);
        }
        else{
        switch(x){
            case 1:
                ParaohDialog.GetComponent<TextMeshProUGUI>().text="Hey hey be careful "+temp+"!";
                break;
            case 2:
                ParaohDialog.GetComponent<TextMeshProUGUI>().text="Thats not a good Work there "+temp+"..";
                break;
            case 3:
                ParaohDialog.GetComponent<TextMeshProUGUI>().text="What are you doing "+temp+"?!";
                break;
        }
            ParaohDialog.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            ParaohDialog.SetActive(false);
        }
    }
    public void LifeBonus(){
        Image[] Component= Lives.GetComponentsInChildren<Image>();
        int count=1;
        foreach(Image i in Component){
            if(i.enabled==true){
                count++;
            }
        }
        foreach(Image i in Component){
            if(count!=0){
                i.enabled=true;
                count--;
            }
            else{
                i.enabled=false;
            }
        }
    }
}

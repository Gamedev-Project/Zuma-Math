using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Zuma : MonoBehaviour
{
    public static Zuma instance;
    public static string prevBallColor;
    [SerializeField] private EquationMaker EquationManger;
    [SerializeField] private SolutionManger Solutions;
    [SerializeField] private int ThrowSpeed;
    [SerializeField] private float timer;
    [SerializeField] private float rate;
    public GameObject CurrBall;
    public Transform CurrBallPoint;
    
    public GameObject NextBall;
    public Transform NextBallPoint;
    public Transform spawnpoint;
    [Tooltip("Need to be Divided by 6")]
    public int BallsToFinish;
    public bool IsFinished = false; //If false the balls will stop coming out to the game path
    private int count=0; // Helps us identify when we have finished writing an entire equation
    private string[] equation=new string[5];
    public string NextScene;
    public SceneManger sceneManger;
    private string keypress="";
   

    private void Awake()
    {
        instance = this;
        prevBallColor="yellow";
    }

    private void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Space)){
            if(keypress!=""&&keypress!="-"){
                CurrBall.GetComponent<BallDestroy>().SetSolutionID(keypress);
                CurrBall.GetComponentInChildren<TextMeshPro>().SetText(keypress);
                keypress="";
            }
        }
        if(BallsToFinish==0){
            IsFinished=true;
        }
        if(BallDestroy.instance.getLevelFinished()){
            sceneManger.MoveToNextScene(NextScene);
        }
        getKeys();
        MouseMovement();
        if (Input.GetMouseButtonDown(1))
        {
            ThrowColorfullBall();
        }
        if (IsFinished) return;
        timer -= Time.deltaTime;
        if(timer<=0)
        {
        if(count==0){
            equation=EquationManger.MakeEquation(EquationMaker.instance.Levelnum);
            Solutions.AddSolution(equation[4]);
            Solutions.SetBank();
        }
            StartCoroutine(CreateBallMovement());
            timer = rate;
        }
    }
    public void MouseMovement()
    {
        Vector3 vec =Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
    public void getKeys(){
        for ( int i = 0; i < 10; ++i ){
            if (Input.GetKeyDown( "" + i )){
            if((keypress==""||keypress[0]=='-')&&i==0){
                continue;
            }
                if(keypress!=""&&keypress[0]=='-'){
                    if(keypress.Length<3){
                        keypress+=i;
                    }
                }
                else{
                    if(keypress.Length<2){
                        keypress+=i;
                    }
                }
                //Debug.Log(keypress);
            }
        }
        if(Input.GetKeyDown("-")&&keypress==""){
            keypress+="-";
            //Debug.Log(keypress);
        }
    }

    public void ThrowColorfullBall()
    {
        
        if (CurrBall == null) return;
        //this line switching the currball with nextball and creates new nextball 
        StartCoroutine(CreateBall());
        //this line doing the throwing out of zuma
        CurrBall.GetComponent<Rigidbody2D>().AddForce(transform.right*ThrowSpeed);
        //this currBall object will be without the father object(zuma)
        CurrBall.transform.SetParent(null);
        Destroy(CurrBall.gameObject, 2f);
        CurrBall = null;
    }
   //create ball near zuma
    public IEnumerator CreateBall()
    {
        
        yield return new WaitForSeconds(0.2f);
        CurrBall = NextBall;
        CurrBall.transform.position = CurrBallPoint.position;
        CurrBall.transform.position = CurrBallPoint.position;

        NextBall = Instantiate(Balls.instance.NextBallSec(SolutionManger.instance.getRandomFromBank()).ballprefab, NextBallPoint.position, Quaternion.identity);
       
        NextBall.transform.SetParent(transform);
        
    }
       //create ball near zuma
    /*public IEnumerator CreateBall(string num)
    {
        
        yield return new WaitForSeconds(0.2f);
        CurrBall.transform.SetParent(null);
        Destroy(CurrBall.gameObject, 2f);
        //CurrBall = NextBall;
        //CurrBall.transform.position = CurrBallPoint.position;
        CurrBall.transform.position = CurrBallPoint.position;

        CurrBall = Instantiate(Balls.instance.NextBallSec(num).ballprefab, NextBallPoint.position, Quaternion.identity);
       Debug.Log("IM HERE");
        CurrBall.transform.SetParent(transform);
        
    }*/
    public IEnumerator CreateBallMovement()
    {
        yield return new WaitForSeconds(0.2f);
       switch (count)
       {
        case 0: //make the "?" ball
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,"?",equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
            count++;
            BallsToFinish--;
            break;
           }
        case 1: //make the "=" ball
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[3],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
             count++;
             BallsToFinish--;
             break;
           }
        case 2: 
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[2],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
            count++;
            BallsToFinish--;
            break;
           }
        case 3: // make the "+/-/*/ / " ball
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[1],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
             count++;
             BallsToFinish--;
             break;
           }
        case 4:
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[0],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
            BallsToFinish--;
             count++;
             break;
           }
           default: //make a yellow break ball
           {   
                BallsToFinish--;  
                GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(true,"","").movingprefab, spawnpoint.position, Quaternion.identity); 
                count=0;
                break;
           }
       }



        

    }
  
}
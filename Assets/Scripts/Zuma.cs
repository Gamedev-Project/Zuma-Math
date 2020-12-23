using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Transform NextBallPoint;
    public GameObject NextBall;
    public Transform spawnpoint;
    public bool IsFinished = false; //If false the balls will stop coming out to the game path
    private int count=0; // Helps us identify when we have finished writing an entire equation
    private string[] equation=new string[5];
      
    
   

    private void Awake()
    {
        instance = this;
        prevBallColor="yellow";
    }

    private void Update()
    {
        if (IsFinished) return;
        timer -= Time.deltaTime;
        if(timer<=0)
        {
        if(count==0){
            equation=EquationManger.MakeEquation(1);
            Solutions.AddSolution(equation[4]);
            Solutions.SetBank();
        }
            StartCoroutine(CreateBallMovement());
            timer = rate;
        }
      
        MouseMovement();
        if (Input.GetMouseButtonDown(1))
        {
            ThrowColorfullBall();
        }
    }
    public void MouseMovement()
    {
        Vector3 vec =Camera.main.ScreenToWorldPoint(Input.mousePosition)- transform.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        transform.localEulerAngles = new Vector3(0, 0, angle);
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

        NextBall = Instantiate(Balls.instance.NextBallSec(equation[4]).ballprefab, NextBallPoint.position, Quaternion.identity);
       
        NextBall.transform.SetParent(transform);
        
    }
    public IEnumerator CreateBallMovement()
    {
        yield return new WaitForSeconds(0.2f);
       switch (count)
       {
        case 0: //make the "?" ball
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,"?",equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
            count++;
            break;
           }
        case 1: //make the "=" ball
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[3],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
             count++;
             break;
           }
        case 2: 
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[2],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
            count++;
            break;
           }
        case 3: // make the "+/-/*/ / " ball
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[1],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
             count++;
             break;
           }
        case 4:
           {
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[0],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);

             count++;
             break;
           }
           default: //make a yellow break ball
           {        
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(true,"","").movingprefab, spawnpoint.position, Quaternion.identity); 
             count=0;
            break;
           }
       }



        

    }
  
}
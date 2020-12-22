using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zuma : MonoBehaviour
{
    public static Zuma instance;
    public static string prevBallColor;
    [SerializeField] private EquationMaker EquationManger;
    [SerializeField] private SolutionManger Solutions;
    public GameObject CurrBall;
    public Transform CurrBallPoint;
    public Transform NextBallPoint;
    public GameObject NextBall;
    public Transform spawnpoint;
    public float timer;
    public float rate;
    public bool IsFinished = false;
    private int count=0;
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
        //if (CurrBall == null) return;
        //this line is for desposing the sprite on the CurrBall when deploying from zuma.
        //CurrBall.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        //this line switching the currball with nextball and creates new nextball 
        StartCoroutine(CreateBall());
        //this line doing the throwing out of zuma
        CurrBall.GetComponent<Rigidbody2D>().AddForce(transform.right*400); //change 400 here to a serializable that will stand for speed of the ball
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
       switch (count)
       {
        case 0:
           {
            yield return new WaitForSeconds(0.2f);
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,"?",equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
            count++;
            break;
           }
        case 1: //חיסור או חיבור בהכרח
           {
            yield return new WaitForSeconds(0.2f);
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[3],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
             count++;
             break;
           }
        case 2:
           {
            yield return new WaitForSeconds(0.2f);
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[2],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
            count++;
            break;
           }
        case 3: // בהכרח סימן שווה
           {
            yield return new WaitForSeconds(0.2f);
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[1],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);
             count++;
             break;
           }
        case 4:
           {
            yield return new WaitForSeconds(0.2f);
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(false,equation[0],equation[4]).movingprefab, spawnpoint.position, Quaternion.identity);

             count++;
             break;
           }
           default:
           {
            yield return new WaitForSeconds(0.2f);
            GameObject ComposedBall = Instantiate(Balls.instance.myNextBall(true,"","").movingprefab, spawnpoint.position, Quaternion.identity); 
             count=0;
            break;
           }
       }



        

    }
  
}
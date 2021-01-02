using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BallMovement : MonoBehaviour
{
    public static BallMovement instance;
    public static List<BallMovement> ballMovement = new List<BallMovement>();

    private Transform target;
    public bool IsMoving = false;
    public float speed;
    public string SolutionID;
    public string colorID;
    private Vector3 vec;
    
    public bool GameOver=false;


    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        target = Path.instance.pathes[0].pointobject;
        IsMoving = true;
        ballMovement.Add(this);
    }
    void OnDestroy()
    {
        ballMovement.Remove(this);
    }
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (IsMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                Transform next = Path.instance.FindNextPoint(target);
                if (next == null)
                {
                    BallDestroy.instance.GameOver();
                    IsMoving = false;
                    Destroy(gameObject);
                    Debug.Log("Movement Complete");
                    BallMovement.ballMovement.ForEach(x=>{
                        x.IsMoving=false;
                        Destroy(x.gameObject);
                        Zuma.instance.IsFinished=true;
                    });
                    return;
                }
                target = next;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BallMovement>())
        {
            other.GetComponent<BallMovement>().IsMoving = true;
        }
        if(other.tag=="FlipDown"&&colorID=="yellow"){
            this.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, -90);
        }
        else if(other.tag=="FlipUp"&&colorID=="yellow"){
            this.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90);
        }
        else if(other.tag=="FlipRight"&&colorID=="yellow"){
            this.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(other.tag=="FlipLeft"&&colorID=="yellow"){
            this.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsMoving) return;
        
        if (other.GetComponent<BallMovement>())
        {
            other.GetComponent<BallMovement>().IsMoving = true;
        }
    }
    public void SetSpeed(float num){
        this.speed=num;
    }

}
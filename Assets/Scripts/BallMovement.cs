using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallMovement : MonoBehaviour
{
    public static List<BallMovement> ballMovement = new List<BallMovement>();

    private Transform target;
    public bool IsMoving = false;
    public float speed;
    public string SolutionID;
    public string colorID;
    private Vector3 vec;
    public BallMovement instance;


    private void Awake()
    {
        instance=this;
        target = Path.instance.pathes[0].pointobject;
        IsMoving = true;
    }
    private void Start()
    {
        ballMovement.Add(this);
    }
    private void OnDestroy()
    {
        ballMovement.Remove(this);
    }
    private void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (IsMoving)
        {
            switch(EquationMaker.instance.getLevelnum()){
                case 1:
                    speed=1f;
                    break;
                case 2:
                    speed=2f;
                    break;
                default:
                    speed=1f;
                    break;
                }
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                Transform next = Path.instance.FindNextPoint(target);
                if (next == null)
                {
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

}



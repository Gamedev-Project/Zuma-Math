using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallMovement : MonoBehaviour
{
    public static List<BallMovement> ballMovement = new List<BallMovement>();

    private Transform target;
    public bool IsMoving = false;
    public float speed = 0.5f;
    public string SolutionID;
    public string colorID;



    private void Awake()
    {
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
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsMoving) return;
        
        if (other.GetComponent<BallMovement>())
        {
            other.GetComponent<BallMovement>().IsMoving = true;
        }
    }
    [System.Serializable]
    public class Balls
    {
        public static Balls instance;   
        public List<Ballprofile> balls;

        private void Awake()
        {
            instance = this;
        }

        public Ballprofile NextBallSec()
        {
            Ballprofile NextBall = balls[Random.Range(0, balls.Count)];
            return NextBall;
        }

        [System.Serializable]
        public class Ballprofile
        {
            public string colorID;
            public GameObject ballprefab;
            public GameObject movingprefab;
        }
    }
}



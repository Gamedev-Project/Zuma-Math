﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Balls : MonoBehaviour
{
    public static Balls instance;   
    public List<Ballprofile> balls;
    

    private void Awake()
    {
        instance = this;
    }

    public Ballprofile myNextBall(bool flag,string sign,string solution)
    {
        Ballprofile NextBall;
        if(flag){
             NextBall = balls[2];
             NextBall.movingprefab.GetComponent<BallMovement>().SolutionID="braker";
        }
        else if(Zuma.prevBallColor=="yellow"){  //set a new color
            NextBall = balls[Random.Range(0, balls.Count)];
            while (NextBall.colorID == "yellow" )
            {
                 NextBall = balls[Random.Range(0, balls.Count)];
            }
            NextBall.movingprefab.GetComponentInChildren<TextMeshPro>().SetText(sign);
            NextBall.movingprefab.GetComponent<BallMovement>().SolutionID=solution;
           
        }
        else{ // find balls with the same color 
            NextBall = balls[Random.Range(0, balls.Count)];
            while (NextBall.colorID != Zuma.prevBallColor || NextBall.colorID == "yellow" )
            {
                 NextBall = balls[Random.Range(0, balls.Count)];
            }
            
            NextBall.movingprefab.GetComponent<BallMovement>().SolutionID=solution;
            NextBall.movingprefab.GetComponentInChildren<TextMeshPro>().SetText(sign);
        }
        Zuma.prevBallColor=NextBall.colorID;
        return NextBall;

    }
    public Ballprofile NextBallSec(string sign)
    {
        Ballprofile NextBall = balls[Random.Range(0, balls.Count)]; 
        NextBall.ballprefab.GetComponentInChildren<TextMeshPro>().SetText(sign);
        NextBall.ballprefab.GetComponent<BallDestroy>().SolutionID=sign;
        return NextBall;
    }

    [System.Serializable]
    public class Ballprofile
    {
        public string colorID;
        public GameObject ballprefab;
        public GameObject movingprefab;
        public string SolutionID;
    }
}
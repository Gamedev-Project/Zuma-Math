using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BallDestroy : MonoBehaviour
{
    public string SolutionID;
    public string colorID;
    private bool flag= false;
    public bool LevelFinished=false;

    void Update() {
        if(BallMovement.ballMovement.Count==0&&Zuma.instance.IsFinished==true){
            LevelFinished=true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="ball")
        {
            if (other.GetComponent<BallMovement>().SolutionID == SolutionID)
            {
                int findedindex = BallMovement.ballMovement.FindIndex(x => x.gameObject == other.gameObject);

                for (int i = findedindex+1; i <=BallMovement.ballMovement.Count-1; i++)
                {
                    if( BallMovement.ballMovement[i].SolutionID=="last"  || BallMovement.ballMovement[i].SolutionID=="braker")
                    {
                        flag=true;
                        
                    }


                }
                if(flag){
                //if a match in balls is happening, this for loop stops movement in the destroied ball partners from right until their partners from left touching them
                //and then the path keep moving
                if(BallMovement.ballMovement.Count>12){
                    Debug.Log(BallMovement.ballMovement.Count);
                    for (int i = findedindex - 1; i >= 0; i--)
                    {
                        BallMovement.ballMovement[i].IsMoving = false;
                    }
                }
              
                Destroy(other.gameObject);
                Debug.Log("ColliderHappend");
                  
                //both for loop here are stands for destroing the left and right matching balls when a match created
                for (int i = findedindex+1; i <=BallMovement.ballMovement.Count-1; i++)
                {
                    if(BallMovement.ballMovement[i].SolutionID==SolutionID || BallMovement.ballMovement[i].SolutionID=="last"|| BallMovement.ballMovement[i].SolutionID=="braker")
                    {
                        
                        Destroy(BallMovement.ballMovement[i].gameObject);
                    }
                    else
                    {
                        break;
                    }

                }


                for (int i = findedindex-1; i >=0; i--)
                {
                    if (BallMovement.ballMovement[i].SolutionID == SolutionID /*|| BallMovement.ballMovement[i].SolutionID=="braker"*/)
                    {

                        Destroy(BallMovement.ballMovement[i].gameObject);
                    }
                    else
                    {
                        break;
                    }

                }
                }
            }
        }
        Destroy(gameObject);
    }
    public void SetSolutionID(string sol){
        this.SolutionID=sol;
    }
}
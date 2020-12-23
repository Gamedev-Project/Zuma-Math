using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BallDestroy : MonoBehaviour
{
    public string SolutionID;
    public string colorID;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="ball")
        {
            if (other.GetComponent<BallMovement>().SolutionID == SolutionID)
            {
                int findedindex = BallMovement.ballMovement.FindIndex(x => x.gameObject == other.gameObject);


                //if a match in balls is happening, this for loop stops movement in the destroied ball partners from right until their partners from left touching them
                //and then the path keep moving
                for (int i = findedindex - 1; i >= 0; i--)
                {
                    BallMovement.ballMovement[i].IsMoving = false;
                }
              
                Destroy(other.gameObject);
                Debug.Log("ColliderHappend");
                  
                //both for loop here are stands for destroing the left and right matching balls when a match created
                for (int i = findedindex+1; i <=BallMovement.ballMovement.Count-1; i++)
                {
                    if(BallMovement.ballMovement[i].SolutionID==SolutionID)
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
                    if (BallMovement.ballMovement[i].SolutionID == SolutionID || BallMovement.ballMovement[i].SolutionID=="braker" )
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

        Destroy(gameObject);
    }
    public void SetSolutionID(string sol){
        this.SolutionID=sol;
    }
}
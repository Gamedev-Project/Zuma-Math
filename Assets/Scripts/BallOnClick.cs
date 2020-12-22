using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOnClick : MonoBehaviour
{
    //public Zuma zuma;
    void OnMouseDown(){
   // this object was clicked - do something
         //Debug.Log ("object that was hit: "+this.gameObject.GetComponent<BallDestroy>().SolutionID);
         
        //Zuma.instance.CurrBall.transform.SetParent(null);
        //Zuma.instance.CurrBall = null;
        Destroy(Zuma.instance.CurrBall);
        GameObject copy=(GameObject)Instantiate(this.gameObject);
        Zuma.instance.CurrBall=copy;
        copy.transform.position = Zuma.instance.CurrBallPoint.position;
        copy.transform.SetParent(Zuma.instance.transform);
   }
}

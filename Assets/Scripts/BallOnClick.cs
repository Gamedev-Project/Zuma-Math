﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOnClick : MonoBehaviour
{

    //void OnMouseDown(){
        void OnMouseOver(){
            if(Input.GetMouseButtonDown(1)){
                Destroy(Zuma.instance.CurrBall);
                GameObject copy=(GameObject)Instantiate(this.gameObject);
                Zuma.instance.CurrBall=copy;
                copy.transform.position = Zuma.instance.CurrBallPoint.position;
                copy.transform.SetParent(Zuma.instance.transform);
            }
        }
}

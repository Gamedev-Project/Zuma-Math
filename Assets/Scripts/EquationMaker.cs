using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationMaker : MonoBehaviour
{
    public static EquationMaker instance; 
    public string[] equation = new string[5];

    
    private void Awake()
    {
        instance = this;
    }

    public string[] MakeEquation(int level){
        int x=Random.Range(1,10);
        int y=Random.Range(1,10);
        equation[0]=x.ToString();
        equation[1]=ChooseAction(1);
        equation[2]=y.ToString();
        equation[3]="=";
        equation[4]=SolveEquation(x,y,equation[1]);
        return equation;
        
    }
    public string SolveEquation(int x,int y,string action){
        switch(action){
            case "+":
                return (x+y).ToString();
                
            case "-":
                return (x-y).ToString();
                
            case "/":
                return (x/y).ToString();
                
            case "*":
                return (x*y).ToString();
            default:
                return "avi";
        }
    }
    private string ChooseAction(int level){

        if(level<=2){
            int temp=Random.Range(1,3);
            if(temp==1){
                return "+";
            }
            else{
                return "-";
            }
        }
        return "+";
    }
}

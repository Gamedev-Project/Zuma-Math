using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationMaker : MonoBehaviour
{
    public static EquationMaker instance; 
    public string[] equation = new string[5];
    public int Levelnum;
    
    private void Awake()
    {
        instance = this;
    }

    public string[] MakeEquation(int level){
    int x,y;
        equation[1]=ChooseAction(Levelnum);
        switch(level){
            case 1: case 2: case 4: case 6:
                x=Random.Range(1,10); //1 to 9
                y=Random.Range(1,10); //1 to 9
                if(equation[1]=="/"){
                    while(((double)x/(double)y)%1!=0||y==0){
                        x=Random.Range(1,10); //1 to 9
                        y=Random.Range(1,10); //1 to 9
                    }
                }
                break;
            case 3: case 5: case 7:
                x=Random.Range(-9,10); //-9 to 9
                y=Random.Range(-9,10); //-9 to 9
                if(equation[1]=="/"){
                    while(((double)x/(double)y)%1!=0||y==0){
                        x=Random.Range(-9,10); //-9 to 9
                        y=Random.Range(-9,10); //-9 to 9
                    }
                }
                break;
            default:
                x=0;
                y=0;
                break;
        }
        equation[0]=x.ToString();
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
        private string ChooseAction(int level){//add 1 to 2 | sub 3 to 5 | multiple 6 to 15 | divide 16 to 25
        int temp;
        if(level==6){ //this level contains divide, add and sub
            temp=Random.Range(1,26);
            while(5<temp&&temp<16){
                temp=Random.Range(1,26);
            }
        }
        else if(level<4){ //this level contains add and sub
           temp=Random.Range(1,6); 
        }
        else if(level<6){ //this level contains multiple, add and sub
            temp=Random.Range(1,16); 
        }
        else{ //this level contains multiple, divide, add and sub
            temp=Random.Range(1,26);
        }
        switch(temp){
            case 1:case 2:
                return "+";
            case 3:case 4:case 5:
                return "-";
            case 6:case 7:case 8:case 9:case 10:case 11:case 12:case 13:case 14:case 15:
                return "*";
            case 16:case 17:case 18:case 19:case 20:case 21:case 22:case 23:case 24:case 25:
                return "/";
            default:
                return "+";
        }
    }
    public void SetLevelnum(int num){
        this.Levelnum=num;
    }
    public int getLevelnum(){
        return this.Levelnum;
    }
}

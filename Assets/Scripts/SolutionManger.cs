using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SolutionManger : MonoBehaviour
{
    public List<string> Solution;
    private List<int> pos;
    private int temp;
    public static SolutionManger instance;
    private List<GameObject> lst;
    // Start is called before the first frame update
    void Start()
    {
        SetBank();
    }
    void Awake(){
        instance=this;

    }
    public void SetBank(){
                
        lst=AllChilds(gameObject);
        for(int i=0;i<lst.Count;i++){
            string temp1=Random.Range(-9,18).ToString(); //this is the range of our equation solutions
            lst[i].GetComponent<BallDestroy>().SetSolutionID(temp1);
            lst[i].GetComponentInChildren<TextMeshPro>().SetText(temp1);
        }
        pos=new List<int>();
        int count=0; // nuber to be sure we do not get into an endless while loop
        for(int i=0;i<Solution.Count;i++){
            temp=Random.Range(1,lst.Count)-1;
            while(pos.Contains(temp)){
                temp=Random.Range(1,lst.Count)-1;
                count++;
                if(count>=100){
                    break;
                }
            }
            count=0;
            pos.Add(temp);
            lst[temp].GetComponentInChildren<TextMeshPro>().SetText(Solution[i]);
            lst[temp].GetComponent<BallDestroy>().SetSolutionID(Solution[i]);
            if(pos.Count==Solution.Count){
                pos.Clear();
            }
            
        }
    }
    private List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {       
                result.Add(VARIABLE.gameObject);
            } 
        }
        return result;
    }
 
    public void AddSolution(string sol){
        this.Solution.Add(sol);
    }
    public string getRandomFromBank(){
        return lst[Random.Range(0, lst.Count)].GetComponent<BallDestroy>().SolutionID;
    }
}
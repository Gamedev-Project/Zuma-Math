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
    private int count;
    // Start is called before the first frame update
    void Start()
    {
        SetBank();
    }
    public void SetBank(){
                
        List<GameObject> lst=AllChilds(gameObject);
        for(int i=0;i<lst.Count;i++){
            string temp1=Random.Range(-9,18).ToString();
            lst[i].GetComponent<BallDestroy>().SetSolutionID(temp1);
            lst[i].GetComponentInChildren<TextMeshPro>().SetText(temp1);
        }
        pos=new List<int>();
        for(int i=0;i<Solution.Count;i++){
            temp=Random.Range(1,lst.Count)-1;
            while(pos.Contains(temp)){
                temp=Random.Range(1,lst.Count)-1;
                count++;
                if(count>=40){
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
}
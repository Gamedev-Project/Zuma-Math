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
            string temp1;
            int level=EquationMaker.instance.Levelnum;
            switch(level){
                case 1: case 2: case 6:
                    temp1=Random.Range(-8,19).ToString(); //-8 to 18
                    break;
                case 3:
                    temp1=Random.Range(-18,19).ToString(); //-18 to 18
                    break;
                case 4:
                    temp1=Random.Range(-8,82).ToString(); //-8 to 81
                    break;
                case 5: case 7:
                    temp1=Random.Range(-81,82).ToString(); //-81 to 81
                    break;
                default:
                    temp1=Random.Range(-81,82).ToString();
                    break;
            }
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationUNITest
{
	static void Main(string []args){
		for(int i=0;i<100;i++){
			int x=Random.Range(1,10);
			int y=Random.Range(1,10);
			int z= Random.Range(1,4);
			string action="";
			int sol=0;
			switch (z){
				case 1:
					sol=x+y;
					action ="+";
					break;
				case 2:
					sol=x*y;
					action ="*";
					break;
				case 3:
					sol=x-y;
					action ="-";
					break;
				case 4:
					sol=x/y;
					action ="/";
					break;
					
			}
			Debug.Assert(sol.ToString().Equals(EquationMaker.instance.SolveEquation(x,y,action)));
		}
	}
}

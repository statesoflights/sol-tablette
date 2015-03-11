using UnityEngine;
using System.Collections;

[System.Serializable] 
public class gameData {

	public bool[] isActived;

	public gameData(){
		isActived = new bool[10];
		isActived[0]=true;
		isActived[1]=true;
		for (int i=2; i<10; i++)
			isActived [i] = false;
	}
}

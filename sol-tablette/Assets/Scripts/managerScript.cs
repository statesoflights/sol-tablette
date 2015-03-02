using UnityEngine;
using System.Collections;

public class managerScript : MonoBehaviour {

	private GameObject light;
	private GameObject laser;

	void Start () {
		//light = GameObject.FindGameObjectWithTag ("Light");
		 
	}
	
	// Update is called once per frame
	void Update () {
		int i = 0;
		while (i < Input.touchCount) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
				if (Physics.Raycast(ray)){
					Vector3 position = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
					position.y = 1;
					light = Instantiate(Resources.Load("Light"), position, Quaternion.identity) as GameObject;
				}
			}
			i++;
		}
	}
}

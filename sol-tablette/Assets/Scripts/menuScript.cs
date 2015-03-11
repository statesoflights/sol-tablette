using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {
	
	void Update () {
		if(Input.touchCount == 1) {
			if (Input.GetTouch(0).phase.Equals(TouchPhase.Began)) {
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if (Physics.Raycast(ray, out hit)) {
					switch(hit.transform.gameObject.tag){
						case "ExperienceMode" :
							Application.LoadLevel(3);
						break;
						case "FreeMode" :
							Application.LoadLevel(2);
						break;
						case "MiniGames":
							Application.LoadLevel(4);
						break;
						case "Credits":

						break;
						case "Parameters":

						break;
					}
				}
			}
		}
	}
}

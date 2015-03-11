using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour {
	
	void Update () {
		if(Input.touchCount == 1) {
			if (Input.GetTouch(0).phase.Equals(TouchPhase.Began)) {
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if (Physics.Raycast(ray, out hit)) {
					Camera.main.GetComponent<AudioSource>().Play();
					SpriteRenderer renderer = hit.transform.gameObject.GetComponent<SpriteRenderer>();
					renderer.sprite=Resources.Load<Sprite>(renderer.sprite.name+"_on");
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
							Application.LoadLevel(0);
						break;
						case "Parameters":
							saveLoad.game = new gameData();
							saveLoad.Save();
							Application.LoadLevel(0);
						break;
					}
				}
			}
		}
	}
}

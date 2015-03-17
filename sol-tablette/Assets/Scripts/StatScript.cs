using UnityEngine;
using System.Collections;

public class StatScript : MonoBehaviour {
		
	// Update is called once per frame
	void Update () {
		if(Input.touchCount == 1) {
			if (Input.GetTouch(0).phase.Equals(TouchPhase.Began)) {
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if (Physics.Raycast(ray, out hit)) {
					SpriteRenderer renderer = hit.transform.gameObject.GetComponent<SpriteRenderer>();
					renderer.sprite=Resources.Load<Sprite>(renderer.sprite.name+"_on");
					switch(hit.transform.gameObject.tag){
					case "RollBack" :
						Application.LoadLevel(Application.loadedLevel);
						break;
					case "Next" :
						GameObject background = GameObject.Find("Background");
						if(background.tag!="2"){
							saveLoad.game.isActived[int.Parse(background.tag)+1]=true;
							saveLoad.Save();
						}
						Application.LoadLevel(Application.loadedLevel+1);
						break;
					}
				}
			}
		}
	}
}

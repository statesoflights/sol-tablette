using UnityEngine;
using System.Collections;

public class backScript : MonoBehaviour {

	private bool levelSelected;

	void Update () {
		if(Input.touchCount == 1) {
			if (Input.GetTouch(0).phase.Equals(TouchPhase.Began)) {
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if (Physics.Raycast(ray, out hit)) {
					if(hit.transform.gameObject.tag=="0"){
						levelSelected = true;
						lampMoving.gameBegin=false;
						SpriteRenderer renderer = hit.transform.gameObject.GetComponent<SpriteRenderer>();
						renderer.sprite=Resources.Load<Sprite>(renderer.sprite.name+"_on");
					}
				}
				if(levelSelected) {
					Application.LoadLevel(1);
				}
			}
			
		}
	}
}

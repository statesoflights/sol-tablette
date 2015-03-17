using UnityEngine;
using System.Collections;

public class introductionScript : MonoBehaviour {

	private bool next;

	void Start(){
		next = false;
	}

	// Update is called once per frame
	void Update () {
		if(Input.touchCount == 1) {
			if (Input.GetTouch(0).phase.Equals(TouchPhase.Began)) {
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if (Physics.Raycast(ray, out hit)) {
					if(hit.transform.gameObject.tag=="Check"){
						next = true;
						SpriteRenderer renderer = GameObject.FindGameObjectWithTag("Check").GetComponent<SpriteRenderer>();
						renderer.sprite=Resources.Load<Sprite>(renderer.sprite.name+"_on");
					}
					if(next){
						Destroy(gameObject);
					}
				}
			}
		}
	}
}

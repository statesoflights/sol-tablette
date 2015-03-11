using UnityEngine;
using System.Collections;

public class levelScript : MonoBehaviour {

	public static int[] level;
	private bool levelSelected;
	private int currentLevel;
	private gameData gd;

	void Start(){
		level = new int[10];
		level [0] = 1;
		level [1] = 5;
		level [2] = 6;
		levelSelected = false;
		if (saveLoad.isExists ()) {
			saveLoad.Load();
		}
		for (int i=2; i< saveLoad.game.isActived.Length; i++) {
			if(saveLoad.game.isActived[i]){
				GameObject gameLevel = GameObject.FindGameObjectWithTag(i.ToString());
				gameLevel.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("level"+i.ToString());
			}
		}
	}

	void Update () {
		if(Input.touchCount == 1) {
			if (Input.GetTouch(0).phase.Equals(TouchPhase.Began)) {
				RaycastHit hit = new RaycastHit();
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if (Physics.Raycast(ray, out hit)) {
					currentLevel = int.Parse(hit.transform.gameObject.tag);
					if(saveLoad.game.isActived[currentLevel]){
						levelSelected = true;
						SpriteRenderer renderer = hit.transform.gameObject.GetComponent<SpriteRenderer>();
						renderer.sprite=Resources.Load<Sprite>(renderer.sprite.name+"_on");
					}
				}
				if(levelSelected) {
					if(saveLoad.game.isActived[currentLevel]){
						GameObject.Find("Background").GetComponent<AudioSource>().Play();
						Application.LoadLevel(level[currentLevel]);

					}
				}
			}

		}
	}
}

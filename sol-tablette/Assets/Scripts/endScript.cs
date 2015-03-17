using UnityEngine;
using System.Collections;

public class endScript : MonoBehaviour {

	private float barlenght;
	private int count;
	public Texture loadBackground;
	public Texture loadForeground;
	private float currentLoad;
	private bool begin;

	void Start(){
		count = 0;
		begin = false;
		barlenght = 1024/10;
	}

	void OnParticleCollision(GameObject other) {
		begin = true;
		if (count <= 101) {
			count++;
		}
		else{
			GameObject background = GameObject.Find("Background");
			if(background.tag!="2"){
				saveLoad.game.isActived[int.Parse(background.tag)+1]=true;
				saveLoad.Save();
			}
			Application.LoadLevel(Application.loadedLevel+1);
		}
	}

	void OnGUI(){
		if (begin) {
			Vector3 screen = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			screen.z = 0;
			screen.y = Screen.height - (screen.y + 55);
			screen.x -= barlenght / 2; 
			GUI.DrawTexture (new Rect (screen.x, screen.y, barlenght, 10), loadBackground, ScaleMode.ScaleAndCrop);
			GUI.DrawTexture (new Rect (screen.x, screen.y, count, 10), loadForeground, ScaleMode.ScaleAndCrop);
		}
	}
}
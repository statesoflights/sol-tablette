using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameScript : MonoBehaviour {
	
	private List<GameObject> mobs;
	private float timer;
	public static bool timeOut=false;
	public static int score=0;

	void Start(){
		timer = 0.0f;
		timeOut = false;
		mobs = new System.Collections.Generic.List<GameObject>();
		for (int i=0; i < 3; i++) {
			float z = Random.Range (-4.5F, 4.5F);
			Vector3 position = new Vector3 (8.0f,1.0f,z);
			mobs.Add(Instantiate (Resources.Load ("Mob"), position, Quaternion.identity) as GameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		float z = Random.Range (-4.5F, 4.0F);
		Vector3 position = new Vector3 (8.0f, 1.0f, z);
		mobs.Add (Instantiate (Resources.Load ("Mob"), position, Quaternion.identity) as GameObject);
	}
	
	void FixedUpdate () {
		if ((!timeOut)&&(lampMoving.gameBegin)) {
			timer += Time.deltaTime;
			GameObject.FindGameObjectWithTag("Timer").GetComponent<GUIText>().text=timer.ToString("F2");
			GameObject.FindGameObjectWithTag("Score").GetComponent<GUIText>().text=score.ToString();
			if(timer>=30.0F)
				timeOut=true;
			int ind = Random.Range (0, mobs.Count);
			for (int i=0; i< mobs.Count; i++) {
				if (mobs [ind] != null) {
					mobs [ind].transform.Translate (Vector3.left * Time.deltaTime);
					if (mobs [ind].transform.position.x <= -6.8f) {
						Destroy (mobs [ind]);
						mobs.RemoveAt (ind);
						score-=10;
					}
				}
				if ((i < mobs.Count) && (mobs [i] == null)) {
					mobs.RemoveAt (i);
					float z = Random.Range (-4.5F, 4.5F);
					Vector3 position = new Vector3 (8.0f, 1.0f, z);
					mobs.Add (Instantiate (Resources.Load ("Mob"), position, Quaternion.identity) as GameObject);
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameScript : MonoBehaviour {
	
	private List<GameObject> mobs;

	void Start(){
		mobs = new System.Collections.Generic.List<GameObject>();
		for (int i=0; i < 3; i++) {
			float z = Random.Range (-4.5F, 4.5F);
			Vector3 position = new Vector3 (8.0f,1.0f,z);
			mobs.Add(Instantiate (Resources.Load ("Checkpoint"), position, Quaternion.identity) as GameObject);
		}
	}
	
	void FixedUpdate () {
			int ind = Random.Range (0, mobs.Count);
			for (int i=0; i< mobs.Count; i++) {
					mobs [ind].transform.Translate (Vector3.left * Time.deltaTime);
					if (mobs [ind].transform.position.x <= -6.8f) {
							Destroy (mobs [ind]);
							mobs.RemoveAt (ind);
							float z = Random.Range (-4.5F, 4.5F);
							Vector3 position = new Vector3 (8.0f, 1.0f, z);
							mobs.Add (Instantiate (Resources.Load ("Checkpoint"), position, Quaternion.identity) as GameObject);
					}
			}
	}
}

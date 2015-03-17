using UnityEngine;
using System.Collections;

public class interactionScript : MonoBehaviour {

	private int index;

	// Use this for initialization
	void Start () {
		index = 0;
		gameObject.renderer.material.color = new Color(1.0f,1.0f,1.0f,0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(index<20){
			index++;
			gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x+0.01f, 0, gameObject.transform.localScale.z+0.01f);
			gameObject.renderer.material.color += new Color(1.0f,1.0f,1.0f,0.05f);
		}
	}
}

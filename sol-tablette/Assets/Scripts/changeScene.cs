using UnityEngine;
using System.Collections;

public class changeScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(Change());
	}

	IEnumerator Change() {
		yield return new WaitForSeconds(2);
		Application.LoadLevel(1);
	}
}

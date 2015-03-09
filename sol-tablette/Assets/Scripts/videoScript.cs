using UnityEngine;
using System.Collections;

public class videoScript : MonoBehaviour {

	void Start () {
		Handheld.PlayFullScreenMovie ("logo.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
	}
}

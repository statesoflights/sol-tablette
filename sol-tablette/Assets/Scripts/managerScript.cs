using UnityEngine;
using System.Collections;

public class managerScript : MonoBehaviour {

	/**
	 * Objets utilisés par le manager
	 * */
	private Lamp light;
	private Mirror mirror;

	/**
	 * Objet Miroir
	 * */
	public class Mirror {

		private static GameObject mirror;

		private Vector3 position;
		private Vector3 direction;

		private Vector3[] point;

		private int dirPoint;
		private int[] posPoint;

		private int[] touchId;

		private float[] dist;

		public Mirror(int[] index){
			point = new Vector3[3];
			posPoint = new int[2];
			touchId = new int[3];
			
			for (int i=0; i<3; i++) {
				point[i] = Camera.main.ScreenToWorldPoint (Input.GetTouch(index[i]).position);
				point[i].y=1;
				touchId[i]=Input.GetTouch(index[i]).fingerId;
			}
			
			float dist1 = Vector3.Distance(point[0],point[1]);
			float dist2 = Vector3.Distance(point[0],point[2]);
			float dist3 = Vector3.Distance(point[1],point[2]);

			float distMin = dist1;
			Vector3 lookAt = point[2];
			dirPoint = 2;
			position = Vector3.Lerp(point[0],point[1], 0.5f);
			posPoint[0]=0;
			posPoint[1]=1;
			
			if(dist2 < distMin){
				distMin = dist2;
				lookAt = point[1];
				dirPoint = 1;
				position = Vector3.Lerp(point[0],point[2], 0.5f);
				posPoint[0]=0;
				posPoint[1]=2;
			}else if(dist3 < distMin){
				distMin = dist3;
				lookAt = point[0];
				dirPoint = 0;
				position = Vector3.Lerp(point[1],point[2], 0.5f);
				posPoint[0]=1;
				posPoint[1]=2;
			}
			
			direction = lookAt - position;
			direction.y = 1;
			
			position.y = 1;
			
			if (mirror != null)
				Destroy (mirror);
			
			mirror = Instantiate (Resources.Load ("Mirror"), position, Quaternion.LookRotation(direction)) as GameObject;
		}

		public void updateDirection(){
			point [dirPoint] = Camera.main.ScreenToWorldPoint (Input.GetTouch (dirPoint).position);
			point [dirPoint].y = 1;
			
			point [posPoint[0]] = Camera.main.ScreenToWorldPoint (Input.GetTouch (posPoint[0]).position);
			point [posPoint[0]].y = 1;
			point [posPoint[1]] = Camera.main.ScreenToWorldPoint (Input.GetTouch (posPoint [1]).position);
			point [posPoint[1]].y = 1;
			
			position = Vector3.Lerp(point[posPoint[0]],point[posPoint[1]], 0.5f);
			position.y = 1;
			
			direction = point [dirPoint];
			direction.y = 1;
			
			mirror.transform.position = position;
			mirror.transform.LookAt (direction);
		}
		
		public bool CheckTouchID(){
			if (touchId [dirPoint] != Input.GetTouch (dirPoint).fingerId) {
				return false;
			}else if(touchId [posPoint[0]] != Input.GetTouch (posPoint[0]).fingerId){
				return false;
			}else if(touchId [posPoint[1]] != Input.GetTouch (posPoint[1]).fingerId){
				return false;
			}else {
				return true;
			}
		}
	}

	/**
	 * Objet Lampe
	 * */
	public class Lamp {

		private static GameObject lamp;

		private Vector3 position;
		private Vector3 direction;

		private Vector3[] point;  

		private int dirPoint;
		private int[] posPoint;

		private int[] touchId;

		public Lamp(int[] index){
			point = new Vector3[3];
			posPoint = new int[2];
			touchId = new int[3];

			for (int i=0; i<3; i++) {
				point[i] = Camera.main.ScreenToWorldPoint (Input.GetTouch(index[i]).position);
				point[i].y=1;
				touchId[i]=Input.GetTouch(index[i]).fingerId;
			}
			
			float dist1 = Vector3.Distance(point[0],point[1]);
			float dist2 = Vector3.Distance(point[0],point[2]);
			float dist3 = Vector3.Distance(point[1],point[2]);
			
			float distMin = dist1;
			Vector3 lookAt = point[2];
			dirPoint = 2;
			position = Vector3.Lerp(point[0],point[1], 0.5f);
			posPoint[0]=0;
			posPoint[1]=1;
			
			if(dist2 < distMin){
				distMin = dist2;
				lookAt = point[1];
				dirPoint = 1;
				position = Vector3.Lerp(point[0],point[2], 0.5f);
				posPoint[0]=0;
				posPoint[1]=2;
			}else if(dist3 < distMin){
				distMin = dist3;
				lookAt = point[0];
				dirPoint = 0;
				position = Vector3.Lerp(point[1],point[2], 0.5f);
				posPoint[0]=1;
				posPoint[1]=2;
			}
			
			direction = lookAt - position;
			direction.y = 1;
			
			position.y = 1;
			
			if (lamp != null)
				Destroy (lamp);
			
			lamp = Instantiate (Resources.Load ("Light"), position, Quaternion.LookRotation(direction)) as GameObject;

		}

		public void updateDirection(){
			point [dirPoint] = Camera.main.ScreenToWorldPoint (Input.GetTouch (dirPoint).position);
			point [dirPoint].y = 1;

			point[posPoint[0]] = Camera.main.ScreenToWorldPoint (Input.GetTouch (posPoint[0]).position);
			point [posPoint [0]].y = 1;
			point [posPoint [1]] = Camera.main.ScreenToWorldPoint (Input.GetTouch (posPoint [1]).position);
			point [posPoint [1]].y = 1;

			position = Vector3.Lerp(point[posPoint[0]],point[posPoint[1]], 0.5f);
			position.y = 1;

			direction = point [dirPoint];
			direction.y = 1;

			lamp.transform.position = position;
			lamp.transform.LookAt (direction);
		}

		public bool CheckTouchID(){
			if (touchId [dirPoint] != Input.GetTouch (dirPoint).fingerId) {
				return false;
			}else if(touchId [posPoint[0]] != Input.GetTouch (posPoint[0]).fingerId){
				return false;
			}else if(touchId [posPoint[1]] != Input.GetTouch (posPoint[1]).fingerId){
				return false;
			}else {
				return true;
			}
		}

	}

	void Start(){
		light = null;
	}

	int checkAlignment(Vector2 pos1, Vector2 pos2, Vector2 pos3){
		int ret;
		Vector2 dir1 = pos1 - pos2;
		dir1 = dir1.normalized;
		Vector2 dir2 = pos1 - pos3;
		dir2 = dir2.normalized;
		
		float dot =Vector2.Dot(dir1,dir2);
		if(dot < 0.0f)
			dot = -dot;
		if(dot < 0.9f)
			ret = 0;
		else
			ret = 1;

		return ret;

	}

	bool checkPosition(int[] index){
		RaycastHit hit = new RaycastHit();
		for (int i=0; i < 3; i++) {
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(index[i]).position);
			if (Physics.Raycast (ray, out hit)) {
				if(hit.transform.gameObject.tag=="NoObjects"){
					return false;
				}
			}
		}
		return true;
	}

	void FixedUpdate () {
		if (Input.touchCount >= 3) 
		{
			int[] index = new int[3];
			int iteration = Input.touchCount/3;
			for(int i =0; i < iteration; i++){
				index[0]=i;
				index[1]=i+1;
				index[2]=i+2;
				if(Input.GetTouch(i).phase == TouchPhase.Began && Input.GetTouch(i+1).phase == TouchPhase.Began && Input.GetTouch(i+2).phase == TouchPhase.Began) 
				{
					int dot = checkAlignment(Input.GetTouch(i).position, Input.GetTouch(i+1).position, Input.GetTouch(i+2).position);
					if(dot == 0){
						if(checkPosition(index)){
							Camera.main.GetComponent<AudioSource>().Play();
							//Instantiation de la lampe
							light = new Lamp(index);
							//on change la direction du faisceau
							if(light.CheckTouchID()){
								light.updateDirection();
							}
						}
					}else if(dot == 1){
						Camera.main.GetComponent<AudioSource>().Play();
						//Instantiation du miroir
						mirror = new Mirror(index);
						//on change la direction du faisceau
						if(mirror.CheckTouchID()){
							mirror.updateDirection();
						}
					}
				} else {
					if(Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i+1).phase == TouchPhase.Moved && Input.GetTouch(i+2).phase == TouchPhase.Moved){
						int dot = checkAlignment(Input.GetTouch(i).position, Input.GetTouch(i+1).position, Input.GetTouch(i+2).position);
						if(dot == 0){
							if(light!=null){
								if(checkPosition(index)){
									//on change la direction du faisceau
									if(light.CheckTouchID()){
										light.updateDirection();
									}
								}
							}
						}else if(dot==1){
							if(mirror!=null){
								//on change la direction du faisceau
								if(mirror.CheckTouchID()){
									mirror.updateDirection();
								}
							}
						}
					}
				}
			}
	  }
  }

}
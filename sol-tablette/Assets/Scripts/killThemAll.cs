using UnityEngine;
using System.Collections;

public class killThemAll : MonoBehaviour {

	private Lamp light;

    /*
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

	bool checkPosition(int[] index){
		RaycastHit hit = new RaycastHit();
		for (int i=0; i < 3; i++) {
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch(i).position);
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
					if(checkPosition(index)){
						//Instantiation de la lampe
						light = new Lamp(index);
						//on change la direction du faisceau
						if(light.CheckTouchID()){
							light.updateDirection();
						}
					}
				} else {
					if(Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i+1).phase == TouchPhase.Moved && Input.GetTouch(i+2).phase == TouchPhase.Moved){
						if(light!=null){
							if(checkPosition(index)){
								//on change la direction du faisceau
								if(light.CheckTouchID()){
									light.updateDirection();
								}
							}
						}
					}
				}
			}
		}
	}

}

using UnityEngine;
using System.Collections;

public class ButtonContinue : MonoBehaviour {

	GameObject button;

	// Use this for initialization
	void Start () {
		button = (GameObject)this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){			
			Debug.Log("mouse" + button.name);
			//Debug.Log(button.GetInstanceID() + " clicked");			
		}

		if(Input.GetMouseButtonDown(0)){			
			Debug.Log("mousePressed" + button.name);
			//Debug.Log(button.GetInstanceID() + " clicked");			
		}
	}

}

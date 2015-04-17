using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {
	bool dialogShown = false;

    void Awake(){
		Dialoguer.Initialize ();
		}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI(){

				if (GUI.Button (new Rect (10, 10, 100, 20), "Начать Игру")) {
						Dialoguer.StartDialogue (1, dialoguerCallback);
			this.enabled=false;
			//Debug.Log(transform.position.x);
				}
		/*if (!dialogShown && transform.position.x < 378.5 && transform.position.x > 377.5
						&& transform.position.z < 188.5 && transform.position.z > 185.5) {
			Debug.Log("done");
			dialogShown = true;
				}*/
		}
	private void dialoguerCallback(){
		this.enabled = true;
		}
}

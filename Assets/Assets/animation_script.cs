using UnityEngine;
using System.Collections;
//using Model;
public class animation_script : MonoBehaviour {
	public GameObject player;
	public int speedRotation = 3;
	public int speed = 5;
	public AnimationClip anima;
	public int jumpSpeed = 50;
	public bool position = false;
	
	void Start () { 
		player = (GameObject)this.gameObject; 
		//animation.AddClip(anima, "walk");
	}
	void Update(){
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
		{ 
			player.transform.position += player.transform.forward * speed * Time.deltaTime; 
			animation.CrossFade("walk");
		} 
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
		{ 
			player.transform.position -= player.transform.forward * speed * Time.deltaTime; 
		} 
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
		{ 
			player.transform.Rotate(Vector3.down * speedRotation); 
		} 
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
		{ 
			player.transform.Rotate(Vector3.up * speedRotation); 
		} 
		if (Input.GetKeyDown(KeyCode.Space)) 
		{ 
			player.transform.position += player.transform.up * jumpSpeed * Time.deltaTime; 
		} 
	//	Debug.Log (transform.position.ToString());
		if (!position && transform.position.x<1391&&transform.position.x>1325
		    && transform.position.z<1517&&transform.position.z>1506) {
//			Character.Instance.Title="!";
			Debug.Log ("!!!!!!!!!!!!!!!!");
			position = true;
			/*Dialoguer.EndDialogue();
				Dialoguer.StartDialogue(1);*/
			Application.LoadLevel("scene1");
		}
	}
}
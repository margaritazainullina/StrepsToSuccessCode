using UnityEngine;
using System.Collections;
using System;
using Model;

public class Init : MonoBehaviour
{
		Boolean dialogShown = false;
		public GameObject man;
	
		void Awake ()
		{
				Dialoguer.Initialize ();
		}
		// Use this for initialization
		void Start ()
		{
				if ((Character.Instance as Character) == null || Character.Instance.Title == null) {
						StartNewGameInCity ();
				} else {
						LoadGameInCity ();
				}	
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}
		void OnGUI ()
		{
		
				if ((Character.Instance as Character) == null || Character.Instance.Title == null) {
						Dialoguer.StartDialogue (0, dialoguerCallback);
						this.enabled = false;
				}
				//Debug.Log(transform.position.x);
		
				/*if (!dialogShown && transform.position.x < 378.5 && transform.position.x > 377.5
						&& transform.position.z < 188.5 && transform.position.z > 185.5) {
			Debug.Log("done");
			dialogShown = true;
				}*/
		}
		private void dialoguerCallback ()
		{
				this.enabled = true;
		}
	
		void StartNewGameInCity ()
		{
				man.transform.position = new Vector3 (1009.429F, 60F, 1424.001F);
		
				//Dialoguer.StartDialogue (0, dialoguerCallback);
		}
	
		void LoadGameInCity ()
		{
		
		}
}

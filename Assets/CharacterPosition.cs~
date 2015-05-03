using UnityEngine;
using System.Collections;

public class CharacterPosition : MonoBehaviour
{
		private bool position = false;
		private bool positionOffice = false;
		private bool pointReached = false;
		
		public GameObject currentMissionTarget;

		// Update is called once per frame
		void Update ()
		{
				if (!position && transform.position.x < currentMissionTarget.transform.position.x + currentMissionTarget.renderer.bounds.size.x / 2 
						&& transform.position.x > currentMissionTarget.transform.position.x - currentMissionTarget.renderer.bounds.size.x / 2  
						&& transform.position.z < currentMissionTarget.transform.position.z + currentMissionTarget.renderer.bounds.size.z / 2
						&& transform.position.z > currentMissionTarget.transform.position.z - currentMissionTarget.renderer.bounds.size.z / 2) {
						if (Application.loadedLevelName != "office" && !pointReached) {
								Debug.Log ("INSIDE");
								pointReached = true;
								//Application.LoadLevel ("office");
								Dialoguer.StartDialogue (2);
								//StartCoroutine(DisplayLoadingScreen("office"));

								//position = true;
						}
				}
				if (!positionOffice && transform.position.x < 378.5 && transform.position.x > 377.5
						&& transform.position.z < 188.5 && transform.position.z > 185.5) {
						if (Application.loadedLevelName != "city") {
								Application.LoadLevel ("city");
								positionOffice = true;
				
						}
				}
		}

	
		void OnLevelWasLoaded (string level)
		{
				if (level == "city") {
						transform.position = new Vector3 (992.9827f, 31.0f, 1109.696f);
				}
		}
}

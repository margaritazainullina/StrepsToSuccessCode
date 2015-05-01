using UnityEngine;
using System.Collections;
using System;


public class changeAnimation : MonoBehaviour
{
		protected Animator animator;
		// Use this for initialization
		void Start ()
		{
				animator = GetComponent<Animator> ();
		}

		// Update is called once per frame
		void Update ()
		{
				if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
						animator.SetBool ("isWalk", true);
				} else {
						animator.SetBool ("isWalk", false);
				}



		}


		IEnumerator DisplayLoadingScreen (string levelToLoad)
		{
				AsyncOperation async = Application.LoadLevelAsync (levelToLoad);

				while (!async.isDone) {


				}

				Debug.Log ("async");
				yield return null;
		}

}

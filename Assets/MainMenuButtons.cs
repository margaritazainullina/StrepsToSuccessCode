using UnityEngine;
using System.Collections;
using Assets;
using System.Collections.Generic;
using System.Linq;
using Model;
using System;

public class MainMenuButtons : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
			
		}
	
		// Update is called once per frame
		void Update ()
		{
			
		}

		public void NewGame ()
		{
				STSDataOperations.LoadGameFromDB ();
                STSDataOperations.SaveGameToDB();
				//Application.LoadLevel ("city");
		}

		public void ExitGame ()
		{
				Application.Quit ();
		}
}

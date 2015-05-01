using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radar : MonoBehaviour
{
		public GameObject[] TrackedObjects;
		private List<GameObject> radarObjects;
		private List<GameObject> borderObjects;
		public GameObject RadarPrefab;
		public float switchDistance;
		public Transform HelpTransform;
		public Transform man;

		// Use this for initialization
		void Start ()
		{
				CreateRadarObjects ();
		}

		void Update ()
		{
		
				for (int i = 0; i < radarObjects.Count; i++) {
						if (Vector3.Distance (radarObjects [i].transform.position, transform.position) >= switchDistance) {
								//switch to border objects		
								Vector3 vectorOfTheTarget = radarObjects [i].transform.position;

								HelpTransform.LookAt (radarObjects [i].transform);
								HelpTransform.Rotate (HelpTransform.parent.transform.position, 180F);
								//float x = borderObjects [i].transform.position.x;
								borderObjects [i].transform.position = transform.position + 18 * HelpTransform.forward;
								borderObjects [i].transform.position = new Vector3 (borderObjects [i].transform.position.x, borderObjects [i].transform.position.y - 20F, borderObjects [i].transform.position.z);
								radarObjects [i].layer = LayerMask.NameToLayer ("Invisible");
								borderObjects [i].layer = LayerMask.NameToLayer ("MiniMap");
								
						} else {
								//switch to radar objects
								borderObjects [i].layer = LayerMask.NameToLayer ("Invisible");
								radarObjects [i].layer = LayerMask.NameToLayer ("MiniMap");
								Debug.Log ("In");
						}

				}
		}

		// Update is called once per frame
		/*void Update ()
		{

				for (int i = 0; i < radarObjects.Count; i++) {
						if (Vector3.Distance (radarObjects [i].transform.position, HelpTransform.transform.position) > switchDistance) {
								//switch to border objects		
								//HelpTransform.LookAt (radarObjects [i].transform);
								//borderObjects [i].transform.position = borderObjects [i].transform.position + 1;//transform.position;// + HelpTransform.forward; //switchDistance * 
								borderObjects [i].transform.position = new Vector3 (borderObjects [i].transform.position.x + transform.position.x, radarObjects [i].transform.position.y + transform.position.y, borderObjects [i].transform.position.z);

								//Debug.Log ("transform.position" + transform.position);
								Debug.Log ("transform.position.x" + (transform.position.x - 952.5F));
								Debug.Log ("transform.position.y" + (transform.position.y - 129.8F));
								radarObjects [i].layer = LayerMask.NameToLayer ("Invisible");
								borderObjects [i].layer = LayerMask.NameToLayer ("MiniMap");
								Debug.Log ("OUT");
								//borderObjects [i].layer = LayerMask.NameToLayer ("MiniMap");
						} else {
								//switch to radar objects
								//borderObjects [i].layer = LayerMask.NameToLayer ("Invisible");
								borderObjects [i].layer = LayerMask.NameToLayer ("Invisible");
								radarObjects [i].layer = LayerMask.NameToLayer ("MiniMap");
								Debug.Log ("IN");

						}
						/*Debug.Log ("Radar" + radarObjects [i].transform.position);
						Debug.Log ("Tpos:" + HelpTransform.transform.position);
						Debug.Log ("Dist:" + Vector3.Distance (radarObjects [i].transform.position, transform.position));
						Debug.Log ("switchDistance" + switchDistance);
						Debug.Log ("out " + (Vector3.Distance (radarObjects [i].transform.position, HelpTransform.transform.position) > switchDistance));
				}
		}*/

		void CreateRadarObjects ()
		{
				radarObjects = new List<GameObject> ();
				borderObjects = new List<GameObject> ();

				foreach (GameObject item in TrackedObjects) {
						GameObject temp = Instantiate (RadarPrefab, item.transform.position, Quaternion.identity) as GameObject;
						radarObjects.Add (temp);
						GameObject tempTwo = Instantiate (RadarPrefab, item.transform.position, Quaternion.identity) as GameObject;
						borderObjects.Add (tempTwo);
				}
		}
}

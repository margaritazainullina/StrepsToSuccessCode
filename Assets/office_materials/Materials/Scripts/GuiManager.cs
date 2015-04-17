using UnityEngine;

public class GuiManager : MonoBehaviour {

	public RenderTexture MiniMapTexture;
	public Material MiniMapMaterial;

	private float offset;

	// Use this for initialization
	void Awake () {
		offset = 10;
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (Event.current.type == EventType.Repaint) {
			Graphics.DrawTexture (new Rect (Screen.width-256, 
			                                0, 256, 256),
		                     MiniMapTexture, MiniMapMaterial);
				}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterScreen : MonoBehaviour {
	void Start () {
        RenderTexture screen = new RenderTexture(256, 256, 16, RenderTextureFormat.ARGB32);
        screen.Create();
        Camera cam = transform.parent.FindChild("ScreenCam").GetComponent<Camera>();
        cam.targetTexture = screen;
        GetComponent<MeshRenderer>().material.mainTexture = screen;
	}
}

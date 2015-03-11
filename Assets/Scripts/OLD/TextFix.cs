using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextFix : MonoBehaviour {

	// quick fix for 3d text to appear above other layers
	void Start () {
        renderer.sortingLayerID = 0;
        renderer.sortingOrder = 5;
	}

}

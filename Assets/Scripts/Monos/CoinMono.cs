using UnityEngine;
using System.Collections;

public class CoinMono : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        Vector3 view = Camera.main.WorldToViewportPoint(this.transform.position);
        if (view.y <= -0.5f)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
	}
}

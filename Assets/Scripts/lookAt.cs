using UnityEngine;
using System.Collections;

public class lookAt : MonoBehaviour {
	public Transform focus;
	void Update () {
		this.transform.LookAt (focus);
	}
}

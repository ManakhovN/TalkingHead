using UnityEngine;
using System.Collections;

public class FocusPointMover : MonoBehaviour {
    float delta;
    public float maxDelta=4f;
    public float minDelta=0f;
    public Vector3 minOffset;
    public Vector3 maxOffset;
    Vector3 startPosition;
	void Start () {
        this.startPosition = this.transform.position;
        delta = Random.Range(minDelta, maxDelta);
	}
	
	// Update is called once per frame
	void Update () {
        delta -= delta >= 0f ? Time.deltaTime : 0f;
        if (delta < 0)
        {
            Vector3 offset = new Vector3(Random.Range(minOffset.x, maxOffset.x),
                                         Random.Range(minOffset.y, maxOffset.y),
                                         Random.Range(minOffset.z,maxOffset.z));
            this.transform.position = this.startPosition + offset;
            delta = Random.Range(minDelta, maxDelta);
        }
	}
}

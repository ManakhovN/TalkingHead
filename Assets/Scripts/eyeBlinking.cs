using UnityEngine;
using System.Collections;

public class eyeBlinking : MonoBehaviour {
    float delta = 4.2f;
    int blink=0;
    public int blinkingSpeed=40;
    SkinnedMeshRenderer SMR;
    void Start () {
        SMR = this.GetComponent<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        delta -= Time.deltaTime;
        if (delta <= 0 && blink==0)
        {
            blink =blinkingSpeed;
        }
        Blink(9);
        Blink(10);
	}

    void Blink(int id)
    {
        if (blink!=0)
        {
            float shapeWeight = SMR.GetBlendShapeWeight(id);
            if (shapeWeight >= 100)
                blink = -blinkingSpeed;
            if (shapeWeight < 0 && blink<0){
                blink = 0;
                delta = 4.2f + Random.Range(-0.5f,0.5f);
            }
            SMR.SetBlendShapeWeight(id, shapeWeight + blink);
        }
    }
}

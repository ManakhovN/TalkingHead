using UnityEngine;
using System.Collections;

public class Emotions : MonoBehaviour {
    Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    public void setEmotion(int i)
    {
        animator.SetInteger("emotion", i);
    }
}

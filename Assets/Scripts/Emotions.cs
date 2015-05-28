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

    public void setEmotionByName(string emotion)
    {
        switch (emotion)
        {
            case "normal":
                animator.SetInteger("emotion", 0);
                break;
            case "sad":
                animator.SetInteger("emotion", 1);
                break;
            case "happy":
                animator.SetInteger("emotion", 2);
                break;
            case "angry":
                animator.SetInteger("emotion", 3);
                break;
            case "fear":
                animator.SetInteger("emotion", 4);
                break;
            case "shame":
                animator.SetInteger("emotion", 5);
                break;
            case "revulsion":
                animator.SetInteger("emotion", 6);
                break;
            case "interest":
                animator.SetInteger("emotion", 7);
                break;
            case "surprise":
                animator.SetInteger("emotion", 8);
                break;            
        }
    }

    public static bool isEmotion(string input)
    {
        switch (input)
        {

            case "normal":
                return true;
            case "sad":
                return true;
            case "happy":
                return true;
            case "angry":
                return true;
            case "fear":
                return true;
            case "surprise":
                return true;
            case "interest":
                return true;
            case "revulsion":
                return true;
            case "shame":
                return true;
        }
        return false;
    }
}

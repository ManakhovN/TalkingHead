﻿using UnityEngine;
using System.Collections;

public class MSAnimationController : MonoBehaviour
{
    public string message = null;
    //the format of Phonemes is russian + english;
    string aPhoneme = "а" + "a";                    //kzbnm
    string oPhoneme = "оё" + "o";
    string yPhoneme = "ую" + "uqw";
    string blPhoneme = "ы" + "";
    string yaPhoneme = "я" + "iy";
    string iPhoneme = "ией" + "e";
    string eePhoneme = "э" + "l";
    string vPhoneme = "в" + "v";
    string sPhoneme = "стхрцлкд" + "cdstxzyrk";
    string mPhoneme = "мнпбфг" + "fpn";
    string shPhoneme = "шжчщз" + "gjh";
    string vowel = "уеыаоэяиюё";
//    string consonant = "йцкнгшщзхфвпрлджчсмтб";
    string alwaysAnimatedChars = "уеыаоэяиюёв " + "eyuoai";
    public float delta = 0.2f;
    float timer = 0f;
    Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
 //       sayMessage("Hello");
    }

    public void sayMessage(string _message)
    {
        Debug.Log("StartAnimation");
        message = _message;
    }


    void FixedUpdate()
    {
        if (message == null)
        {
            animator.SetTrigger("nothing");
            return;
        }
        timer += Time.fixedDeltaTime;
        if (message.Length > 0)
        {
            string trigger = getTrigger(message.ToLower()[0]);
            //            Debug.Log(trigger);
            if (timer >= delta && message.Length != 0)
            {
                timer = 0;
                if (message.Length > 1 && !isCharIsAlwaysAnimate(message.ToLower()[0]) && !isCharIsAlwaysAnimate(message.ToLower()[1]))
                {
                    message = message.Remove(0, 2);
                }
                else
                    if (message.Length != 0 && !isCharIsAlwaysAnimate(message.ToLower()[0]))
                    {
                        message = message.Remove(0, 1);
                        return;
                    }
                    else
                        if (isCharIsAlwaysAnimate(message.ToLower()[0]))
                            message = message.Remove(0, 1);
                if (trigger != null) animator.SetTrigger(trigger);

            }
        }
        else animator.SetTrigger("nothing");
    }

    string getTrigger(char c)
    {
        if (aPhoneme.IndexOf(c) != -1) return "a";
        if (oPhoneme.IndexOf(c) != -1) return "o";
        if (yPhoneme.IndexOf(c) != -1) return "y";
        if (blPhoneme.IndexOf(c) != -1) return "bl";
        if (yaPhoneme.IndexOf(c) != -1) return "ya";
        if (iPhoneme.IndexOf(c) != -1) return "i";
        if (eePhoneme.IndexOf(c) != -1) return "ee";
        if (vPhoneme.IndexOf(c) != -1) return "v";
        if (sPhoneme.IndexOf(c) != -1) return "s";
        if (mPhoneme.IndexOf(c) != -1) return "m";
        if (shPhoneme.IndexOf(c) != -1) return "sh";
        if (c.Equals(' ')) return "nothing";
        return null;
    }

    bool isCharIsVowel(char c)
    {
        return vowel.IndexOf(c) != -1;
    }

    bool isCharIsAlwaysAnimate(char c)
    {
        return alwaysAnimatedChars.IndexOf(c) != -1;
    }

    public bool isMessageIsNull()
    {
        return this.message == null;
    }
}

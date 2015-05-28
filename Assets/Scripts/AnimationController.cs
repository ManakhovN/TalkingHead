using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;
using System.Threading;
using System.Collections.Generic;

public static class AnimationController
{
    static string tempFolder = System.IO.Path.GetTempPath();
    static int[] aPhoneme = { 10, 11, 12, 15, 16 };
    static int[] oPhoneme = { 13, 14, 35, 36 };
    static int[] yPhoneme = { 22, 23, 43, 44 };
    static int[] blPhoneme = { };
    static int[] yaPhoneme = { 47 };
    static int[] iPhoneme = { 27, 28 };
    static int[] eePhoneme = { 21 };
    static int[] vPhoneme = { 45, 46 };
    static int[] sPhoneme = { 19, 26, 30, 31, 38, 39, 41, 42 };
    static int[] mPhoneme = { 17, 24, 25, 32, 33, 34, 37 };
    static int[] shPhoneme = { 18, 20, 29, 40, 48, 49 }; //sorted by looking for https://msdn.microsoft.com/en-us/library/ms717239(v=vs.85).aspx
    static int[] alwaysAnimatable = { 7, 10, 11, 12, 13, 14, 15, 16, 21, 22, 23, 27, 28, 35, 36, 43, 44, 47 };
    
    public static string getTrigger(int c)
    {
        if (isNumberInArray(c, aPhoneme)) return "a";
        if (isNumberInArray(c, oPhoneme)) return "o";
        if (isNumberInArray(c, yPhoneme)) return "y";
        if (isNumberInArray(c, blPhoneme)) return "bl";
        if (isNumberInArray(c, yaPhoneme)) return "ya";
        if (isNumberInArray(c, iPhoneme)) return "i";
        if (isNumberInArray(c, eePhoneme)) return "ee";
        if (isNumberInArray(c, vPhoneme)) return "v";
        if (isNumberInArray(c, sPhoneme)) return "s";
        if (isNumberInArray(c, mPhoneme)) return "m";
        if (isNumberInArray(c, shPhoneme)) return "sh";
        return "nothing";
    }

    static private bool isNumberInArray(int num, int[] arr)
    {
        foreach (int n in arr)
            if (num == n) return true;
        return false;
    }

    public static bool isAlwaysAnimatable(int c)
    {
        return isNumberInArray(c, alwaysAnimatable);
    }
    public static IEnumerator LoadClip(AudioSource audioSource)
    {
        WWW www = new WWW("file://" + tempFolder + "output.wav");
        while (!www.isDone)
            yield return www;
        AudioClip clip = www.GetAudioClip(false);
        audioSource.clip = clip;
        if (clip.length > 0)
            audioSource.PlayDelayed(0.02f);
    }

    public static void clearFolder()
    {

        DirectoryInfo dir = new DirectoryInfo(tempFolder);
        FileInfo[] info = dir.GetFiles("output.wav");
        if (info.Length > 0) info[0].Delete();
    }
}

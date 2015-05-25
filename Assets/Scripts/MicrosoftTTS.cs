using UnityEngine;
using System.Collections;
using SpeechLib;
using System;
using System.IO;
using UnityEngine.UI;
using System.Threading;
using System.Collections.Generic;

public class MicrosoftTTS : MonoBehaviour
{
  //  Queue
    string tempFolder = System.IO.Path.GetTempPath();
    SpeechLib.SpVoiceClass Voice = new SpeechLib.SpVoiceClass();
    SpeechLib.SpVoiceClass FileWriteVoice = new SpeechLib.SpVoiceClass();
    Queue<EmotionsQueueElement> emotions;
    string message;
    private Animator animator;
    int[] aPhoneme = { 10, 11, 12, 15, 16 };
    int[] oPhoneme = { 13, 14, 35, 36 };
    int[] yPhoneme = { 22, 23, 43, 44 };
    int[] blPhoneme = { };
    int[] yaPhoneme = { 47 };
    int[] iPhoneme = { 27, 28 };
    int[] eePhoneme = { 21 };
    int[] vPhoneme = { 45, 46 };
    int[] sPhoneme = { 19, 26, 30, 31, 38, 39, 41, 42 };
    int[] mPhoneme = { 17, 24, 25, 32, 33, 34, 37 };
    int[] shPhoneme = { 18, 20, 29, 40, 48, 49 }; //sorted by looking for https://msdn.microsoft.com/en-us/library/ms717239(v=vs.85).aspx
    int[] alwaysAnimatable = { 7,10, 11, 12,13, 14, 15, 16, 21, 22, 23, 27, 28, 35, 36, 43, 44, 47};
    int prevPhonemeId = -1;
    public AudioSource audioSource;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }
    int countOfPauses = 0;
    void Update()
    {
        
        int id = Voice.Status.PhonemeId;
        if (isNumberInArray(id, alwaysAnimatable))
            if (Voice != null && animator != null && prevPhonemeId != id)
            { animator.SetTrigger(getTrigger(id));
            if (id == 7)
                countOfPauses++;
            }
    //    Debug.Log(countOfPauses);
           prevPhonemeId = id;
        
    }

    public void Say(int voiceNum, string input)
    {
        Voice.SetVolume(0);
        SpeechLib.ISpeechObjectTokens token = Voice.GetVoices("", "");
        Voice.SetVoice((SpeechLib.ISpObjectToken)token.Item(voiceNum));
        Voice.Speak(input, SpeechVoiceSpeakFlags.SVSFlagsAsync);
    }

    public void SayInFile(int voiceNum, string input)
    {
        Debug.Log(tempFolder);
        clearFolder();
        SpeechLib.ISpeechObjectTokens token = FileWriteVoice.GetVoices("", "");
        SpFileStream SPFileStream = new SpFileStream();
        SPFileStream.Open(tempFolder + "output.wav", SpeechStreamFileMode.SSFMCreateForWrite, false);
        FileWriteVoice.AudioOutputStream = SPFileStream;
        FileWriteVoice.SetVoice((SpeechLib.ISpObjectToken)token.Item(voiceNum));
        FileWriteVoice.Speak(input);
        SPFileStream.Close();
        this.StartCoroutine(LoadClip());
    }
    void clearFolder()
    {
     
        DirectoryInfo dir = new DirectoryInfo(tempFolder);
        Debug.Log(tempFolder);
        FileInfo[] info = dir.GetFiles("output.wav");
        if (info.Length>0) info[0].Delete();
    }

    string getTrigger(int c)
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

    private Boolean isNumberInArray(int num, int[] arr)
    {
        foreach (int n in arr)
            if (num == n) return true;
        return false;
    }

    public IEnumerator LoadClip()
    {
        WWW www = new WWW("file://" + this.tempFolder + "output.wav");
        while (!www.isDone)
            yield return www;
        AudioClip clip = www.GetAudioClip(false);
        this.audioSource.clip = clip;
        if (clip.length>0)
        this.audioSource.Play();
        Debug.Log("LoadingClipEnded");
    }

}

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
public class SpeechMessagesReciever : MonoBehaviour {
    Animator animator;
    AudioSource audioSource;
    int previousPhonemeId;
	void Start () {
        previousPhonemeId = -1;
        animator = this.GetComponent<Animator>();
        Application.runInBackground = true;
        audioSource = this.GetComponent<AudioSource>();
	}

    void Update () {
        this.StartCoroutine(Connect("127.0.0.1", "Ping"));
        if (audioSource.isPlaying == false)
        {
            this.StartCoroutine(AnimationController.LoadClip(audioSource));
            this.GetComponent<Emotions>().setEmotionByName("normal");
        }
        else
            AnimationController.clearFolder();
    }

    IEnumerator Connect(String server, String message)
    {
        try
        {
            Int32 port = 9595;
            TcpClient client = new TcpClient(server, port);
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
            data = new Byte[256];
            String responseData = String.Empty;
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            if (responseData.Equals("done"))
                audioSource.Stop();
            else 
            if (Emotions.isEmotion(responseData)) {
                this.GetComponent<Emotions>().setEmotionByName(responseData);
            }
            else {
                
                int PhonemeId = Convert.ToInt32(responseData);
                if (AnimationController.isAlwaysAnimatable(PhonemeId) && PhonemeId != previousPhonemeId)
                    animator.SetTrigger(AnimationController.getTrigger(PhonemeId));
                previousPhonemeId = PhonemeId;
            }
            stream.Close();
            client.Close();
        }
        catch (ArgumentNullException e)
        {
            Debug.Log(e.ToString());
        }
        catch (SocketException e)
        {
            Debug.Log(e.ToString());
        }

        yield return null;
    }

    void OnDestroy()
    {
        AnimationController.clearFolder();
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpeakingController : MonoBehaviour {
    public string message;
    public int voiceNum = 1;
    public float timeBetweenAnimationAndVoice = 0.2f;
    float timer = 0f;
    bool animationStarted = false;
    void Update () {
	    timer=timer>0?timer-Time.deltaTime:timer;
        if (timer <= 0f && message != null)
        {
            if (!animationStarted){
                StringParser.getQueue(message);
                this.GetComponent<MSAnimationController>().sayMessage(message);
                this.GetComponent<MicrosoftTTS>().Say(voiceNum, message);
                timer = this.timeBetweenAnimationAndVoice;
                animationStarted = true;
            }
            else {
                //this.GetComponent<MicrosoftTTS>().Say(voiceNum, message);
                this.GetComponent<MicrosoftTTS>().SayInFile(voiceNum, message);
                message = null;
                animationStarted = false;
            }
        }
	}

    public void setMessage(string _message)
    {
        message = _message;
    }

    public void onSpeakButtonClick()
    {
        Text text = GameObject.Find("Canvas/InputField/Text").GetComponent<Text>();
        message = text.text;        
 //       this.GetComponent<MicrosoftTTS>().Say(voiceNum, message);
    }
}

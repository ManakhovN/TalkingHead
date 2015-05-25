using UnityEngine;
using System.Collections;
using System.IO;
public class GoogleTTS : MonoBehaviour {
    public string tempFolder = "assets/resources/speech_temp_folder";
    public static string inputPath = "assets/resources/input.txt";
    public string lang = "ru";
    Object[] myMusic;
    int currentIndex = 0;
    bool ready = false;
    public float delayBetweenAnimationAndSound = 0.4f;
    public string message;

    public void Start()
    {
        clearFolder();
        this.StartCoroutine(sayMessage());
    }

    public IEnumerator sayMessage()
    {
        Debug.Log(message);
        string url, partString;
        int startChar = 0;
        Debug.Log("Full Text:\n" + message);
        int times = get_times(message);
        Debug.Log("Need to generate " + times + " files;");
        
        for (int i = 0; i < times; i++)
        {
            partString = new_part(message, startChar);
            //			Debug.Log(i + " PART IS: " + partString);
            startChar += partString.Length;
            //			Debug.Log(partString.Length);

            url = "http://translate.google.com/translate_tts?ie=UTF-8&tl=" + lang + "&total=1&idx=0&client=t&prev=input&textlen=" + partString.Length + "&q=" + WWW.EscapeURL(partString);
            //			Debug.Log(url);
            WWW www = new WWW(url);
            yield return www;
            File.WriteAllBytes(tempFolder + "/temp_part_" + i + ".mp3", www.bytes);
        }

        if (startChar != message.Length)
        {
            Debug.Log("Oops, we need one more. But it is done. Awesome!");
            partString = new_part(message, startChar);
            url = "http://translate.google.com/translate_tts?ie=UTF-8&tl=" + lang + "&total=1&idx=0&client=t&prev=input&textlen=" + partString.Length + "&q=" + WWW.EscapeURL(partString);
            WWW www = new WWW(url);
            yield return www;
            File.WriteAllBytes(tempFolder + "/temp_part_last.mp3", www.bytes);
            //			Debug.Log("LAST PART IS: " + partString);
        }
        Debug.Log("GENERATING DONE!");

        myMusic = Resources.LoadAll("");
        Debug.Log(myMusic.Length);
        ready = true;
    }

    private int get_times(string q)
    {
        int times = 0;
        while (q.Length > 0)
        {
            if (q.Length > 99)
                q = q.Remove(0, 100);
            else
                q = q.Remove(0);
            times++;
        }
        return times;
    }

    private string new_part(string q, int startChar)
    {
        int size;
        bool notHundredSize = false;

        if (q.Substring(startChar).Length < 100)
        {
            size = q.Substring(startChar).Length;
            notHundredSize = true;
        }
        else
            size = 100;

        int begin = startChar, end = begin + size;

        if (!notHundredSize)
            if (end != q.Length && !q[end].Equals(' '))
                while (!q[end - 1].Equals(' '))
                {
                    end -= 1;
                    size -= 1;
                }

        string part = q.Substring(begin, size);
        return part;
    }

    void Speak()
    {
        currentIndex = 0;
        GetComponent<AudioSource>().clip = myMusic[currentIndex] as AudioClip;
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        if (ready && !GetComponent<AudioSource>().isPlaying && currentIndex < myMusic.Length)
            if (this.delayBetweenAnimationAndSound < 0f) playMusic();
            else
            {
                this.delayBetweenAnimationAndSound -= Time.deltaTime;
                AnimationController animationController = this.GetComponent<AnimationController>();
                if (animationController.isMessageIsNull())
                    animationController.sayMessage(message);
              }
    }

    void playMusic()
    {
        GetComponent<AudioSource>().clip = myMusic[currentIndex] as AudioClip;
        GetComponent<AudioSource>().Play();
//        GetComponent<AudioSource>().pitch = 0.5f;
        currentIndex++;
    }
    void clearFolder()
    {
        DirectoryInfo dir = new DirectoryInfo(tempFolder);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo file in info)
        {
            file.Delete();
        }
    }
}

using UnityEngine;
using System.Collections;

public class EmotionsQueueElement{
    private int currentPosition;
    private int emotionNumber;
    public EmotionsQueueElement(int _currentPosition, int _emotionNumber)
    {
        this.currentPosition = _currentPosition;
        this.emotionNumber = _emotionNumber;
    }
    public int CurrentPosition
    { 
        get {   return this.currentPosition; }
        set { this.currentPosition = value; }
    }

    public int EmotionNumber
    {
        get { return this.emotionNumber; }
        set { this.emotionNumber = value; }
    }
}

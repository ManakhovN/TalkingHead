using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StringParser{
    public static Queue<EmotionsQueueElement> getQueue(string input)
    {
        Queue<EmotionsQueueElement> result = new Queue<EmotionsQueueElement>();
        char[] separator = { '{', '}'};
        string[] tokens = input.Split(separator);
        int currentPause = 0;
        for (int i=0; i<tokens.Length; i++){
            if (i % 2 == 0)
            {
                int previousChar = 888;
                for (int j = 0; j < tokens[i].Length; j++)
                {
                    if (tokens[i][j] == 7 && previousChar != 7)
                        currentPause++;
                    previousChar = tokens[i][j];
                }
            }
            else {
                result.Enqueue(new EmotionsQueueElement(currentPause, getEmotionIndexByName(tokens[i])));
            }

        }

        input = "";
        for (int i = 0; i < tokens.Length; i += 2)
            input += tokens[i];
            return result;
    }

    public static int getEmotionIndexByName(string name)
    {
        switch(name)
        {
            case "normal":
                return 0;
            case "sadness":
                return 1;
            case "happiness":
                return 2;
            case "angry":
                return 3;
            case "fear":
                return 4;
            case "shame":
                return 5;
            case "revulsion":
                return 6;
            case "interest":
                return 7;
            case "surprise":
                return 8;
        }
        return 0;
    }
}

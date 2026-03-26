using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Game/Dialog")]
public class DialogData : ScriptableObject
{
    public DialogLine[] lines;

    [Header("Scene Transition")]
    public string nextSceneName;
}

[System.Serializable]
public class DialogLine
{
    public DialogAudio audio;
    public DialogContent content;
    public DialogVisual visual;
}

[System.Serializable]
public class DialogContent
{
    public string speakerName;
    [TextArea(2, 5)]
    public string text;

    //animation effect 
}

[System.Serializable]
public class DialogVisual
{
    public Sprite portrait; //Character Portrait
    [Range(0.0f, 1.0f)]
    public float blurValue;
    public PortraitSide side;
    public Sprite background; // Add blur
    public bool clearPortrait;

}


[System.Serializable]
public class DialogAudio
{
    public AudioClip soundEffect;
    public AudioClip audioBackground;
    public bool audioStop;
}


public enum PortraitSide
{
    LEFT,
    MIDDLE,
    RIGHT
}
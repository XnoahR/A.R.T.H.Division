using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName="Dialog/Dialog Data")]
public class DialogData : ScriptableObject
{
    public DialogLine[] lines;
}

[System.Serializable]
public class DialogLine
{
    public string speakerName;
    [TextArea(2,5)]
    public string text;
    public Sprite portrait;
    public PortraitSide side;
    public Sprite background;
}

public enum PortraitSide
{
    LEFT,
    MIDDLE,
    RIGHT
}
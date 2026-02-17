using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogController : MonoBehaviour
{
    public Image backgroundImage;
    public Image LeftPortrait;
    public Image RightPortrait;

    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogText;

    public DialogData dialogData;
    string currentText = "";
    int index;
    Tween typeTween;
    public float typeSpeed = 0.3f;
    bool isPlaying;
    bool isTyping;

    private void Start()
    {
        PlayDialogue(dialogData);
    }
    public void InitDialogData(DialogData data)
    {
        PlayDialogue(data);
    }

    public void PlayDialogue(DialogData data)
    {
        dialogData = data;
        index = 0;
        isPlaying = true;

        gameObject.SetActive(true);

        ShowLine();
    }

    void ShowLine()
    {
        var line = dialogData.lines[index];

        speakerNameText.text = line.speakerName;
        currentText = line.text;
        TypeWrite(currentText);

        if (line.background != null)
            backgroundImage.sprite = line.background;

        if (line.side == PortraitSide.LEFT)
            SetPortrait(LeftPortrait, line.portrait);
        else
            SetPortrait(RightPortrait, line.portrait);
    }

    void TypeWrite(string textData)
    {
        if (typeTween != null && typeTween.IsActive())
            typeTween.Kill();

        dialogText.text = "";
        isTyping = true;

        float duration = textData.Length * typeSpeed;
        typeTween = DOTween.To(() => dialogText.text, x => dialogText.text = x, textData, duration)
    .SetEase(Ease.Linear)
    .OnComplete(() =>
    {
        isTyping = false;
    });
    }

    void NextLine()
    {
        index++;

        if (index >= dialogData.lines.Length)
        {
            EndDialog();
            return;
        }

        ShowLine();
    }

    void EndDialog()
    {
        isPlaying = false;
        isTyping = false;

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isPlaying) return;

        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        if (isTyping)
            SkipTyping();
        else
            NextLine();
    }

    void SkipTyping()
    {
        typeTween.Kill();
        dialogText.text = currentText;
        isTyping = false;
    }

    void SetPortrait(Image portraitImage, Sprite portraitData)
    {
        portraitImage.sprite = portraitData;
        portraitImage.preserveAspect = true;
        portraitImage.gameObject.SetActive(true);
    }
}
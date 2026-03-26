using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class DialogController : MonoBehaviour
{
    public Image backgroundImage;
    public Image LeftPortrait;
    public Image MiddlePortrait;
    public Image RightPortrait;

    public AudioSource SFXSource;
    public AudioSource BGMSource;

    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogText;
    public DialogData dialogData;
    string currentText = "";
    int index;
    Tween typeTween;
    public float typeSpeed = 0.3f;
    bool isPlaying;
    bool isTyping;

    public static event Action<String> OnSceneChanged;
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
        SFXSource.Stop();
        BGMSource.Stop();
        gameObject.SetActive(true);

        ShowLine();
    }

    void ShowLine()
    {
        if (dialogData.lines[index].audio.soundEffect != null)
        {
            SFXSource.PlayOneShot(dialogData.lines[index].audio.soundEffect);
        }
        if (dialogData.lines[index].audio.audioBackground != null)
        {
            BGMSource.PlayOneShot(dialogData.lines[index].audio.audioBackground);
        }

        var line = dialogData.lines[index];
        if (line.visual.background != null)
        {
            backgroundImage.sprite = line.visual.background;
        }
        speakerNameText.text = line.content.speakerName;
        currentText = line.content.text;
        TypeWrite(currentText);

        if (line.visual.background != null)
            backgroundImage.sprite = line.visual.background;

        if (line.visual.side == PortraitSide.LEFT)
            SetPortrait(LeftPortrait, line.visual.portrait);
        else if (line.visual.side == PortraitSide.RIGHT)
            SetPortrait(RightPortrait, line.visual.portrait);
        else
            SetPortrait(MiddlePortrait, line.visual.portrait);
    }

    void ClearPortrait(PortraitSide side)
    {
        if (side == PortraitSide.LEFT)
            SetPortrait(LeftPortrait, null);
        else if (side == PortraitSide.RIGHT)
            SetPortrait(RightPortrait, null);
        else
            SetPortrait(MiddlePortrait, null);
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

        if (dialogData.lines[index].audio.audioStop)
        {
            BGMSource.Stop();
        }

        if (dialogData.lines[index].visual.clearPortrait)
        {
            ClearPortrait(dialogData.lines[index].visual.side);
        }
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
        if (!string.IsNullOrEmpty(dialogData.nextSceneName))
        {
            OnSceneChanged?.Invoke(dialogData.nextSceneName);
        }
        StartCoroutine(EndDialogTime());
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

    IEnumerator EndDialogTime()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatBarUI : MonoBehaviour
{
    [SerializeField] private STAT_TYPE statType;
    [SerializeField] private TextMeshProUGUI statNameText;
    [SerializeField] private Transform barContainer;

    [SerializeField] private Color activeColor = new Color(1f, 0.5f, 0f);
    [SerializeField] private Color inactiveColor = new Color(0.21f, 0.2f, 0.18f);

    [SerializeField] private RawImage[] bars;

    public STAT_TYPE StatType => statType;

    void Awake()
    {
        CacheBars();
    }

    void CacheBars()
    {
        bars = new RawImage[barContainer.childCount];
        for (int i = 0; i < bars.Length; i++)
        {
            bars[i] = barContainer.GetChild(i).GetComponent<RawImage>(); 
        }
    }

    public void UpdateBar(int level)
    {
        if (bars == null || bars.Length == 0)
            CacheBars();

        level = Mathf.Clamp(level, 0, bars.Length);

        for (int i = 0; i < bars.Length; i++)
        {
            bars[i].color = i < level ? activeColor : inactiveColor;
        }
    }
}

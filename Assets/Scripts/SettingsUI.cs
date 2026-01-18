using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    public Slider BGMSlider;

    public TextMeshProUGUI BGMSliderValue;

    public Slider SESlider;

    public TextMeshProUGUI SESliderValue;

    void Start()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SESlider.value = PlayerPrefs.GetFloat("SEVolume");
    }

    public void SetBGMValueText()
    {
        // Slider‚Ìvalue‚Í0`1‚È‚Ì‚ÅA•ª‚©‚è‚³‚·‚³d‹‚Å100”{‚µ‚Ä‚ ‚°‚é
        BGMSliderValue.text = $"{BGMSlider.value * 100}";
    }

    public void SetSEValueText()
    {
        // Slider‚Ìvalue‚Í0`1‚È‚Ì‚ÅA•ª‚©‚è‚³‚·‚³d‹‚Å100”{‚µ‚Ä‚ ‚°‚é
        SESliderValue.text = $"{SESlider.value * 100}";
    }

}

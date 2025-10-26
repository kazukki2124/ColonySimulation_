using UnityEngine;
using TMPro;

public class ColonistStatusUI : MonoBehaviour
{
    public TextMeshProUGUI StatusText;
    public ColonistAI ColonistAI;

    // Update is called once per frame
    void Update()
    {
        StatusText.text = $"State:{ColonistAI.State}";
    }
}

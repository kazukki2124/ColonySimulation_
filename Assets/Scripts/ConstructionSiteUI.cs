using UnityEngine;
using UnityEngine.UI;


public class ConstructionSiteUI : MonoBehaviour
{
    public Image FillImage;

    public ConstructionSite ConstructionSite;

    void Update()
    {
        FillImage.fillAmount = ConstructionSite.GetProgress;
    }
}

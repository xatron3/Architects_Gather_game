using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
  public TMP_Text tooltipText;
  public GameObject tooltipPanel;
  public static TooltipManager Instance;

  private void Awake()
  {
    if (Instance != null)
      Destroy(gameObject);
    else
      Instance = this;
  }

  public void ShowTooltip(string content, Vector2 position)
  {
    tooltipText.text = content;
    tooltipPanel.transform.position = position;
    tooltipPanel.SetActive(true);
  }

  public void HideTooltip()
  {
    tooltipPanel.SetActive(false);
  }
}

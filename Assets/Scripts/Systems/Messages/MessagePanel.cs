using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace SpellStone.Messages
{
  public class MessagePanel : MonoBehaviour
  {
    public TMP_Text messageText;
    public Image panelBackground; // Reference to the Image component of the panel background

    private Coroutine fadeOutCoroutine;

    public void ShowMessage(string message, Color color)
    {
      messageText.text = message;
      messageText.color = color;
      panelBackground.color = new Color(panelBackground.color.r, panelBackground.color.g, panelBackground.color.b, 1f); // Reset background alpha

      // Start fade out coroutine
      if (fadeOutCoroutine != null)
      {
        StopCoroutine(fadeOutCoroutine);
      }
      fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(5f));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
      // Wait for the specified duration
      yield return new WaitForSeconds(duration);

      // Fade out the panel
      float fadeDuration = 1f; // Time to fade out in seconds
      float elapsedTime = 0f;
      Color initialTextColor = messageText.color;
      Color initialBackgroundColor = panelBackground.color;
      while (elapsedTime < fadeDuration)
      {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / fadeDuration);
        messageText.color = Color.Lerp(initialTextColor, new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, 0f), t);
        panelBackground.color = Color.Lerp(initialBackgroundColor, new Color(initialBackgroundColor.r, initialBackgroundColor.g, initialBackgroundColor.b, 0f), t);
        yield return null;
      }

      // Disable or destroy the panel after fading out
      gameObject.SetActive(false); // Or Destroy(gameObject) if you want to destroy it
    }
  }
}

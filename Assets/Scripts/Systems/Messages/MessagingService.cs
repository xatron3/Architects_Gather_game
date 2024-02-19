using UnityEngine;

namespace SpellStone.Messages
{
  public class MessagingService : MonoBehaviour, IMessagingService
  {
    public MessagePanel messagePanelPrefab;
    private static MessagingService instance;

    public static MessagingService Instance
    {
      get
      {
        if (instance == null)
        {
          instance = FindObjectOfType<MessagingService>();
          if (instance == null)
          {
            Debug.LogError("MessagingService is missing in the scene.");
          }
        }
        return instance;
      }
    }

    private void Awake()
    {
      if (instance != null && instance != this)
      {
        Destroy(gameObject);
      }
      else
      {
        instance = this;
      }
    }

    public void ShowMessage(string message, Color color)
    {
      MessagePanel messagePanel = Instantiate(messagePanelPrefab, transform);
      messagePanel.ShowMessage(message, color);
    }
  }
}
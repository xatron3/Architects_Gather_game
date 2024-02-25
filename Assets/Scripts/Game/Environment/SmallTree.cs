using UnityEngine;
using SpellStone.Messages;

public class SmallTree : MonoBehaviour, IInteractable, ISkillProvider
{
  public float InteractionRadius => 3f;
  public string Tooltip => "Chop down tree";

  public ItemPickupable logPrefab;
  private PlayerController playerController;

  public void Interact(GameObject interactor)
  {
    HandlePlayerInteract(interactor);
  }

  private void HandlePlayerInteract(GameObject interactor)
  {
    interactor.TryGetComponent<PlayerController>(out PlayerController _playerController);
    if (_playerController == null)
    {
      Debug.LogError("PlayerController not found on interactor");
      return;
    }

    playerController = _playerController;

    if (RequiredItem == null)
    {
      MessagingService.Instance.ShowMessage("You need a axe in your Tool Belt to chop down this tree.", Color.red);
      return;
    }

    if (playerController.scripts.skills.GetSkill("Woodcutting").GetSkillLevel() < RequiredLevel)
    {
      MessagingService.Instance.ShowMessage($"You need level {RequiredLevel} woodcutting to chop down this tree.", Color.red);
      return;
    }

    playerController.scripts.skills.PerformSkillAction(this, RequiredItem);

    Destroy(gameObject);

    for (int i = 0; i < 3; i++)
    {
      Vector3 randomPosition = new Vector3(
        Random.Range(-1f, 1f),
        0,
        Random.Range(-1f, 1f)
      );
      Instantiate(logPrefab, transform.position + randomPosition, Quaternion.identity);
    }
  }

  public SkillBase GetSkill()
  {
    return new SkillWoodcutting();
  }

  public int GetExperienceGain()
  {
    return 10;
  }

  public Vector3 GetPosition()
  {
    return transform.position;
  }

  public void OnMoveOutOfRange()
  {
    // Do nothing
  }

  public void OnDrawGizmos()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, InteractionRadius);
  }

  public int RequiredLevel => 1;

  public ToolItem RequiredItem => playerController.scripts.inventory.toolBelt.ContainsToolOfType(ToolType.Axe);
}
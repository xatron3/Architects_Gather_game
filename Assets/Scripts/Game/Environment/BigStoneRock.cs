using UnityEngine;
using SpellStone.Messages;

public class BigStoneRock : MonoBehaviour, IInteractable, ISkillProvider
{
  public float InteractionRadius => 3f;
  public ItemPickupable stonePrefab;
  public string Tooltip => "Mine Big Stone Rock";

  private PlayerController player;

  public void Interact(GameObject interactor)
  {
    interactor.TryGetComponent<PlayerController>(out PlayerController _playerController);
    if (_playerController == null)
    {
      Debug.LogError("PlayerController not found on interactor");
      return;
    }

    player = _playerController;


    if (RequiredItem == null)
    {
      MessagingService.Instance.ShowMessage("You need an pickaxe in your Tool Belt to mine this rock.", Color.red);
      return;
    }

    if (player.scripts.skills.GetSkill("Mining").GetSkillLevel() < RequiredLevel)
    {
      MessagingService.Instance.ShowMessage($"You need level {RequiredLevel} mining to mine this rock.", Color.red);
      return;
    }

    player.scripts.skills.PerformSkillAction(this, RequiredItem);

    Destroy(gameObject);

    for (int i = 0; i < 5; i++)
    {
      Vector3 randomPosition = new Vector3(
        Random.Range(-1f, 1f),
        0,
        Random.Range(-1f, 1f)
      );
      Instantiate(stonePrefab, transform.position + randomPosition, Quaternion.identity);
    }

  }

  public SkillBase GetSkill()
  {
    return new SkillMining();
  }

  public int GetExperienceGain()
  {
    return 30;
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
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, InteractionRadius);
  }

  public int RequiredLevel => 2;

  public ToolItem RequiredItem => player.scripts.inventory.toolBelt.ContainsToolOfType(ToolType.Pickaxe);
}
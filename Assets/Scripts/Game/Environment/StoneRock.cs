using UnityEngine;

public class StoneRock : MonoBehaviour, IInteractable
{
  public ItemPickupable stonePrefab;
  public string Tooltip => "Mine stone";
  public void Interact()
  {
    Destroy(gameObject);

    // Instantiate 3 logs at random positions around the tree
    for (int i = 0; i < 3; i++)
    {
      Vector3 randomPosition = new Vector3(
        Random.Range(-1f, 1f),
        0,
        Random.Range(-1f, 1f)
      );
      Instantiate(stonePrefab, transform.position + randomPosition, Quaternion.identity);
    }
  }
}
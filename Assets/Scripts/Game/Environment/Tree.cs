using UnityEngine;

public class Tree : MonoBehaviour, IInteractable
{
  public InteractableItem logPrefab;
  public string Tooltip => "Chop down tree";
  public void Interact()
  {
    Debug.Log("Tree chopped down");
    Destroy(gameObject);

    // Instantiate 3 logs at random positions around the tree
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
}
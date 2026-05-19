using UnityEngine;
using System.Collections;

namespace DefaltNamespace
{
public class EndGame : MonoBehaviour, Iinteractable
{
    public string InteractMessage => objectInteractMessage;
    [SerializeField]  GameObject floor;
    [SerializeField] string objectInteractMessage;
    IEnumerator End()
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(8f);
        floor.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        Application.Quit();
    }

    public void interact()
    {
        StartCoroutine(End());
    }
}
}

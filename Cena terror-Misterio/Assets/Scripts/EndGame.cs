using UnityEngine;
using System.Collections;

namespace DefaltNamespace
{
public class EndGame : MonoBehaviour, Iinteractable
{
    [SerializeField] GameObject ritual;
    [SerializeField] GameObject floor;

    public string InteractMessage => objectInteractMessage;
    [SerializeField] string objectInteractMessage;

    IEnumerator End()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(ritual);
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

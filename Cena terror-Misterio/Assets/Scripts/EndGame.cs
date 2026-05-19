using UnityEngine;
using System.Collections;
using UnityEditor;

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
            gameObject.GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(1.5f);
            Destroy(ritual);
            yield return new WaitForSeconds(8f);
            floor.SetActive(false);
            yield return new WaitForSeconds(2.5f);
            Exit();
        }

        public void interact()
        {
            StartCoroutine(End());
        }

        public void Exit()
        {
            Debug.Log("Game Over!");

#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
        }
    }
}

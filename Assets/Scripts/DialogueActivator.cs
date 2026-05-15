using UnityEngine;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour
{

    private bool canActivate = false;
    [SerializeField] private string[] lines;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (
            canActivate && 
            Input.GetButtonDown("Fire1") && 
            lines.Length > 0 && 
            !DialogueManager.instance.DialogueUI.activeInHierarchy)
            {
                DialogueManager.instance.DisplayDialogue(lines);
            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}

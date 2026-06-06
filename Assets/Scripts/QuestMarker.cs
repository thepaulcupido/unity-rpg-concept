using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    // inspector-available fields
    [Header("Quest Marker Settings")]
    [SerializeField] private string questToMark;
    [SerializeField] private bool markComplete;
    [SerializeField] private bool markOnEnter;
    [SerializeField] private bool deactivateAfterMarking;
    
    // private variables
    private bool canBeMarked = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeMarked && Input.GetButtonDown("Fire1"))
        {
            canBeMarked = false;
            MarkQuest();
        }
    }

    public void MarkQuest()
    {
        QuestManager.instance.UpdateQuestStatus(questToMark, markComplete);
        gameObject.SetActive(!deactivateAfterMarking);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (markOnEnter)
            {
                MarkQuest();
                return;
            }
            canBeMarked = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canBeMarked = false;
        }
    }
}

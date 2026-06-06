using UnityEngine;

/// <summary>
/// This class is responsible for managing the overall state of quests in the game, 
/// including tracking which quests are complete and providing methods to update quest status and 
/// notify relevant game objects of changes in quest status.
/// </summary>
public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [SerializeField] private bool[] questMarkerCompleted;
    [SerializeField] private string[] questMarkerNames;

    public void Awake()
    {
        if (instance != null && instance != this)        
        {
            Debug.LogWarning("Multiple instances of Quest Manager detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        questMarkerCompleted = new bool[questMarkerNames.Length];
    }

    /// <summary>
    /// Checks if a quest is complete based on its name.
    /// </summary>
    /// <param name="questName"></param>
    /// <returns></returns>
    public bool IsQuestComplete(string questName)
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerNames[i] == questName)
            {
                return questMarkerCompleted[i];
            }
        }
        return false;
    }


    /// <summary>
    /// This method updates the completion status of a quest based on its name.
    /// </summary>
    /// <param name="questName"></param>
    /// <param name="isComplete"></param>
    public void UpdateQuestStatus(string questName, bool isComplete)
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkerNames[i] == questName)
            {
                questMarkerCompleted[i] = isComplete;
                string status = isComplete ? "complete" : "incomplete";
                Debug.Log($"Quest '{questName}' marked as {status}.");
                return;
            }
        }
        Debug.LogWarning($"Quest '{questName}' not found in quest markers.");
    }

    /// <summary>
    /// This method marks a quest as complete by calling the UpdateQuestStatus method with isComplete set to true.
    /// </summary>
    /// <param name="questName"></param>
    public void MarkQuestComplete(string questName)
    {
        UpdateQuestStatus(questName, true);
    }

    /// <summary>
    /// This method marks a quest as incomplete by calling the UpdateQuestStatus method with isComplete set to false.
    /// </summary>
    /// <param name="questName"></param>
    public void MarkQuestIncomplete(string questName)
    {
        UpdateQuestStatus(questName, false);
    }

    /// <summary>
    /// This method finds all QuestObject instances in the scene and calls their CheckQuestStatus method 
    /// to update their active state based on the current quest completion status.
    /// </summary>
    public void UpdateLocalQuestObjects()
    {
        QuestObject[] questObjects = FindObjectsByType<QuestObject>();

        if (questObjects.Length == 0)
        {
            Debug.LogWarning("No QuestObjects found in the scene to update.");
            return;
        }

        for (int i = 0; i < questObjects.Length; i++)
        {
            questObjects[i].CheckQuestStatus();
        }
        
        
    }

}

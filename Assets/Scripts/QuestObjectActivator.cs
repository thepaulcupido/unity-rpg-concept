using UnityEngine;

/// <summary>
/// This class is responsible for activating or deactivating game objects based on the 
/// completion status of specifix quests.
/// </summary>
public class QuestObject : MonoBehaviour
{
    [Header("Quest Object Settings")]
    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private bool isActiveIfQuestCompleted;
    [SerializeField] private string questToCheck;

    private bool initCheckDone = false;

    // Update is called once per frame
    void Update()
    {
        if (!initCheckDone)
        {
            objectToActivate.SetActive(isActiveIfQuestCompleted);
            CheckQuestStatus();
            initCheckDone = true;
        }
    }

    /// <summary>
    /// Checks the status of the specified quest and activates/deactivates the object accordingly.
    /// </summary>
    public void CheckQuestStatus()
    {
        bool isQuestCompleted = QuestManager.instance.IsQuestComplete(questToCheck);

        if (isQuestCompleted)
        {
            objectToActivate.SetActive(isActiveIfQuestCompleted);
        }

    }
}

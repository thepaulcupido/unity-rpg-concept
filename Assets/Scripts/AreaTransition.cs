using UnityEngine;

/// <summary>
/// Base component used to identify and manage scene transition points.
///
/// Stores a transition name that can be shared between
/// AreaExit and AreaEntrance objects to determine where
/// the player should appear after changing scenes.
///
/// Example:
/// An AreaExit with transition name "HouseEntrance"
/// can load another scene containing an AreaEntrance
/// with the same transition name.
/// </summary>
public class AreaTransition : MonoBehaviour
{
    /// <summary>
    /// Unique identifier used to match exits and entrances between scenes.
    /// </summary>
    [SerializeField] private string areaTransitionName = "1-1";

    public void SetAreaTransitionName(string newAreaTransitionName)
    {
        areaTransitionName = newAreaTransitionName;
    }

    public string GetAreaTransitionName()
    {
        return areaTransitionName;
    }
}

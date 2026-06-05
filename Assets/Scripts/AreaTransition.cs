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

    // Inspector-available fields
    [SerializeField] private string areaTransitionName = "1-1";

    public string AreaTransitionName => areaTransitionName;

    // TODO - write this setter in a modern way using C# properties and expression-bodied members
    public void SetAreaTransitionName(string newAreaTransitionName)
    {
        areaTransitionName = newAreaTransitionName;
    }
    
}

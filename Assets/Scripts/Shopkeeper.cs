using UnityEngine;

/// <summary>
/// Handles player interaction with a shopkeeper NPC.
///
/// When the player enters the shopkeeper's trigger area,
/// they are allowed to open the shop interface by pressing
/// the interact button. The class also manages loading the
/// configured items for sale into the shop system.
///
/// Requires:
/// - A 2D trigger collider attached to the GameObject.
/// - A player object tagged as "Player".
/// - An active Shop singleton instance.
/// </summary>
public class Shopkeeper : MonoBehaviour
{    
    private bool playerInRange;

    // TODO: refactor to use an array of Items instead of array of string IDs
    [SerializeField] private string[] itemsForSale = new string[36];

    /// <summary>
    /// Checks for player interaction input while the player
    /// is within the shopkeeper's trigger area.
    ///
    /// If the player presses the interact button and:
    /// - player movement is enabled
    /// - the shop menu is not already open
    /// - the player is close enough to interact
    ///
    /// then the shop UI is opened and the configured
    /// items are loaded into the shop.
    /// </summary>
    private void Update()
    {
        if (!playerInRange) return;
        if (!Input.GetButtonDown("Fire1"))  return;

        Shop shop = Shop.instance;

        if (shop.IsOpen) return;
        if (!PlayerController.instance.IsPlayerMovementEnabled()) return;
       
        shop.SetItemsForSale(itemsForSale);
        shop.OpenShop();
    }

    /// <summary>
    /// Called automatically by Unity when another 2D collider
    /// enters this object's trigger collider.
    /// </summary>
    /// <param name="other">
    /// The collider that entered the trigger area.
    /// </param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    /// <summary>
    /// Called automatically by Unity when another 2D collider
    /// exits this object's trigger collider.
    /// </summary>
    /// <param name="other">
    /// The collider that left the trigger area.
    /// </param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

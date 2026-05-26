using System.Runtime.CompilerServices;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{

    private bool canOpen;
    [SerializeField] private string[] itemsForSale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GameObject shopObj = Shop.instance.GetShopMenu();

        bool canMove = PlayerController.instance.IsPlayerMovementEnabled();
        bool isShopActive = !shopObj.activeInHierarchy;

        if (canOpen && Input.GetButtonDown("Fire1") && canMove && isShopActive)
        {
            Shop.instance.SetItemsForSale(itemsForSale);
            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canOpen = false;
        }
    }

        private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canOpen = true;
        }
    }
}

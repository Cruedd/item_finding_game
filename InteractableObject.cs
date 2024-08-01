using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInteractable
{
    public void Interact(PlayerInventory inventory);
}
public class InteractableObject : MonoBehaviour, IInteractable
{
    public Collectables item;
    public void Interact(PlayerInventory inventory)
    {
        if (!item )
        {
            Debug.LogError(gameObject.name+" item object is null");
            return;
        }

        inventory.AddItem(item.name);
        Destroy(gameObject);

        if (item.name != "LockKey")
            MarketList.Instance.CheckList();
        else if (item.name == "LockKey")
        {
            ItemNameUI.Instance.ItemInfo("The Lock Key Taken");
            ItemNameUI.Instance.importantInfo = true;
            MarketList.Instance.TakenTheKey();
        }

    }
   


}

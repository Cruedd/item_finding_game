using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutDoorOpen : MonoBehaviour
{
    public OutDoorOpen[] doors;
    public int doorID;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 antirotation;
    [SerializeField] private Vector3 lockedrotation;
    [SerializeField] private Vector3 lockedantirotation;
    [SerializeField] private Vector3 normal;
    public bool opened;
    public bool locked = false;

    public OutDoorOpen doorOpen;
    public OutDoorOpen doorOpen2;

    private void Start()
    {
        if (doorOpen == null)
        {
            doorOpen = FindObjectOfType<OutDoorOpen>();
        }
    }

    public void EndingDoorOpened()
    {
        doorOpen2.locked = false;
        doorOpen2.DoorOpened();
        doorOpen.locked = false;
        doorOpen.DoorOpened();
    }
    public void DoorOpened()
    {
        if (!opened && !locked)
        {
            transform.DOLocalRotate(rotation, 1f);
            opened = true;
        }
        else if (opened && !locked)
        {
            transform.DOLocalRotate(antirotation, 1f);
            opened = false;
        }
        if (locked)
        {
            transform.DOLocalRotate(lockedrotation, 0.1f).OnComplete(() =>
            {
                transform.DOLocalRotate(lockedantirotation, 0.1f).OnComplete(() =>
                {
                    transform.DOLocalRotate(normal, 0.5f);
                });
            });

        }

    }

    public void Interact(PlayerInventory inventory)
    {

            Debug.Log("This door cannot be unlocked as it is an outdoor door.");
            ItemNameUI.Instance.ItemInfo("You have to collect all items", 1f);
            return;


    }


    public void LockTheDoor()
    {
        if (locked)
        {
            Debug.Log("kilit açýldý");
            locked = false;
            MarketList.Instance.keyItemCount++;
        }
        else if (MarketList.Instance.keyItemCount > 0 && locked == false)
        {
            Debug.Log("kilitlendi");
            locked = true;
            MarketList.Instance.keyItemCount--;

        }

    }
}

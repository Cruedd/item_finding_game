using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class DoorOpen : MonoBehaviour , IInteractable
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


    public void DoorOpened()
    {
        if (!opened && !locked)
        {
            transform.DOLocalRotate(rotation, 1f);
            opened = true;
        }
        else if(opened && !locked)
        {
            transform.DOLocalRotate(antirotation, 1f);
            opened = false;
        }
        if (locked)
        {
            transform.DOLocalRotate(lockedrotation, 0.1f).OnComplete(() => 
            {
                transform.DOLocalRotate(lockedantirotation, 0.1f).OnComplete(()=>
                {
                    transform.DOLocalRotate(normal, 0.5f);
                });
            });
            
        }

    }

    public void Interact(PlayerInventory inventory)
    {
        DoorOpened();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OutlineComponent;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public LayerMask layerMask;
    public Outline lastOutlineObject;
    RaycastHit hitInfo;

    

    void LateUpdate()
    {
        if (Physics.Raycast(InteractorSource.position, InteractorSource.forward, out hitInfo, InteractRange, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E) && hitInfo.transform.TryGetComponent<IInteractable>(out IInteractable interactObj))
            {
                interactObj.Interact(MarketList.Instance.playerInventory);
            }
            if (hitInfo.transform.GetComponent<InteractableObject>() )
            {
                ItemNameUI.Instance.WritingNameOnUI(hitInfo.transform.GetComponent<InteractableObject>().item.name);
            }
            else if (hitInfo.transform.tag == "OutDoor")
            {
                ItemNameUI.Instance.ItemInfo("You have to collect all items for escape");
            }
            else
            {

                ItemNameUI.Instance.WritingNameOnUI("Press 'E' to interact.");

            }
                
            if (hitInfo.transform.GetComponent<DoorOpen>())
            {
                if (Input.GetKeyDown(KeyCode.F) )
                {
                    
                    hitInfo.transform.GetComponent<DoorOpen>().LockTheDoor();
                    Debug.Log("dsadsa0");
                    

                }
                if (hitInfo.transform.GetComponent<OutDoorOpen>())
                {

                }

                if (hitInfo.transform.GetComponent<DoorOpen>().locked == true)
                {
                ItemNameUI.Instance.ItemInfo("The door is locked if you locked your self you can take your key and unlock the door", 1f);
                }
                else if (MarketList.Instance.keyItemCount > 0)
                {
                ItemNameUI.Instance.ItemInfo("You have a key you can lock the objects with press 'F' button", 1f);
                }
                else
                {
                    ItemNameUI.Instance.ItemInfo("");
                }
            }
            
            ChangeOutlineObject();

        }
        else
        {
            ItemNameUI.Instance.WritingNameOnUI("");
            ChangeOutlineObject();
        }


    void ChangeOutlineObject()
    {
            if (hitInfo.transform && hitInfo.transform.GetComponent<Outline>() && !lastOutlineObject)
            {
                SetOutline(hitInfo.transform.GetComponent<Outline>());
            }
            else if (hitInfo.transform && hitInfo.transform.GetComponent<Outline>() && lastOutlineObject)
            {
                ResetOutline();
                SetOutline(hitInfo.transform.GetComponent<Outline>());
            }
            else
            {
                if (lastOutlineObject)
                {
                    ResetOutline();
                }
            }
        }

        if (hitInfo.transform == null && lastOutlineObject)
        {
            ResetOutline();
        }
    }
    void SetOutline(Outline outline)
    {
        lastOutlineObject = outline;
        lastOutlineObject.OutlineWidth = 23;
        
    }

    void ResetOutline()
    {
        lastOutlineObject.OutlineWidth = 0;
        lastOutlineObject = null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(InteractorSource.position, InteractorSource.forward * InteractRange);
    }
}

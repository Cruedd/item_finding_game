using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemNameUI : MonoBehaviour
{
    #region Singleton
    public static ItemNameUI Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public Text itemNameText;
    public Text itemInfoText;
    public bool importantInfo = false;
    public Text keyCountInfo;


    public void WritingNameOnUI(string itemName)
    {
        itemNameText.text = itemName;
        
    }
    public void ItemInfo(string itemInfo,float resetDelay = 5f)
    {
        if (importantInfo)
        {
            Invoke("WaitImportantInfo", 5.0f);
        }
        else
        {
            itemInfoText.text = itemInfo;
            Invoke("ResetItemInfo", resetDelay);
        }
        
    }
    public void WritingKeyCount(float Count)
    {
        keyCountInfo.text = Count.ToString();
    }

    private void ResetItemInfo()
    {
        itemInfoText.text = "";
    }
    private void WaitImportantInfo()
    {
        importantInfo = false;
    }

}

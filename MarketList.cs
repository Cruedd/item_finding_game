using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class MarketList : MonoBehaviour
{
    #region Singleton

    public static MarketList Instance;
    public static bool isPaused = false;
    public GameObject youLostMenu;
    public GameObject youWinMenu;
    public OutDoorOpen outDoorOpen;
    public GameObject camera;
    public GameObject mainCamera;
    private bool cameraChanged = false;
    private void Awake()
    {
        Instance = this;
    }

    #endregion
    [SerializeField] private List<Collectables> allItems;
    [SerializeField] private List<Collectables> isNeedToBeFoundItems;
    [SerializeField] private int minNeedToBeFoundItemCount = 0;
    [SerializeField] private int maxNeedToBeFoundItemCount = 3;
    [SerializeField] public int keyItemCount = 0;
    [SerializeField] private TextMeshProUGUI uiText;
    public GameObject PauseMenu;
    public PlayerInventory playerInventory;
    public void Start()
    {

        if (outDoorOpen == null)
        {
            outDoorOpen = FindObjectOfType<OutDoorOpen>();
        }
        ItemNameUI.Instance.WritingKeyCount(0);
        ResumeGame();
        int random = Random.Range(minNeedToBeFoundItemCount, maxNeedToBeFoundItemCount);

        for (int i = 0; i < random; i++)
        {
            int randomItem = Random.Range(0, allItems.Count);
            Collectables collectables = allItems[randomItem];

            if (!isNeedToBeFoundItems.Contains(collectables))
            {
                isNeedToBeFoundItems.Add(collectables);
            }
            else
            {
                i--;
            }
        }
        ListWriter();
    }

    public void ListWriter()
    {
        if (uiText != null)
        {
            uiText.text = "Bulunmas� gereken itemler \n";
            foreach (Collectables item in isNeedToBeFoundItems)
            {
                string symbol = playerInventory.HasItem(item.name) ? "+" : "-";
                uiText.text += item.name + " "+ symbol + "\n";
            }
        }
    }

    public void CheckList()
    {
        int missingItemCount = 0; // Eksik ��e sayac�
        int notmatchedItemCount = 0; // Uyumsuz ��e sayac�
        bool allItemsFound = true;
        //t�m gerekli ��eler topland� m� kontrol
        foreach (Collectables item in isNeedToBeFoundItems)
        {
            if (!playerInventory.HasItem(item.name))
            {

                allItemsFound = false;
                Debug.Log("Eksik ��e: " + item.name);
                missingItemCount++; // Eksik ��e say�s�n� art�r
            }
        }
        //envanterde ne kadar gereksiz ��e var kontrol

        //buna BAKILICAK
        foreach (string item in playerInventory.items)
        {
            bool isItNeedToBeFound = false;
            foreach (Collectables needItem in isNeedToBeFoundItems)
            {
                if (item == needItem.name)
                {
                    // bu k�sma e�er item name key ise �unu yap eklemek istiyorum
                    isItNeedToBeFound = true;
                    break;
                }
            }
            if (!isItNeedToBeFound )
            {
                Debug.Log("Uyumsuz ��e: " + item);
                notmatchedItemCount++; 
            }
        }
        Debug.Log("Eksik ��e say�s�: " + missingItemCount);
        Debug.Log("Uyumsuz ��e say�s�: " + notmatchedItemCount);


        if (notmatchedItemCount == 3)
        {
            Debug.Log("oyunu kaybettin");
            youLostMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        ListWriter();
        if (allItemsFound)
        {
            OnAllItemsFound();
        }
        else
        {
            OnItemMissing();
        }
    }

    private void OnItemMissing()
    {
        Debug.Log("Baz� itemler hala kay�p, oyun devam ediyor.");
    }

    private void OnAllItemsFound()
    {
        ChangeCameras();
        Debug.Log("T�m itemler bulundu, oyunu kazand�n�z.");
        Invoke("EndingDoorOpener", 1f);
        

        
    }
    public void EndingDoorOpener()
    {
        outDoorOpen.EndingDoorOpened();
        Invoke("ChangeCameras", 3f);

    }

    public void ChangeCameras()
    {
        if (cameraChanged == false)
        {
            cameraChanged = true;

            mainCamera.active = false;
            camera.active = true;
        }
        else if (cameraChanged == true) 
        {
            cameraChanged = false;

            mainCamera.active = true;
            camera.active = false;
        }

    }
    public void PausedGame()
    {
        PauseMenu.SetActive(true);
        GameFreaze();
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        UnGameFreaze();
    }
    public void GameFreaze()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void UnGameFreaze()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void TakenTheKey()
    {
        keyItemCount++;
        ItemNameUI.Instance.WritingKeyCount(keyItemCount);
    }
    public void YouEscapedYouWin()
    {
        youWinMenu.SetActive(true);
        GameFreaze();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && youLostMenu.active == false && youWinMenu.active == false)
        {
            if (!isPaused)
            {
                PausedGame();
                isPaused = true;
            }
            else if (isPaused)
            {
                ResumeGame();
                isPaused = false;
            }
        }
    }
}
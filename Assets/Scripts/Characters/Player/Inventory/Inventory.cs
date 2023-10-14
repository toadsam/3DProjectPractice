using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

   
    public GameObject inventoryInfo;
   
    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private int curEquipIndex;
   

   

    public static Inventory instance;


    void Awake()
    {
        instance = this;      
    }
    private void Start()
    {
        slots = new ItemSlot[uiSlots.Length]; // ������ ��� �迭 ����

        for (int i = 0; i < slots.Length; i++)  //�� ���Կ� ��ȣ�� �ٿ��ְ� 
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();  //���� Ŭ����
        }
    }

   

    public void AddItem(ItemData item)
    {
        slots[item.itemNum].item = item;
        //ItemSlot slotToStackTo = slots[item.itemNum]; //�ٷ� ������ȣ ���̱�
        UpdateUI(item.itemNum);
    }

    void UpdateUI(int itemNum) //������ȣ�� �����۸� ���ε� �ϵ���
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[itemNum].item != null)
                uiSlots[itemNum].Set(slots[itemNum]);
            else
                uiSlots[itemNum].Clear();
        }
    }


    public void SelectItem(int index)
    {
        if (slots[index].item == null)
            return;
        inventoryInfo.SetActive(true); //�ϴ� �ӽ÷� �����
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;

        for (int i = 0; i < selectedItem.item.consumables.Length; i++)
        {
            selectedItemStatNames.text += selectedItem.item.consumables[i].type.ToString() + "\n";
            selectedItemStatValues.text += selectedItem.item.consumables[i].value.ToString() + "\n";
        }
        useButton.SetActive(true);
      
        dropButton.SetActive(true);
    }

    private void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatNames.text = string.Empty;
        selectedItemStatValues.text = string.Empty;
        inventoryInfo.SetActive(false);    
    }

    public void OnUseButton()
    {

        switch(selectedItem.item.itemNum)
        {
            case 0:
                Debug.Log("1������ ������ ���");
                break;
            case 1:
                Debug.Log("2������ ������ ���");
                break;
            case 2:
                Debug.Log("3������ ������ ���");
                break;
            case 3:
                Debug.Log("4������ ������ ���");
                break;
            case 4:
                Debug.Log("5������ ������ ���");
                break;
            case 5:
                Debug.Log("6������ ������ ���");
                break;
            case 6:
                Debug.Log("7������ ������ ���");
                break;
            case 7:
                Debug.Log("8������ ������ ���");
                break;


        }
       
        RemoveSelectedItem(selectedItem.item.itemNum);
    }

    public void OnEquipButton()
    {

    }

    void UnEquip(int index)
    {

    }

    public void OnUnEquipButton()
    {

    }


    private void RemoveSelectedItem(int itemNum)
    {
        //slots[itemNum].item.icon = GetComponent<image>();
       // selectedItem.quantity--;                 
            selectedItem.item = null;
         //  slots[itemNum].item.icon
            ClearSeletecItemWindow();
        //�ؽ�Ʈ�� �ϳ� �־ ����̶�� �߰��ϱ�

        UpdateUI(itemNum);
    }

    public void RemoveItem(ItemData item)
    {

    }

    public bool HasItems(ItemData item, int quantity)
    {
        return false;
    }
    
    
}
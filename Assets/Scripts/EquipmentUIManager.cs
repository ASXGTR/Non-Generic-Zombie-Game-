using Game.Inventory;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIManager : MonoBehaviour
{
    [System.Serializable]
    public class SlotReference
    {
        public string slotName;
        public Button slotButton;
        public Image iconImage;
    }

    [Header("Equipment Slots")]
    public List<SlotReference> equipmentSlots = new List<SlotReference>();

    [Header("Default Icon")]
    public Sprite defaultIcon;

    private Inventory inventory;

    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("❌ EquipmentUIManager: No Inventory found in scene.");
            return;
        }

        FindAndRegisterSlotButtons();

        foreach (var gear in inventory.GetEquippedGear())
        {
            if (gear?.data == null) continue;
            Debug.Log($"🧠 EQUIPPED ON LOAD: {gear.data.itemName}, IsContainer: {gear.data.isContainer}, Capacity: {gear.data.storageCapacity}");
        }

        InitializeStorageSlots();
    }

    void FindAndRegisterSlotButtons()
    {
        foreach (SlotReference slot in equipmentSlots)
        {
            string buttonName = "Slot_" + slot.slotName;

            GameObject found = GameObject.Find(buttonName);
            if (found == null)
            {
                Debug.LogWarning("⚠️ Could not find button: " + buttonName);
                continue;
            }

            slot.slotButton = found.GetComponent<Button>();
            if (slot.slotButton != null)
            {
                string slotCopy = slot.slotName;
                slot.slotButton.onClick.AddListener(() => OnSlotClicked(slotCopy));
                Debug.Log($"🔗 Linked: {slot.slotName} slot button.");
            }

            Image img = found.GetComponentInChildren<Image>();
            if (img != null)
                slot.iconImage = img;
        }
    }

    void OnSlotClicked(string slotName)
    {
        Debug.Log($"🎯 Slot clicked: {slotName}");

        Item heldItem = inventory.rightHand ?? inventory.leftHand;
        if (heldItem == null)
        {
            Debug.Log("👐 No item in hand to equip.");
            return;
        }

        ItemData itemData = heldItem.data;

        if (!IsItemValidForSlot(itemData, slotName))
        {
            Debug.Log($"❌ {itemData.itemName} cannot be equipped in {slotName} slot.");
            return;
        }

        EquipToSlot(slotName, heldItem);
    }

    bool IsItemValidForSlot(ItemData itemData, string slotName)
    {
        slotName = slotName.ToLower();

        if (itemData.itemType == ItemType.Clothing)
            return itemData.clothingSlot.ToString().ToLower() == slotName;

        if (itemData.itemType == ItemType.Bag && slotName == "backpack")
            return true;

        if (itemData.itemType == ItemType.Holster && slotName == "holster")
            return true;

        return false;
    }

    void EquipToSlot(string slotName, Item item)
    {
        Debug.Log($"✅ Equipped {item.itemName} to {slotName}");

        inventory.EquipItem(item);

        SlotReference slotRef = equipmentSlots.Find(s => s.slotName.ToLower() == slotName.ToLower());
        if (slotRef != null && slotRef.iconImage != null)
        {
            slotRef.iconImage.sprite = item.icon != null ? item.icon : defaultIcon;
            slotRef.iconImage.enabled = true;
        }
    }

    public int GetTotalStorageCapacity()
    {
        int total = 0;
        foreach (var gear in inventory.GetEquippedGear())
        {
            if (gear?.data != null)
                total += gear.data.storageCapacity;
        }
        return total;
    }

    void InitializeStorageSlots()
    {
        foreach (var gear in inventory.GetEquippedGear())
        {
            if (gear?.data == null) continue;

            ItemData data = gear.data;

            if (data.isContainer && data.storageCapacity > 0)
            {
                Debug.Log($"📦 Initializing storage panel for: {data.itemName} ({data.storageCapacity} slots)");
                // TODO: Instantiate storage UI panels or slot containers here using your HUD prefab manager
            }
        }
    }
}

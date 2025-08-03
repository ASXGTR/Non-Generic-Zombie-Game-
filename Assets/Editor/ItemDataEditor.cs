using UnityEditor;
using UnityEngine;
using Game.Inventory;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    // Serialized Properties
    SerializedProperty idProp, itemNameProp, iconProp, tagsProp, itemTypeProp;
    SerializedProperty rarityProp, conditionProp;
    SerializedProperty clothingSlotProp, slotTypeProp, slotTypesProp;

    SerializedProperty consumableTypeProp, hungerRestoreProp, hydrationRestoreProp, sicknessChanceProp;
    SerializedProperty canBeCookedProp, cookMethodProp;
    SerializedProperty cookedVariantBoilProp, cookedVariantGrillProp, cookedVariantBakeProp, cookedVariantRoastProp;
    SerializedProperty cookingRequirementsProp;

    SerializedProperty isHotProp, hotDurationProp;
    SerializedProperty maxDurabilityProp, storageCapacityProp, containerCapacityProp, isContainerProp, weightProp;

    void OnEnable()
    {
        var s = serializedObject;
        idProp = s.FindProperty("id");
        itemNameProp = s.FindProperty("itemName");
        iconProp = s.FindProperty("iconSprite"); // changed from icon
        tagsProp = s.FindProperty("tags");
        itemTypeProp = s.FindProperty("itemType");
        rarityProp = s.FindProperty("rarity");

        conditionProp = s.FindProperty("condition");
        clothingSlotProp = s.FindProperty("clothingSlot");
        slotTypeProp = s.FindProperty("slotType");
        slotTypesProp = s.FindProperty("slotTypes");

        consumableTypeProp = s.FindProperty("consumableType");
        hungerRestoreProp = s.FindProperty("hungerRestore");
        hydrationRestoreProp = s.FindProperty("hydrationRestore");
        sicknessChanceProp = s.FindProperty("sicknessChance");
        canBeCookedProp = s.FindProperty("canBeCooked");
        cookMethodProp = s.FindProperty("cookMethod");

        cookedVariantBoilProp = s.FindProperty("cookedVariantBoil");
        cookedVariantGrillProp = s.FindProperty("cookedVariantGrill");
        cookedVariantBakeProp = s.FindProperty("cookedVariantBake");
        cookedVariantRoastProp = s.FindProperty("cookedVariantRoast");
        cookingRequirementsProp = s.FindProperty("cookingRequirements");

        isHotProp = s.FindProperty("isHot");
        hotDurationProp = s.FindProperty("hotDuration");
        maxDurabilityProp = s.FindProperty("maxDurability");
        storageCapacityProp = s.FindProperty("storageCapacity");
        containerCapacityProp = s.FindProperty("containerCapacity");
        isContainerProp = s.FindProperty("isContainer");
        weightProp = s.FindProperty("weight");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawSafe(idProp);
        DrawSafe(itemNameProp);
        DrawSafe(itemTypeProp);
        DrawSafe(rarityProp);
        DrawSafe(iconProp);
        DrawSafe(tagsProp);

        EditorGUILayout.Space();
        ItemType type = (ItemType)itemTypeProp.enumValueIndex;

        if (type == ItemType.Consumable)
        {
            EditorGUILayout.LabelField("Consumable", EditorStyles.boldLabel);
            DrawSafe(consumableTypeProp);
            DrawSafe(hungerRestoreProp);
            DrawSafe(hydrationRestoreProp);
            DrawSafe(sicknessChanceProp);
            DrawSafe(canBeCookedProp);

            if (canBeCookedProp != null && canBeCookedProp.boolValue)
            {
                DrawSafe(cookMethodProp);
                EditorGUILayout.LabelField("Cooked Variants", EditorStyles.boldLabel);
                DrawSafe(cookedVariantBoilProp);
                DrawSafe(cookedVariantGrillProp);
                DrawSafe(cookedVariantBakeProp);
                DrawSafe(cookedVariantRoastProp);
                DrawSafe(cookingRequirementsProp, true);
            }
        }
        else if (type == ItemType.Clothing)
        {
            EditorGUILayout.LabelField("Clothing", EditorStyles.boldLabel);
            DrawSafe(clothingSlotProp);
            DrawSafe(conditionProp);
            DrawSafe(slotTypeProp);
            DrawSafe(slotTypesProp, true);
        }

        EditorGUILayout.LabelField("Container & Durability", EditorStyles.boldLabel);
        DrawSafe(maxDurabilityProp);
        DrawSafe(storageCapacityProp);
        DrawSafe(containerCapacityProp);
        DrawSafe(isContainerProp);
        DrawSafe(weightProp);
        DrawSafe(isHotProp);
        DrawSafe(hotDurationProp);

        serializedObject.ApplyModifiedProperties();
    }

    void DrawSafe(SerializedProperty prop, bool includeChildren = false)
    {
        if (prop != null)
            EditorGUILayout.PropertyField(prop, includeChildren);
        else
            EditorGUILayout.HelpBox("Missing property reference", MessageType.Warning);
    }
}

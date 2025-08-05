using UnityEngine;
using Inventory.DataModels;
using Systems.Equipment;
using Tools.Events;

public class GearTransferService : MonoBehaviour
{
    public enum TransferType { Equip, Drop, Give, Store }

    public float transferCooldown = 0.5f;
    private float lastTransferTime;

    public GearEvents gearEvents;

    public bool CanTransfer(GearData gear)
    {
        return gear != null && Time.time > lastTransferTime + transferCooldown;
    }

    public bool TransferGear(GearData gear, TransferType type)
    {
        if (!CanTransfer(gear)) return false;

        GearSlotUI targetSlot = GetTransferTarget(gear, type);
        if (targetSlot == null) return false;

        targetSlot.SetGear(gear);
        lastTransferTime = Time.time;

        gearEvents?.OnTransferComplete?.Invoke(gear, type);
        return true;
    }

    private GearSlotUI GetTransferTarget(GearData gear, TransferType type)
    {
        // Example logic — replace with your own routing rules
        switch (type)
        {
            case TransferType.Equip: return GearSlotRegistry.Instance.GetSlot(gear.type);
            case TransferType.Drop: return null; // Drop to world
            case TransferType.Give: return GearSlotRegistry.Instance.GetAllySlot(gear.type);
            case TransferType.Store: return GearSlotRegistry.Instance.GetStorageSlot(gear.type);
            default: return null;
        }
    }
}

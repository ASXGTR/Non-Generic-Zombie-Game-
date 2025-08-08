using Core.Shared.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Shared.Models
{
    /// <summary>
    /// Runtime instance of an item, wrapping static ItemData with dynamic state.
    /// </summary>
    public class ItemInstance
    {
        /// <summary>Reference to the static item definition.</summary>
        public ItemData Data;

        /// <summary>How many units of this item are stacked together.</summary>
        public int Quantity = 1;

        /// <summary>Whether the item is currently equipped.</summary>
        public bool IsEquipped;

        /// <summary>Runtime internal storage for container items.</summary>
        private readonly List<ItemInstance> internalStorage = new();
        public IReadOnlyList<ItemInstance> InternalStorage => internalStorage;

        // ──────────────────────────────────────
        // 🔫 Weapon & Magazine Runtime State
        // ──────────────────────────────────────

        public ItemInstance ChamberedRound { get; private set; }
        public ItemInstance LoadedMagazine { get; private set; }

        private readonly List<ItemInstance> magazineRounds = new();
        public IReadOnlyList<ItemInstance> MagazineRounds => magazineRounds;

        // ──────────────────────────────────────
        // 🆕 Constructors
        // ──────────────────────────────────────

        public ItemInstance() { }

        public ItemInstance(ItemData data)
        {
            Data = data;
        }

        public ItemInstance(ItemData data, int quantity)
        {
            Data = data;
            Quantity = quantity;
        }

        // ──────────────────────────────────────
        // 🔎 Accessors for static data
        // ──────────────────────────────────────

        public string ItemName => Data?.ItemName ?? "Unnamed";
        public int StorageCapacity => Data?.StorageCapacity ?? 0;
        public ItemTypeEnum Type => Data?.Type ?? default;
        public bool IsContainer => Data?.isContainer ?? false;
        public bool HasStorage => IsContainer && internalStorage.Count < StorageCapacity;
        public bool IsStackable => Data?.IsStackable ?? false;
        public List<ItemSlot> SlotTypes => Data?.slotTypes ?? new();

        public bool IsWeapon => Data?.HasTag("Weapon") ?? false;
        public bool IsMagazine => Data?.HasTag("Magazine") ?? false;
        public bool IsAmmo => Data?.HasTag("Ammo") ?? false;

        // ──────────────────────────────────────
        // 📦 Container Logic
        // ──────────────────────────────────────

        public bool TryAddToStorage(ItemInstance item)
        {
            if (!HasStorage || item == null) return false;
            internalStorage.Add(item);
            return true;
        }

        public void EnsureStorageInitialized()
        {
            // Storage is always initialized via readonly field.
        }

        // ──────────────────────────────────────
        // 🔫 Weapon Logic
        // ──────────────────────────────────────

        public bool TryInsertRound(ItemInstance bullet)
        {
            if (!IsWeapon || bullet == null || !bullet.IsAmmo) return false;
            if (ChamberedRound != null) return false;

            ChamberedRound = bullet;
            return true;
        }

        public ItemInstance EjectChamberedRound()
        {
            var round = ChamberedRound;
            ChamberedRound = null;
            return round;
        }

        public bool TryInsertMagazine(ItemInstance magazine)
        {
            if (!IsWeapon || magazine == null || !magazine.IsMagazine) return false;
            LoadedMagazine = magazine;
            return true;
        }

        public ItemInstance EjectMagazine()
        {
            var mag = LoadedMagazine;
            LoadedMagazine = null;
            return mag;
        }

        public bool TryFire()
        {
            if (ChamberedRound != null)
            {
                ChamberedRound = null;
                return true;
            }

            if (LoadedMagazine != null && LoadedMagazine.TryRemoveBullet(out var round))
            {
                ChamberedRound = round;
                return TryFire(); // Fire the newly chambered round
            }

            return false;
        }

        public int GetMagazineCount()
        {
            return MagazineRounds?.Count ?? 0;
        }

        // ──────────────────────────────────────
        // 🔘 Magazine Logic
        // ──────────────────────────────────────

        public bool TryAddBullet(ItemInstance bullet)
        {
            if (!IsMagazine || bullet == null || !bullet.IsAmmo) return false;
            if (magazineRounds.Count >= Data.MaxStackSize) return false;

            magazineRounds.Add(bullet);
            return true;
        }

        public bool TryRemoveBullet(out ItemInstance bullet)
        {
            bullet = null;
            if (!IsMagazine || magazineRounds.Count == 0) return false;

            bullet = magazineRounds[0];
            magazineRounds.RemoveAt(0);
            return true;
        }

        // ──────────────────────────────────────
        // 🏷️ Tag Parsing Helpers
        // ──────────────────────────────────────

        public bool HasTag(string query) =>
            Data?.Tags != null && Data.Tags.Contains(query);

        public bool TryGetTagInt(string key, out int value)
        {
            value = 0;
            if (Data?.Tags == null) return false;

            foreach (var tag in Data.Tags)
            {
                if (tag.StartsWith($"{key}:"))
                {
                    var raw = tag.Substring(key.Length + 1);
                    return int.TryParse(raw, out value);
                }
            }
            return false;
        }

        // ──────────────────────────────────────
        // 🧠 Debug & Display
        // ──────────────────────────────────────

        public override string ToString() =>
            $"{ItemName} x{Quantity} {(IsEquipped ? "[Equipped]" : "")}";
    }
}

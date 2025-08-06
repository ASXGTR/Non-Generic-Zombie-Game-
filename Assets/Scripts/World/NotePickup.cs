using Core.Shared.Models;
// File: Assets/Scripts/World/NotePickup.cs
using UnityEngine;

namespace Game.World
{
    public class NotePickup : MonoBehaviour
    {
        [TextArea] public string noteContent;
        public string noteID;

        public void Pickup()
        {
            Debug.Log($"[NotePickup] Note '{noteID}' collected: {noteContent}");
            // Extend: Store in journal or queue for TypewriterDisplay
        }
    }
}

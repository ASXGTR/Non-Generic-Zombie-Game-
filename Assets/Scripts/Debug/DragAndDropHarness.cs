using Inventory.UI;
using UI.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Debug
{
    public class DragAndDropHarness : MonoBehaviour
    {
        public GameObject itemPrefab;
        public Transform[] dropSlots;

        private GameObject draggedItem;

        void Start()
        {
            draggedItem = Instantiate(itemPrefab, transform);
            var dragHandler = draggedItem.AddComponent<UIDragHandler>();
            dragHandler.Initialize(dropSlots);
            Debug.Log("Drag-and-drop test initialized.");
        }
    }
}



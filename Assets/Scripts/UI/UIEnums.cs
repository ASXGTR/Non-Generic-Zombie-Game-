using Core.Shared.Models;
namespace Survival.UI
{
    public enum HUDState { Default, InventoryOpen, MapView, DialogueMode }
    public enum UIPage { MainMenu, PauseMenu, Inventory, Settings, Journal }
    public enum NotificationType { Info, Warning, Error, Success }
    public enum PopupMode { Modal, Tooltip, Banner, FadeIn }
}

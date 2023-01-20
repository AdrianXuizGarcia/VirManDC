using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

namespace VMDC.UI.Events
{
    /// <summary>
    ///     An event representing a new change on the Submenu
    /// </summary>
    [Serializable]
    public class ChangeSubmenuButtonEvent : UnityEvent<Button,MenuEventType,Color> { }
    [Serializable]
    public class ChangeSubmenuEvent : UnityEvent<MenuEventType> { }
}

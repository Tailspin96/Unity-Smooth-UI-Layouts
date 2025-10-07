using System;
using UnityEngine;

namespace Tailspin96.SmoothUILayouts.Core
{
    public class ItemHolderManager : MonoBehaviour
    {
        public static event Action<ItemHolderManager> ItemLayoutChanged;

        private void OnTransformChildrenChanged()
        {
            ItemLayoutChanged?.Invoke(this);
        }
    }
}

using System;
using Tailspin96.SmoothUILayouts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Tailspin96.SmoothUILayouts.Layouts
{
    public class SmoothGridLayout : MonoBehaviour, ISmoothLayout
    {
        [Range(0, 100f)]
        [Tooltip("Set the speed that items move to their new spots.")]
        [SerializeField] private float moveSpeed = 10f;
        public float MoveSpeed => moveSpeed;

        [Space(10)]
        [SerializeField] private RectTransform itemHolder;
        [SerializeField] private RectTransform cloneHolder;

        [Space(10)]
        [SerializeField] private GridLayoutGroup grid;

        ItemHolderManager holderManager;

        private void Awake()
        {
            holderManager = itemHolder.GetComponent<ItemHolderManager>();
            ItemHolderManager.ItemLayoutChanged += OnLayoutChnaged;
        }

        private void OnDestroy()
        {
            ItemHolderManager.ItemLayoutChanged -= OnLayoutChnaged;
        }

        private void Start()
        {
            CreateLayout();
        }

        private void OnLayoutChnaged(ItemHolderManager sender)
        {
            if (sender == holderManager) CreateLayout();
        }

        private void CreateLayout()
        {

            for (int i = cloneHolder.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(cloneHolder.GetChild(i).gameObject);
            }

            foreach (RectTransform item in itemHolder)
            {
                RegisterItem(item);
            }
        }

        private void RegisterItem(RectTransform item)
        {
            item.anchorMin = item.anchorMax = new Vector2(0.5f, 0.5f);
            item.sizeDelta = grid.cellSize;

            GameObject clone = new GameObject($"{item.name} Clone", typeof(RectTransform));
            RectTransform cloneRect = clone.GetComponent<RectTransform>();
            cloneRect.sizeDelta = grid.cellSize;
            clone.transform.SetParent(cloneHolder);

            ItemFollowClone follow = item.GetComponent<ItemFollowClone>();
            if (follow == null)
                follow = item.gameObject.AddComponent<ItemFollowClone>();

            follow.cloneTransform = cloneRect;
            follow.smoothLayout = this;
        }
    }
}

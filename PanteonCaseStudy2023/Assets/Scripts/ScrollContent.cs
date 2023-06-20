using UnityEngine;

public class ScrollContent : MonoBehaviour
{
    // The RectTransform component of the scroll content.
    private RectTransform rectTransform;

    // The RectTransform components of all the children of this GameObject.
    private RectTransform[] rtChildren;

    // The width and height of the parent.
    private float height;

    // The width and height of the children GameObjects.
    private float childHeight;

    [Tooltip("How far apart each item is in the scroll view")]
    [SerializeField]
    private float itemSpacing;

    [Tooltip("How much the items are indented from the top/bottom and left/right of the scroll view")]
    [SerializeField]
    private float verticalMargin;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rtChildren = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }

        // Subtract the margin from the top and bottom.
        height = rectTransform.rect.height - (2 * verticalMargin);

        childHeight = rtChildren[0].rect.height;

        InitializeContentVertical();
    }

    /// <summary>
    /// Initializes the scroll content if the scroll view is oriented vertically.
    /// </summary>
    private void InitializeContentVertical()
    {
        float originY = 0 - (height * 0.5f);
        float posOffset = childHeight * 0.5f;
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.y = originY + posOffset + i * (childHeight + itemSpacing);
            rtChildren[i].localPosition = childPos;
        }
    }

    public float GetHeight()
    {
        return height;
    }

    public float GetChildHeight()
    {
        return childHeight;
    }

    public float GetItemSpacing()
    {
        return itemSpacing;
    }
}

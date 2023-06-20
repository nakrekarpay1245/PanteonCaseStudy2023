using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    [Tooltip("The ScrollContent component that belongs to the scroll content GameObject")]
    [SerializeField]
    private ScrollContent scrollContent;

    [Tooltip("How far the items will travel outside of the scroll view before being repositioned")]
    [SerializeField]
    private float outOfBoundsThreshold;

    // The ScrollRect component for this GameObject.
    private ScrollRect scrollRect;

    // The last position where the user has dragged.
    private Vector2 lastDragPosition;

    // Is the user dragging in the positive axis or the negative axis?
    private bool positiveDrag;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    /// <summary>
    /// Called when the user starts to drag the scroll view.
    /// </summary>
    /// <param name="eventData">The data related to the drag event.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPosition = eventData.position;
    }

    /// <summary>
    /// Called while the user is dragging the scroll view.
    /// </summary>
    /// <param name="eventData">The data related to the drag event.</param>
    public void OnDrag(PointerEventData eventData)
    {
        positiveDrag = eventData.position.y > lastDragPosition.y;

        lastDragPosition = eventData.position;
    }

    /// <summary>
    /// Called when the user starts to scroll with their mouse wheel in the scroll view.
    /// </summary>
    /// <param name="eventData">The data related to the scroll event.</param>
    public void OnScroll(PointerEventData eventData)
    {
        positiveDrag = eventData.scrollDelta.y > 0;
    }

    /// <summary>
    /// Called when the user is dragging/scrolling the scroll view.
    /// </summary>
    public void OnViewScroll()
    {
        HandleVerticalScroll();
    }

    /// <summary>
    /// Called if the scroll view is oriented vertically.
    /// </summary>
    private void HandleVerticalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag)
        {
            newPos.y = endItem.position.y - scrollContent.GetChildHeight() * 1.5f + 
                scrollContent.GetItemSpacing();
        }
        else
        {
            newPos.y = endItem.position.y + scrollContent.GetChildHeight() * 1.5f -
                scrollContent.GetItemSpacing();
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    /// <summary>
    /// Checks if an item has the reached the out of bounds threshold for the scroll view.
    /// </summary>
    /// <param name="item">The item to be checked.</param>
    /// <returns>True if the item has reached the threshold for either ends of the scroll view, false otherwise.</returns>
    private bool ReachedThreshold(Transform item)
    {
        float posYThreshold = transform.position.y + scrollContent.GetHeight() * 0.5f + 
            outOfBoundsThreshold;
        float negYThreshold = transform.position.y - scrollContent.GetHeight() * 0.5f -
            outOfBoundsThreshold;
        return positiveDrag ? item.position.y - scrollContent.GetChildHeight() * 0.5f > posYThreshold :
            item.position.y + scrollContent.GetChildHeight() * 0.5f < negYThreshold;
    }
}

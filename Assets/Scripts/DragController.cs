using UnityEngine;
using UnityEngine.EventSystems;

public class DragController : BasicSignal, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] RectTransform Card;
    [SerializeField] RectTransform CardParent;
    int StateInIerarchy;
    RectTransform m_DraggingPlane;
    [SerializeField] BasicSignal Listening;
    [SerializeField] string TurnOnSignal;
    [SerializeField] string TurnOffSignal;
    bool Switch;
    Vector3 Mouse;
    void Start()
    {
        StateInIerarchy = Card.GetSiblingIndex();
        if(Listening) Listening.Signal += SignalBox;
    }
    void SignalBox(string Message, GameObject Obj)
    {
        if(Message == TurnOnSignal) Switch = true;
        if(Message == TurnOffSignal) Switch = false;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Card.anchoredPosition = new Vector3(0, -50, 0);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(Mouse.x < 20 && Mouse.x > - 20)
        CardParent.anchoredPosition = new Vector3(0, -50, 0);
    }
    public void OnDrag(PointerEventData data)
    {
        if (data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null && Switch)
        {
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out Mouse))
            {
                CardParent.position = new Vector3(Mouse.x, CardParent.position.y, Mouse.z);
                if(Mouse.x > 20)
                {
                    if(Card.tag == "Eatable") {EmitSignal("AddScore", gameObject); Switch = false;}
                    if(Card.tag == "Uneatable") {EmitSignal("SubtractHP", gameObject); Switch = false;}
                }
                if(Mouse.x < -20)
                {
                    if(Card.tag == "Eatable") {EmitSignal("SubtractHP", gameObject); Switch = false;}
                    if(Card.tag == "Uneatable") {EmitSignal("AddScore", gameObject); Switch = false;} 
                }
                Card.SetParent(CardParent);
            }    
        }
    }
}

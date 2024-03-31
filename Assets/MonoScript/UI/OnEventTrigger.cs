using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace Chengzi
{
    public delegate void _baseEvent(BaseEventData eventData);
    public delegate void _pointEvent(PointerEventData eventData);
    public delegate void _axisEvent(AxisEventData eventData);

    public class OnEventTrigger : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
    {
        public _pointEvent _onBeginDrag;
        public _baseEvent _onCancel;
        public _baseEvent _onDeselect;
        public _pointEvent _onDrag;
        public _pointEvent _onDrop;
        public _pointEvent _onEndDrag;
        public _pointEvent _onInitializePotentialDrag;
        public _axisEvent _onMove;
        public _pointEvent _onPointerClick;
        public _pointEvent _onPointerDown;
        public _pointEvent _onPointerEnter;
        public _pointEvent _onPointerExit;
        public _pointEvent _onPointerUp;
        public _pointEvent _onScroll;
        public _baseEvent _onSelect;
        public _baseEvent _onSubmit;
        public _baseEvent _onUpdateSelected;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_onBeginDrag != null)
                _onBeginDrag(eventData);
        }

        public void OnCancel(BaseEventData eventData)
        {
            if (_onCancel != null)
                _onCancel(eventData);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (_onDeselect != null)
                _onDeselect(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_onDrag != null)
                _onDrag(eventData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (_onDrop != null)
                _onDrop(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_onEndDrag != null)
                _onEndDrag(eventData);
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (_onInitializePotentialDrag != null)
                _onInitializePotentialDrag(eventData);
        }

        public void OnMove(AxisEventData eventData)
        {
            if (_onMove != null)
                _onMove(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_onPointerClick != null)
                _onPointerClick(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_onPointerDown != null)
                _onPointerDown(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_onPointerEnter != null)
                _onPointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_onPointerExit != null)
                _onPointerExit(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_onPointerUp != null)
                _onPointerUp(eventData);
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (_onScroll != null)
                _onScroll(eventData);
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (_onSelect != null)
                _onSelect(eventData);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (_onSubmit != null)
                _onSubmit(eventData);
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            if (_onUpdateSelected != null)
                _onUpdateSelected(eventData);
        }
    }
}
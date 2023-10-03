using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollSnap : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform content;
    [SerializeField] private float snapSpeed;
    [SerializeField] private float center;

    [SerializeField] private RectTransform[] elements;
    [SerializeField] private float[] elementDistances;
    [SerializeField] public int closestElementIndex;
    [SerializeField] bool isDragging;

    private void Start()
    {
        center = scrollRect.transform.position.x;

        int count = content.childCount;

        elements = new RectTransform[count];
        elementDistances = new float[count];


        for (int i = 0; i < count; i++)
        {
            elements[i] = content.GetChild(i).GetComponent<RectTransform>();
            elements[i].localPosition = new Vector3(elements[i].localPosition.x + (i * (elements[i].rect.width + 10)), elements[i].localPosition.y);
        }
    }

    private void Update()
    {
        if (!isDragging)
        {
            FindClosestElement();
            LerpToElement(closestElementIndex);
        }
    }

    public void StartDrag()
    {
        isDragging = true;
        //Debug.Log("Drag");
    }

    public void EndDrag()
    {
        isDragging = false;
        //Debug.Log("Dont drag");
    }

    private void FindClosestElement()
    {

        for (int i = 0; i < elements.Length; i++)
        {
            float distance = Mathf.Abs(elements[i].position.x - center);
            elementDistances[i] = distance;
        }

        float minDistance = Mathf.Min(elementDistances);
        closestElementIndex = System.Array.IndexOf(elementDistances, minDistance);
    }

    private void LerpToElement(int index)
    {
        float targetPosition = -elements[index].position.x;

        content.position = new Vector2(Mathf.Lerp(content.position.x + targetPosition, 0, Time.deltaTime * snapSpeed), content.position.y);

    }
}
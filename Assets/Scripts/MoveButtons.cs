using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MoveButtons : MonoBehaviour
{
    [SerializeField] GameObject playerOneDistanceKeeper;

    [SerializeField] GameObject closeRangeIndicator;
    [SerializeField] GameObject mediumRangeIndicator;
    [SerializeField] GameObject longRangeIndicator;

    [FormerlySerializedAs("tempButton")] public GameObject currentRange;

    private void Start()
    {
        currentRange = longRangeIndicator;
    }

    private void Update()
    {
        float distance = playerOneDistanceKeeper.GetComponent<DistanceKeeper>().distance;

        if (distance >= 1.5 && distance <= 6.33)
        {
            closeRangeIndicator.GetComponent<Image>().color = new Color(0f, 1f, 0f);
        }
        else
        {
            closeRangeIndicator.GetComponent<Image>().color = new Color(0f, 0f, 0f);
        }

        if (distance >= 6.34 && distance <= 10.66)
        {
            mediumRangeIndicator.GetComponent<Image>().color = new Color(0f, 1f, 0f);
        }
        else
        {
            mediumRangeIndicator.GetComponent<Image>().color = new Color(0f, 0f, 0f);
        }

        if (distance >= 10.67 && distance <= 15)
        {
            longRangeIndicator.GetComponent<Image>().color = new Color(0f, 1f, 0f);
        }
        else
        {
            longRangeIndicator.GetComponent<Image>().color = new Color(0f, 0f, 0f);
        }
    }

    public void ChangeColor()
    {
        currentRange.GetComponent<Image>().color = new Color(0f, 1f, 0f);
    }

    public void RevertColor()
    {
        currentRange.GetComponent<Image>().color = new Color(0f, 0f, 0f);
    }
}

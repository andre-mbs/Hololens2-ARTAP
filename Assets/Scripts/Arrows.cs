using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    public GameObject head;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public GameObject upArrow;
    public GameObject downArrow;
    public bool arrowsEnabled;
    public Vector3 nextBoxPosition;

    private int frameCount;
    // Start is called before the first frame update
    void Start()
    {
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (arrowsEnabled)
        {
            Vector3 directionToTarget = (nextBoxPosition - head.transform.position).normalized;
            //Vector3 directionToTarget = (new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(head.transform.position.x, 0, head.transform.position.z)).normalized;
            float angleH = Vector3.SignedAngle(new Vector3(head.transform.forward.x, 0, head.transform.forward.z), new Vector3(directionToTarget.x, 0, directionToTarget.z), Vector3.up);

            if (angleH > -20 && angleH < 20)
            {
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);

                Vector3 axis = Vector3.Cross(head.transform.forward, new Vector3(head.transform.forward.x, head.transform.forward.y + 1, head.transform.forward.z)).normalized;
                float angleV = Vector3.SignedAngle(head.transform.forward, directionToTarget, axis);
                //Debug.Log("Vertical Angle: " + angleV);
                if (angleV < -20)
                {
                    upArrow.SetActive(false);
                    downArrow.SetActive(true);
                }
                else if (angleV > 20)
                {
                    upArrow.SetActive(true);
                    downArrow.SetActive(false);
                }
                else
                {
                    upArrow.SetActive(false);
                    downArrow.SetActive(false);
                }
            }
            else
            {
                upArrow.SetActive(false);
                downArrow.SetActive(false);

                if (angleH > 0)
                {
                    leftArrow.SetActive(false);
                    rightArrow.SetActive(true);
                }
                else
                {
                    rightArrow.SetActive(false);
                    leftArrow.SetActive(true);
                }
            }
        }
		else
		{
            rightArrow.SetActive(false);
            leftArrow.SetActive(false);
            upArrow.SetActive(false);
            downArrow.SetActive(false);
        }
    }
}

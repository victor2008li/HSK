using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Functionalities : MonoBehaviour
{

    public GameObject m_objecttorotate;

    [Space]
    public int m_minScale;
    public int m_maxScale;

    private float initialFingersDistance;

    private Vector3 initialScale;
    private float m_firstpointx, m_firstpointy;
    private float m_secondpointx, m_secondpointy;
    private float rotateZMax = 120;
    private float rotateZMin = 30;

    private float m_inc = 0;


    const float pinchTurnRatio = Mathf.PI / 2;
    const float minTurnAngle = 0;

    const float pinchRatio = 1;
    const float minPinchDistance = 0;

    const float panRatio = 1;
    const float minPanDistance = 0;

    /// <summary>
    ///   The delta of the angle between two touch points
    /// </summary>
    static public float turnAngleDelta;
    /// <summary>
    ///   The angle between two touch points
    /// </summary>
    static public float turnAngle;

    /// <summary>
    ///   The delta of the distance between two touch points that were distancing from each other
    /// </summary>
    static public float pinchDistanceDelta;
    /// <summary>
    ///   The distance between two touch points that were distancing from each other
    /// </summary>
    static public float pinchDistance;

    public Vector3 firstposition = Vector3.zero;
    public float speed = 0.25f;
    private float X;
    private float Y;

    float presstime = 0;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            presstime += Time.deltaTime;
            if (presstime >0.2f)
            {
                float s = speed * Time.deltaTime;
                //transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * s, -Input.GetAxis("Mouse X") * s, 0));
                transform.localEulerAngles += new Vector3(Input.GetAxis("Mouse Y") * s, -Input.GetAxis("Mouse X") * s, 0);
            }
        }
        else
            presstime = 0;
        if (Input.touchCount == 0)
        {
            m_inc = 0;
            m_firstpointx = 0;
            m_firstpointy = 0;
            m_secondpointx = 0;
            m_secondpointy = 0;

            return;
        }

        if (m_firstpointx == 0)
        {
            firstposition = Input.GetTouch(0).position;
            m_firstpointx = (int)Input.GetTouch(0).position.x;
            m_firstpointy = (int)Input.GetTouch(0).position.y;
        }

        m_inc += Time.deltaTime;

        if (m_inc < 0.5f)
            return;

        if (m_objecttorotate == null)
        {
            return;
        }


        if (Input.touchCount == 1)
        {
            //rotatehoz();
            return;
            m_secondpointx = (int)Input.GetTouch(0).position.x;
            m_secondpointy = (int)Input.GetTouch(0).position.y;

            float deltaX = Mathf.Abs(m_firstpointx - m_secondpointx) / Screen.width;
            float deltaY = Mathf.Abs(m_firstpointy - m_secondpointy) / Screen.height;

            deltaY = deltaY > 0.1 ? deltaY : 0;
            if (deltaX > deltaY)
            {
                if (m_firstpointx < m_secondpointx)
                {
                    _Rotating(false, 1 / deltaX);
                }
                else if (m_firstpointx > m_secondpointx)
                {
                    _Rotating(true, 1 / deltaX);
                }
            }
            else if (deltaY > deltaX)
            {
                if (m_firstpointy < m_secondpointy)
                {
                    _RotatingUp(true, 1 / deltaY);
                }
                else if (m_firstpointy > m_secondpointy)
                {
                    _RotatingUp(false, 1 / deltaY);
                }
            }

            return;
        }

        if (Input.touches.Length == 2)
        {
            m_secondpointx = (int)Input.GetTouch(0).position.x;

            float deltaX = Mathf.Abs(m_firstpointx - m_secondpointx) / Screen.width;
            _Scaling(deltaX);
            return;
        }
    }

    void rotatehoz()
    {
        Vector3 secondposition = Input.GetTouch(0).position;
        float angle = Vector3.SignedAngle(firstposition, secondposition, new Vector3(Screen.width / 2, Screen.height));
        print(angle);
        firstposition = secondposition;
    }

    void _Rotating(bool m_right, float speed)
    {
        speed = speed * 3;
        print("Rotating: " + m_right);

        if (m_right)
        {
            m_objecttorotate.transform.Rotate(transform.up * Time.deltaTime * speed * 10);
        }
        else
        {
            m_objecttorotate.transform.Rotate(-transform.up * Time.deltaTime * speed * 10);
        }
    }
    void _RotatingUp(bool m_Up, float speed)
    {
        speed = speed * 3;
        print("RotatingUP: " + m_Up);

        if (!m_Up)
        {
            m_objecttorotate.transform.Rotate(transform.right * Time.deltaTime * speed);
            transform.position += new Vector3(0, 0, Time.deltaTime * speed);
        }
        else
        {
            m_objecttorotate.transform.Rotate(-transform.right * Time.deltaTime * speed);
            transform.position -= new Vector3(0, 0, Time.deltaTime * speed);
        }
    }

    void _Scaling(float speed)
    {
        if (Input.touches.Length == 2)
        {
            Touch touch1 = Input.touches[0];
            Touch touch2 = Input.touches[1];
            // ... if at least one of them moved ...
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // ... check the delta distance between them ...
                pinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float prevDistance = Vector2.Distance(touch1.position - touch1.deltaPosition,
                                                      touch2.position - touch2.deltaPosition);
                pinchDistanceDelta = pinchDistance - prevDistance;

                // ... if it's greater than a minimum threshold, it's a pinch!
                if (Mathf.Abs(pinchDistanceDelta) > minPinchDistance)
                {
                    pinchDistanceDelta *= pinchRatio;
                    if (pinchDistanceDelta < 0)
                    {

                        m_objecttorotate.GetComponent<Camera>().fieldOfView += Time.deltaTime * 5;
                        transform.position += new Vector3(0, Time.deltaTime * 5, 0);
                        print("adding " + transform.position.y + " " + speed);
                        if (m_objecttorotate.GetComponent<Camera>().fieldOfView > rotateZMax)
                            m_objecttorotate.GetComponent<Camera>().fieldOfView = rotateZMax;
                    }
                    else
                    {
                        m_objecttorotate.GetComponent<Camera>().fieldOfView -= Time.deltaTime * 5;
                        transform.position -= new Vector3(0, Time.deltaTime * 5, 0);
                        print("desecending " + transform.position.y + " " + speed);
                        if (m_objecttorotate.GetComponent<Camera>().fieldOfView < rotateZMin)
                            m_objecttorotate.GetComponent<Camera>().fieldOfView = rotateZMin;
                    }

                }
                else
                {
                    pinchDistance = pinchDistanceDelta = 0;
                }
                /*
                // ... or check the delta angle between them ...
                turnAngle = Angle(touch1.position, touch2.position);
                float prevTurn = Angle(touch1.position - touch1.deltaPosition,
                                       touch2.position - touch2.deltaPosition);
                turnAngleDelta = Mathf.DeltaAngle(prevTurn, turnAngle);

                // ... if it's greater than a minimum threshold, it's a turn!
                if (Mathf.Abs(turnAngleDelta) > minTurnAngle)
                {
                    turnAngleDelta *= pinchTurnRatio;
                    t.text = turnAngleDelta.ToString();
                }
                else
                {
                    turnAngle = turnAngleDelta = 0;
                }*/


            }
        }

    }

    static private float Angle(Vector2 pos1, Vector2 pos2)
    {
        Vector2 from = pos2 - pos1;
        Vector2 to = new Vector2(1, 0);

        float result = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);

        if (cross.z > 0)
        {
            result = 360f - result;
        }

        return result;
    }
}
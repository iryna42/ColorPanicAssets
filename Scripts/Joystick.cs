//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

///// <summary>
///// Moves a target based on the center of the parent gameObject of the class
///// </summary>
//public class Joystick : MonoBehaviour
//{
//    public Transform target;//target to move

//    public float maxSpeed;//max speed of the target
//    public float maxSpeedDist;//distance from the center to apply max speed
//    public float maxDist;//max distance to record input

//    // Start is called before the first frame update
//    void Start()
//    {
//        Input.simulateMouseWithTouches = false;
//        Input.multiTouchEnabled = true;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Vector2 WorldPos = GameManager.instance.mainCam.WorldToScreenPoint(gameObject.transform.position);
//        Vector2 relativePos = Vector2.zero;
//        bool isMoving = false;

//        //check mouse input (testing)
//        if (Input.GetMouseButton(0))
//        {
//            relativePos = WorldPos - (Vector2)Input.mousePosition;

//            if (relativePos.magnitude < maxDist)
//            {
//                isMoving = true;
//            }
//        }

//        //check all input touches
//        for (int i = 0; i < Input.touchCount; i++)
//		{
//            Touch touch = Input.GetTouch(i);

//			if (touch.phase != TouchPhase.Ended)
//			{
//				relativePos = WorldPos - touch.position;

//				//stop checking if this touch is close enough to the joystick
//				if (relativePos.magnitude < maxDist)
//				{
//					isMoving = true;
//					break;
//				} 
//			}
//        }

//		if (isMoving)
//		{
//            Vector2 normalised = relativePos.normalized;
//            float distance = relativePos.magnitude;
//            distance = distance > maxSpeedDist ? 1 : distance / maxSpeedDist;

//            target.position += (Vector3)(-1 * distance * maxSpeed * Time.deltaTime * normalised);
//        }
//    }
//}

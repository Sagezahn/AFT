using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public Transform target;
    private float distance = 80.0f;

    // Left and right swipe movement speed
    private float xSpeed = 200.0f;
    private float ySpeed = 100.0f;
    // Scaling limit factor
    private float yMinLimit = -20;
    private float yMaxLimit = 80;
    Vector3 tmp;

    // Camera position
    private float x = 0.0f;
    private float y = 0.0f;
    // Record the position of the last phone touch to determine whether the user is doing a zoom-in or zoom-out gesture
    private Vector2 oldPosition1 = new Vector2(0, 0);
    private Vector2 oldPosition2 = new Vector2(0, 0);
    public float speed = 1;
    public float mouseSpeed = 60;
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
#if (UNITY_ANDROID || UNITY_IOS) 
        //Determine the number of touches as single point touch
        if(Input.touchCount == 1)
        {
            //Touch type is mobile touch
            if(Input.GetTouch(0).phase==TouchPhase.Moved)
            {
                //Calculate X and Y positions based on touch points
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
    
            }
        }
    
        //Determine the number of touches as multi-touch
        if(Input.touchCount >1 )
        {
            //The first two fingers touch type are mobile touch
            if(Input.GetTouch(0).phase==TouchPhase.Moved||Input.GetTouch(1).phase==TouchPhase.Moved)
            {
                    //Calculate the current position of the two touch points
                    var tempPosition1 = Input.GetTouch(0).position;
                    var tempPosition2 = Input.GetTouch(1).position;
                    //The function returns true for enlargement, false for reduction
                    if(IsEnlarge(oldPosition1,oldPosition2,tempPosition1,tempPosition2))
                    {
                        //No further amplification is allowed after the amplification factor exceeds 3
                        if(distance > 50)
                        {
                            distance -= 0.5f;
                        }
                    }else
                    {
                        //Shrink wash return to 18.5 after not allowed to continue to shrink
                        if(distance < 100)
                        {
                            distance += 0.5f;
                        }
                    }
                //Back up the position of the last touch point for comparison
                oldPosition1=tempPosition1;
                oldPosition2=tempPosition2;
                
            }
        }
#endif

#if UNITY_STANDALONE_WIN
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
#endif
    }
    bool IsEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //The function calculates the user's gesture by passing in the position of the last two touch points and the position of the current two touch points
        float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if(leng1<leng2)
        {
            //Enlarge
            return true;
        }else
        {
            return false;
        }
    }

    void LateUpdate()
        {
#if (UNITY_ANDROID || UNITY_IOS) 
            if (target)
            {
 
                //Resetting the position of the camera
                ClampAngle(y, yMinLimit, yMaxLimit);
                Quaternion rotation = Quaternion.Euler(y, x, 0);
 
                tmp.Set(0.0F,0.0F,(-1)*distance);
                Vector3 position = rotation * tmp + target.transform.position;
 
                transform.rotation = rotation;
                transform.position = position;
 
            }
        
#endif
#if UNITY_STANDALONE_WIN
        transform.Translate(new Vector3(h * speed, mouse * mouseSpeed, v * speed) * Time.deltaTime, Space.World);
#endif

        }

    
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

}
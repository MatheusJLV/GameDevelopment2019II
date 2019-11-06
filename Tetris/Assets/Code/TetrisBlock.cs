using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * To Do
 * - Move on a grid
 * - Blocks collision
 * - Line clereance
 * - Losing condition
 */
public class TetrisBlock : MonoBehaviour
{
    public float tickDeltaTime;
    public float fastTickDeltaTime;

    private Transform trans;
    private float accDeltaTime;

    void Awake()
    {
        accDeltaTime = 0f; 
    }

    // Start is called before the first frame update
    void Start()
    {
        trans = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        { // Rotation
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                trans.rotation *= Quaternion.Euler(0, 0, 90);

                //trans.Rotate(Vector3.forward, 90);
                //trans.Rotate(new Vector3(0, 0, 90), Space.Self);
            }
        }

        { // X-axis movement
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var oldPos = trans.position;
                var newPos = trans.position;
                newPos.x -= 1;
                trans.position = newPos;
                {
                    var isOut = false;
                    foreach (var childTransform in trans.GetComponentsInChildren<Transform>())
                    {
                        if (childTransform.position.x < 0
                            || childTransform.position.x > 19)
                        {
                            isOut = true;
                            break;
                        }
                    }
                    if (isOut)
                    {
                        trans.position = oldPos;
                    }
                }
                
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                var newPos = trans.position;
                newPos.x += 1;
                trans.position = newPos;
            }
        }

        { // Y-axis movement
            accDeltaTime += Time.deltaTime;

            //var actualMovementTick = tickDeltaTime;
            //if (Input.GetKey(KeyCode.LeftShift)
            //    || Input.GetKey(KeyCode.RightShift))
            //{
            //    actualMovementTick = fastTickDeltaTime;
            //    //actualMovementTick = speedFactor * tickDeltaTime;
            //}

            var fastSpeedPressed = Input.GetKey(KeyCode.DownArrow);
            var actualMovementTick = fastSpeedPressed ? fastTickDeltaTime : tickDeltaTime;

            if (accDeltaTime > actualMovementTick)
            {
                { // Move down
                    var newPos = trans.position;
                    newPos.y -= 1;
                    trans.position = newPos;
                }
                accDeltaTime = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        { // X-axis movement
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var newPos = trans.position;
                newPos.x -= 1;
                trans.position = newPos;
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

            var shiftPressed = Input.GetKey(KeyCode.LeftShift)
                               || Input.GetKey(KeyCode.RightShift);
            var actualMovementTick = shiftPressed ? fastTickDeltaTime : tickDeltaTime;

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

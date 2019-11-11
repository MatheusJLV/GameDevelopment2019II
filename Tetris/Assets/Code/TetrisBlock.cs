using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * To Do
 * - Move on a grid
 * - Blocks collision
 * - Line cleareance
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

    bool CheckConstraints()
    {
        var isOut = false;
        for (int i = 0; i < trans.childCount; i++) {
            var childTrans = trans.GetChild(i);
            if (childTrans.position.x < 0
                || childTrans.position.x > 9) {
                isOut = true;
                break;
            }
        }
        return isOut;
    }

    void ApplyConstraints(Vector3 rollbackPos, Quaternion rollbackRot)
    {
        var isOut = CheckConstraints();
        if (isOut) {
            trans.position = rollbackPos;
            trans.rotation = rollbackRot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        { // Rotation
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                var oldPos = trans.position;
                var oldRot = trans.rotation;

                trans.rotation *= Quaternion.Euler(0, 0, 90);

                ApplyConstraints(oldPos, oldRot);
                //trans.Rotate(Vector3.forward, 90);
                //trans.Rotate(new Vector3(0, 0, 90), Space.Self);
            }
        }

        { // X-axis movement
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                var oldPos = trans.position;
                var oldRot = trans.rotation;

                var newPos = trans.position;
                newPos.x -= 1;
                trans.position = newPos;

                ApplyConstraints(oldPos, oldRot);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                var oldPos = trans.position;
                var oldRot = trans.rotation;

                var newPos = trans.position;
                newPos.x += 1;
                trans.position = newPos;

                ApplyConstraints(oldPos, oldRot);
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

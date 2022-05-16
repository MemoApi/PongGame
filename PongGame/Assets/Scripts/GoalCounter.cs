using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalCounter : MonoBehaviour
{

    GameManager gameManager;
    private bool isRight;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //We check which side the object is

        if (gameObject.name== "LeftGoal")
        {
            isRight = false;
        }
        if(gameObject.name== "RightGoal")
        {
            isRight = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Score increasing function in gamemanager called
        if (collision.gameObject.CompareTag("Ball"))
        {
            Camera.main.DOShakeRotation(.2f, .3f);
            Camera.main.DOShakePosition(.2f, .2f);

            if (isRight)
            {
                gameManager.leftScored();
            }
            if (!isRight)
            {
                gameManager.rightScored();
            }
        }       
    }


}

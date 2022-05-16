using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Score Values
    private int leftScore = 0, rightScore = 0, startScore = 0;
    public Text leftScoreTxt, rightScoreTxt, winnerTxt;

    //Canvas
    public GameObject GameOverPanel;

    // Ball Direction Values
    Vector2 toRight1 = Vector2.right + Vector2.down;
    Vector2 toRight2 = Vector2.right + Vector2.up;
    Vector2 toLeft1 = Vector2.left + Vector2.down;
    Vector2 toLeft2 = Vector2.left + Vector2.up;

    // Ball Components
    GameObject ballObject;
    Rigidbody2D ballRB;
    TrailRenderer ballTrail;

    [SerializeField] private float ballSpeed;

    void Start()
    {
        
        GameOverPanel.SetActive(false);


        ballObject = GameObject.FindGameObjectWithTag("Ball");
        ballRB = ballObject.GetComponent<Rigidbody2D>();
        ballTrail = ballObject.GetComponent<TrailRenderer>();

        // We call the Throw function to the start
        StartCoroutine(ThrowTheBall(true));

    }

    public void leftScored()
    {
        //GoalCounter objects will trigger these functions every time there is a goal

        //Scores are increasing and some effect
        leftScore++;
        leftScoreTxt.transform.DOShakeScale(.5f, 1.6f);
        leftScoreTxt.text = leftScore.ToString();

        //If the score is 10, the game is over.
        if (leftScore == 10)
        {
            gameOver(true);
            return;
        }

        speedUp();

        //After each goal the ball is thrown again
        StartCoroutine(ThrowTheBall(true));
    }
    public void rightScored()
    {
        //Same operations as leftScored()
        rightScore++;
        rightScoreTxt.transform.DOShakeScale(.5f, 1.6f);
        rightScoreTxt.text = rightScore.ToString();

        if (rightScore == 10)
        {
            gameOver(false);
            return;
        }

        speedUp();

        StartCoroutine(ThrowTheBall(false));
    }



    public IEnumerator ThrowTheBall(bool toTheRight)
    {
        //The ball is thrown towards the side that conceded the goal.
        //And it does this with a random cross.

        ballTrail.emitting = false;
        yield return new WaitForSeconds(.5f);
        ballObject.transform.position = Vector2.zero;
        int random = Random.Range(0, 2);
        
        if (toTheRight)
        {                
            if (random == 0) ballRB.velocity = toRight1 * ballSpeed;            
            if (random == 1) ballRB.velocity = toRight2 * ballSpeed;            
        }
        else
        {
            if (random == 0) ballRB.velocity = toLeft1 * ballSpeed;         
            if (random == 1) ballRB.velocity = toLeft2 * ballSpeed;         
        }
        yield return new WaitForSeconds(.1f);

        ballTrail.emitting = true;

    }

    void speedUp()
    { 
        //Speed increases every two points
        if (rightScore + leftScore >= startScore + 2)
        {
            ballSpeed += 2;
            startScore = rightScore + leftScore;
        }
    }

    void gameOver(bool isPlayer1)
    {
        //The game is over and the winner's side is written
        GameOverPanel.SetActive(true);

        if (isPlayer1) winnerTxt.text = "Left Side\nWon The Game";
        else winnerTxt.text = "Right Side\nWon The Game";
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }

}

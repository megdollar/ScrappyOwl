//used to test OwlJumpScript for gameOver

using UnityEngine;

public class LogicScript : MonoBehaviour
{
    public GameObject gameOverScreen;


    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

}

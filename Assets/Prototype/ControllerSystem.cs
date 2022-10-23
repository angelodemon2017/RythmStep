using UnityEngine;

public class ControllerSystem : MonoBehaviour
{
    public PlayerEntity linkPE;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StepAndTurn(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StepAndTurn(true);
        }
    }

    public void ButtonLeft()
    {
        StepAndTurn(false);
    }

    public void ButtonRight()
    {
        StepAndTurn(true);
    }

    private void StepAndTurn(bool isRight) 
    {
        linkPE.ChangeCenter(isRight);
    }
}
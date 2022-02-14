using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] float verticalSpeed = 5, horizontalSpeed = 5;

    public int maxHeatlh;
    public static CharacterControl instance;
    public Animator animatorController;
    public bool isGameRunning = false;

    private int health;
    private SwerveInput swerveInp;
    private int collectedGems;

    void Start()
    {
        health = maxHeatlh;
        swerveInp = GetComponent<SwerveInput>();
        animatorController = GetComponent<Animator>();
        collectedGems = 0;
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (isGameRunning)
        {
            PlayerMovement();
        }
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        InGameUI.instance.UpdateHealthTxt(health);
        if (health <= 0)
        {
            LevelFailed();
        }
    }

    public void LevelPassed()
    {
        isGameRunning = false;
        GameController.instance.AddTotalGems(collectedGems);
        animatorController.SetBool("isVictory", true);
    }

    public void TakeGem()
    {
        collectedGems++;
        InGameUI.instance.UpdateScoreTxt(collectedGems);
    }


    void PlayerMovement()
    {
        float horizontalMove = horizontalSpeed * swerveInp.MoveFactorX * Time.deltaTime;

        horizontalMove = Mathf.Clamp(horizontalMove, -1, 1);

        transform.Translate(horizontalMove, 0, verticalSpeed * Time.deltaTime);
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -1.5f, 1.5f);
        transform.position = pos;
    }

    void LevelFailed()
    {
        animatorController.SetBool("isFailed", true);
        animatorController.SetBool("isRunning", false);

        /*        collectedGems = 0;
                health = maxHeatlh;*/
        isGameRunning = false;
    }




}

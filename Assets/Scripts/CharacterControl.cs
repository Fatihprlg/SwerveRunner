using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] float verticalSpeed = 5, horizontalSpeed = 5;
    [SerializeField] int maxHeatlh;

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
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if(isGameRunning)
        {
            PlayerMovement();
        }    
    }

    public void DealDamage(int damage)
    {
        health -= damage;
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
    }

    void PlayerMovement()
    {
        float horizontalMove = horizontalSpeed * swerveInp.MoveFactorX * Time.deltaTime;
        horizontalMove = Mathf.Clamp(horizontalMove, -1.5f, 1.5f);
        transform.Translate(horizontalMove, 0, verticalSpeed * Time.deltaTime);
    }

    void LevelFailed()
    {
        animatorController.SetBool("isFailed", true);
/*        collectedGems = 0;
        health = maxHeatlh;*/
        isGameRunning = false;
    }




}

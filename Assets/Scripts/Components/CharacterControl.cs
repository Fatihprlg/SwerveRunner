using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    [SerializeField] private float verticalSpeed = 5, horizontalSpeed = 2;
    [SerializeField] private Renderer charRenderer;
    private static CharacterControl _instance;
    private SwerveInput swerveInp;
    private int health;
    private bool damagable = true;
    private int maxHeatlh;

    [HideInInspector] public int collectedGems;
    public static CharacterControl Instance { get { return _instance; } }
    [HideInInspector] public Animator animatorController;
    [HideInInspector] public bool isGameRunning = false;

    void Start()
    {
        maxHeatlh = PlayerPrefs.GetInt("MaxHealth", 3);
        health = maxHeatlh;
        swerveInp = GetComponent<SwerveInput>();
        animatorController = GetComponent<Animator>();
        collectedGems = 0;
        

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        if (isGameRunning)
        {
            PlayerMovement();
        }
    }

    public void Refresh()
    {
        maxHeatlh = PlayerPrefs.GetInt("MaxHealth", 3);
        health = maxHeatlh;
    }

    public void DealDamage(int damage)
    {
        if (damagable)
        {
            health -= damage;
            InGameUI.Instance.UpdateHealthTxt(health);
            if (health <= 0)
            {
                GameController.Instance.LevelFailed();
            }
            else StartCoroutine(DamageEffect());
        }
        
    }

    public void TakeGem()
    {
        collectedGems += GameController.Instance.gemMultiplier;
        InGameUI.Instance.UpdateScoreTxt(collectedGems);
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

    IEnumerator DamageEffect()
    {
        damagable = false;
        for(int i = 0; i <4; i++)
        {
            charRenderer.enabled = false;
            yield return new WaitForSeconds(0.15f);
            charRenderer.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }
        damagable = true;
    }



}

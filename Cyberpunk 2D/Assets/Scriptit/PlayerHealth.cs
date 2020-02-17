using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]

    public float fullHealth; //maxHealth
    public float currentHealth;    //previoushealth
    public float health;
    public float counter;
    public float maxCounter;
    public Image filler;
    public Animator anim;

    public string endText = "TESTI!";
    public Image deathScreen;
    public CanvasGroup endCG;
    public Text endGameUIText;
    public int fallBoundary = 1;

    private void Start()
    {
        GetComponent<Animator>();
       GameStatus.status.currentHealth = GameStatus.status.fullHealth;
    }

    private void Update()
    {
        if (transform.position.y <= -fallBoundary)
        {
            //addDamage(100);
            EndGame();
        }

        if (counter>maxCounter)
        {
            GameStatus.status.currentHealth = GameStatus.status.health;
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }


        filler.fillAmount = Mathf.Lerp(GameStatus.status.currentHealth / GameStatus.status.fullHealth, GameStatus.status.health / GameStatus.status.fullHealth, counter / maxCounter);
    }

    public void MakeDead()

    {

        endText = "GAMEOVER";
        anim.SetTrigger("Die");
        //SceneManager.LoadScene("DeathScene");

        //EndGame();


    }
    
    public void EndGame()
    {
        deathScreen.color = Color.red;
        endGameUIText.text = endText;
        endCG.alpha = 1;
        print(endText);
    }

    public void addDamage(float damage)
    {
        Debug.Log("otetaan vahinkoa: " + damage);
        GameStatus.status.currentHealth = filler.fillAmount * GameStatus.status.fullHealth;
        counter = 0;
        if (damage <= 0)
            return;
        GameStatus.status.health -= damage;
        if (filler.fillAmount<=0)
        {
          MakeDead();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("osuttiin ansaan: " + collision.gameObject.name);

            addDamage(20);
        }
        if (collision.gameObject.CompareTag("Bombalicious"))
        {
            Debug.Log("osuttiin viholliseen: "+  collision.gameObject.name);
            addDamage(20);
            anim.SetBool("Armed", false);
            GetComponent<Weapon>().hasWeapon = false;

        }

        if (collision.gameObject.CompareTag("AddHealth"))
        {
            GetHealth(collision.GetComponent<AddHealth>().healthAmount);
            Destroy(collision.gameObject);

        }
        if(collision.gameObject.CompareTag("AddHealthBonus"))
        {
            GetHealth(collision.GetComponent<AddHealthBonus>().healthAmount);
            Destroy(collision.gameObject);
        }

    }

    void GetHealth(float HealthAmount)
    {
        GameStatus.status.health += HealthAmount;
        if(GameStatus.status.health > GameStatus.status.fullHealth)
        {
            GameStatus.status.health = GameStatus.status.fullHealth;
        }
    }
}

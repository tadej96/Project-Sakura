using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            //iframes
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                if(GetComponent<PlayerMovement>() != null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                    //SceneManager.LoadScene(GameOverPanel);
                    //gameOverPanel.SetActive(true);
                }
                    

                if(GetComponent<EnemyPatrol>() != null)
                    GetComponent<EnemyPatrol>().enabled = false;
                
                if(GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;

                if(GetComponent<playerCombat>() != null)
                    GetComponent<playerCombat>().enabled = false;

                dead = true;

                
                
            }
        }
    }

}

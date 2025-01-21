using UnityEngine;

public class Firewall : MonoBehaviour
{
    [SerializeField] GameObject firawallPreFab;
    [SerializeField] private float defendTime = 100;

    private Health playerHealthComponent;

    private void Awake()
    {
        playerHealthComponent = GetComponent<Health>(); 
    }


    private void OnEnable()
    {
        if (playerHealthComponent)
        {
            StopAllCoroutines();
            StartCoroutine(DefenderTimer());
        }
    }


    private System.Collections.IEnumerator DefenderTimer()
    {
        playerHealthComponent.Invincible = true;
        firawallPreFab.SetActive(true);
        yield return new WaitForSeconds(defendTime);
        firawallPreFab.SetActive(false);
        playerHealthComponent.Invincible = false;
    }

}

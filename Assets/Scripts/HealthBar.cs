using UnityEngine;

public class HealthBar : Health
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private UnityEngine.UI.Image currentHealthBar;
    private int numberOfHeart = 3;

    // Start is called before the first frame update
    void Start()
    {
        currentHealthBar.fillAmount = playerHealth.CurrentHealth / (float) numberOfHeart;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthBar.fillAmount = playerHealth.CurrentHealth / (float) numberOfHeart;
    }
}

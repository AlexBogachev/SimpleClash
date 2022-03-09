using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Soldier soldier;
    Vector2 offset;

    HealthBarController controller;
    float maxHealth;

    public void Initialize(Soldier soldier)
    {
        this.soldier = soldier;
        offset = (Vector2)transform.position - soldier.GetPosition();
        GameObject newParent = GameObject.Find("HealthBars");
        transform.parent = newParent.transform;

        controller = GetComponent<HealthBarController>();
        maxHealth = soldier.GetCharacteristic().health;

        soldier.soldierKilled.AddListener(delegate { Destroy(gameObject); });
    }

    private void Update()
    {
        transform.position = soldier.GetPosition() + offset;
    }

    public void UpdateBar(float health)
    {
        float ratio = health / maxHealth;
        controller.BarProgress = ratio;
    }
}

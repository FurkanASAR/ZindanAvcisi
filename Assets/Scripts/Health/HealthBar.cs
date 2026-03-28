using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private IHasHealth characterHealth;

    
    private HealthSystem heatlh;
    private void Start()
    {
        characterHealth = GetComponent<IHasHealth>();
        heatlh = characterHealth.CharacterHealth;
        heatlh.OnHealthChanged += Heatlh_OnHealthChanged;
    }

    private void Heatlh_OnHealthChanged(object sender, System.EventArgs e)
    {
        slider.value = heatlh.GetHealth();
        Debug.Log("HelathBar slider valie " + slider.value);
        Debug.Log("HealthBar healt.GetHealth " + heatlh.GetHealth());
    }
}

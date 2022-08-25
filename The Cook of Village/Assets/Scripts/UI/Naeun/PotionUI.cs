using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionUI : MonoBehaviour, IObserver<Potion>
{
    [SerializeField] private GameObject RedPotion;
    [SerializeField] private Text RedPotionText;
    [SerializeField] private Image RedPotionTime;
    [SerializeField] private GameObject OrangePotion;
    [SerializeField] private Text OrangePotionText;
    [SerializeField] private Image OrangePotionTime;
    [SerializeField] private GameObject GreenPotion;
    [SerializeField] private GameObject BrownPotion;
    

    private void RedTime(float RedDuration, float CurrentRedTime)
    {
        RedPotionText.text = ((int)CurrentRedTime).ToString();
        RedPotionTime.fillAmount = 1 - CurrentRedTime / RedDuration;
    }
    private void OrangeTime(float OrangeDuration, float CurrentOrangeTime)
    {
        OrangePotionText.text = ((int)CurrentOrangeTime).ToString();
        OrangePotionTime.fillAmount = 1 - CurrentOrangeTime / OrangeDuration;
    }
    #region observer
    private void Start()
    {
        AddObserver(Potion.Instance);
    }
    private void OnDisable()
    {
        RemoveObserver(Potion.Instance);
    }

    public void AddObserver(IPotionOb o)
    {
        o.AddObserver(this);
    }

    public void RemoveObserver(IPotionOb o)
    {
        o.RemoveObserver(this);
    }

    public void Change(Potion obj)
    {
        if (obj is Potion)
        {
            var potion = obj;

            RedTime(potion.RedDuration, potion.RedTime);
            OrangeTime(potion.OrangeDuration, potion.OrangeTime);
            RedPotion.SetActive(potion.Red);
            OrangePotion.SetActive(potion.Orange);
            GreenPotion.SetActive(potion.Green);
            BrownPotion.SetActive(potion.Brown);
        }
    }
    #endregion
}

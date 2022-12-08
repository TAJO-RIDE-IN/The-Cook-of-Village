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
    private Potion potion;

    private void PotionTime(float Duration, float CurrentTime, Text text, Image image)
    {
        text.text = ((int)CurrentTime).ToString();
        image.fillAmount = 1 - CurrentTime / Duration;

    }
    #region observer
    private void Start()
    {
        potion = Potion.Instance;
        AddObserver(potion);
        PotionChange();
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
        if(o != null)  { o.RemoveObserver(this); }
    }

    private void PotionChange()
    {
        PotionTime(potion.RedDuration, potion.RedTime, RedPotionText, RedPotionTime);
        PotionTime(potion.OrangeDuration, potion.OrangeTime, OrangePotionText, OrangePotionTime);
        RedPotion.SetActive(Potion.Red);
        OrangePotion.SetActive(Potion.Orange);
        GreenPotion.SetActive(Potion.Green);
        BrownPotion.SetActive(Potion.Brown);
    }
    public void Change(Potion obj)
    {
        if (obj is Potion)
        {
            var potion = obj;
            PotionChange();
        }
    }
    #endregion
}

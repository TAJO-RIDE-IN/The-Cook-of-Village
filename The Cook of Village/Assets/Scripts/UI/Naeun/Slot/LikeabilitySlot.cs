using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LikeabilitySlot : Slot<NPCInfos>
{
    public Image FavoriteFoodImage;
    public Text CountText;
    public Slider LikeabilitySlider;
    public Color Eat;
    public Color NotEat;
    private void Start()
    {
        Infos = NPCData.Instance.npcInfos[(int)Infos.work];
        ModifySlot();
    }
    private void OnEnable()
    {
        ModifySlot();
    }
    public override void ModifySlot()
    {
        int likeability = Infos.Likeability;
        CountText.text = (likeability/100).ToString();
        LikeabilitySlider.value = likeability;
        FavoriteFoodImage.color = (Infos.EatFavriteFood)? Eat : NotEat;
    }

    public override void SelectSlot()
    {

    }
}

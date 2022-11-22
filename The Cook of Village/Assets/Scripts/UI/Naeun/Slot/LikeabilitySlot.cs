using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LikeabilitySlot : Slot<NPCInfos>
{
    public Image[] PointImage;
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
        foreach (var image in PointImage.Select((value, index) => (value, index)))
        {
            image.value.color = (image.index <= likeability) ? new Color(1, 1, 0) : new Color(1, 1, 1);
        }
    }

    public override void SelectSlot()
    {

    }
}

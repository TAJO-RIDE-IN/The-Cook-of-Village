using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LikeabilitySlot : Slot<NPCInfos>
{
    public Image[] PointImage;
    public Text CountText;
    public Slider LikeabilitySlider;
    private void Start()
    {
        Infos = NPCData.Instance.npcInfos[(int)Infos.work];
    }
    private void OnEnable()
    {
        ModifySlot();
    }
    public override void ModifySlot()
    {
        int likeability = Infos.likeability;
        CountText.text = likeability.ToString();
        LikeabilitySlider.value = likeability;
        foreach(var image in PointImage.Select((value, index) => (value, index)))
        {
            image.value.color = (image.index <= likeability) ? new Color(1, 1, 0) : new Color(1, 1, 1);
        }
    }

    public override void SelectSlot()
    {

    }
}

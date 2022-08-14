using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ShopCount
{
    static int ShopLemonCount;
    static int ShopOrangeCount;
    static int ShopWatermelonCount;
    static int ShopTomatoCount;
    static int ShopPotatoCount;
    static int ShopEggCount;
    static int ShopChickenCount;
    static int ShopBeefCount;
    static int ShopBaconCount;
    static int ShopChocolateCount;
    static int ShopRedPotionCount;
    static int ShopOrangePotionCount;
    static int ShopGreenPotionCount;
    static int ShopBrownPotionCount;
    static int ShopRainbowPotionCount;
    static int ShopBlenderCount;
    static int ShopPotCount;
    static int ShopFrypanCount;
    static int ShopWhippingMachineCount;
    static int ShopOvenCount;

    public static List<int> ShopCountList = new List<int> 
    { 
        ShopLemonCount, ShopOrangeCount, ShopWatermelonCount,
        ShopTomatoCount, ShopPotatoCount,
        ShopEggCount, ShopChickenCount, ShopBeefCount, ShopBaconCount,
        ShopChocolateCount,
        ShopRedPotionCount, ShopOrangePotionCount, ShopGreenPotionCount, ShopBrownPotionCount, ShopRainbowPotionCount,
        ShopBlenderCount, ShopPotCount, ShopFrypanCount, ShopWhippingMachineCount, ShopOvenCount
    };

    public static Dictionary<string, int> ShopCountDictionary = new Dictionary<string, int>()
    {
        {"Lemon", ShopLemonCount }, {"Orange", ShopOrangeCount }, {"Watermelon", ShopWatermelonCount },
        {"Tomato", ShopTomatoCount }, {"Potato", ShopPotatoCount },
        {"Egg", ShopEggCount }, {"Chicken", ShopChickenCount }, {"Beef", ShopBeefCount }, {"Bacon", ShopBaconCount },
        {"Chocolate", ShopChocolateCount },
        {"RedPotion", ShopRedPotionCount }, {"OrangePotion", ShopOrangePotionCount }, {"GreenPotion", ShopGreenPotionCount }, {"BrownPotion", ShopBrownPotionCount }, {"RainbowPotion", ShopRainbowPotionCount },
        {"Blender", ShopBlenderCount }, {"Pot", ShopPotCount }, {"Frypan", ShopFrypanCount }, {"WhippingMachine", ShopWhippingMachineCount }, {"Oven", ShopOvenCount }
    };
    public static void ResetShopCount()
    {
        ShopCountDictionary.Keys.ToList().ForEach(key => ShopCountDictionary[key] = 0);
    }
}

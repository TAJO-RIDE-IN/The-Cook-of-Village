using UnityEngine;
 using UnityEngine.UI;

 

 [RequireComponent(typeof(Button))]

 public class ImgChange : MonoBehaviour

 {

     public Sprite newsprite;

     public bool condition;

 

     private Button button;

 

     void Start ()

     {

         button = GetComponent<Button>();

     }

 

     void Update ()

     {

 

         if (condition)

         {

             button.image.sprite = newsprite;

         }

         else

         {

             button.image.overrideSprite = null;

         }

     }

 }
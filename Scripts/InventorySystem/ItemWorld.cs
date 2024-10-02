// Source Code ini untuk Mengahandle Item di World

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
   public static ItemWorld MunculkanItem(Vector3 position, Item item)
   {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
   }
   private Item item;
   private SpriteRenderer spriteRenderer;
   private TextMeshPro jumlah;
   private void Awake() 
   {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
   }
   public void SetItem(Item item)
   {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
     //    if (item.jumlah > 1)
     //    {
     //      jumlah.SetText(item.jumlah.ToString());
     //    } else
     //    {
     //      jumlah.SetText("");
     //    }
   }

   public Item GetItem()
   {
          return item;     
   }

   public void DestroySelf()
   {
        Destroy(gameObject);
   }
}

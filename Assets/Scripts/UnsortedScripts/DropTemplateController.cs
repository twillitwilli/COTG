using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTemplateController : MonoBehaviour
{
    public ItemDropSelection dropSelection;

    public void UseSpecificItemDrop(ItemDropSelection.ItemType itemType)
    {
        dropSelection.spawnSpecificItem = true;
        dropSelection.itemType = itemType;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTemplateController : MonoBehaviour
{
    public ItemDropSelection dropSelection;

    public void UseSpecificItemDrop(ItemPoolManager.DroppableItem itemType)
    {
        dropSelection.spawnSpecificItem = true;
        dropSelection.droppableItem = itemType;
    }
}

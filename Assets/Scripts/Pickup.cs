using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] public ItemData itemData;  //what kind of item it is
    [SerializeField] public int ammoAmount = 1; //how much to add
}
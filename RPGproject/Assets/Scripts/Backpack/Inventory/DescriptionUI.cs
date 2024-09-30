using UnityEngine;
using UnityEngine.UI;

public class DescriptionUI : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Text descText;

    public void setInfo(ItemBase item)
    {
        image.sprite = item.Icon;
        descText.text = item.Description;


    }
}

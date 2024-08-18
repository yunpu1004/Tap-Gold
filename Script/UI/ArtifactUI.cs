using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 아티팩트 UI를 관리하는 클래스입니다.
public class ArtifactUI : MonoBehaviour
{
    public TextMeshProUGUI prestigePointText;
    public GameObject artifactSlotsParent;
    public Button prestigeOpenButton;
    public (GameObject slot, Image image, TextMeshProUGUI name, TextMeshProUGUI desc)[] artifactSlots;


    // 아티팩트 슬롯의 텍스트와 이미지를 설정합니다.
    public void SetArtifactSlot(int index, Sprite sprite, string name, string desc)
    {
        artifactSlots[index].image.sprite = sprite;
        artifactSlots[index].name.text = name;
        artifactSlots[index].desc.text = desc;
    }

    // 아티팩트 슬롯을 활성화/비활성화합니다.
    public void SetArtifactSlotActive(int index, bool value)
    {
        artifactSlots[index].slot.SetActive(value);
    }

    // 프레스티지 포인트 텍스트를 설정합니다.
    public void SetPrestigePointText(int prestigePoint)
    {
        prestigePointText.text = $"<sprite=0> {prestigePoint}";
    }
}
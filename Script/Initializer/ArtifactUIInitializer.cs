using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ArtifactUI 컴포넌트를 초기화하는 클래스입니다
public class ArtifactUIInitializer : MonoBehaviour
{
    private ArtifactUI artifactUI;

    // ArtifactUI의 필드를 초기화하고 이벤트 핸들러를 등록합니다
    private void Awake()
    {
        artifactUI = FindObjectOfType<ArtifactUI>(true);

        // 이벤트 핸들러 등록
        GameController.instance.OnPlayerDataChangedEvent += OnPlayerDataChanged;
        GameController.instance.OnPlayerDataLoadedEvent += OnPlayerDataLoaded;

        // 아티팩트 슬롯 오브젝트를 찾아서 배열에 저장
        artifactUI.artifactSlots = new (GameObject slot, Image image, TextMeshProUGUI name, TextMeshProUGUI desc)[artifactUI.artifactSlotsParent.transform.childCount];
        for (int i = 0; i < artifactUI.artifactSlotsParent.transform.childCount; i++)
        {
            var slot = artifactUI.artifactSlotsParent.transform.GetChild(i).gameObject;
            var image = slot.transform.Find("Image").GetComponent<Image>();
            var name = slot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var desc = slot.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            artifactUI.artifactSlots[i] = (slot, image, name, desc);
        }
    }

    // 플레이어 데이터가 로드될 때, 프레스티지 포인트와 아티팩트 정보를 UI에 반영합니다
    private void OnPlayerDataLoaded(PlayerData data)
    {
        // 보유한 프레스티지 포인트를 UI에 반영함
        artifactUI.SetPrestigePointText(data.status.prestigePoint);

        // 모든 아티팩트의 정보를 각 슬롯에 반영함
        // 보유한 아티팩트가 있을 경우 해당 슬롯을 활성화함
        var artifactAcquired = data.status.artifactAcquired;
        for (int i = 0; i < artifactAcquired.Length; i++)
        {
            var artifactData = ArtifactData.GetArtifactData(i);
            artifactUI.SetArtifactSlot(i, artifactData.sprite, artifactData.name, artifactData.description);
            artifactUI.SetArtifactSlotActive(i, artifactAcquired[i]);
        }
    }

    // 프레스티지 포인트, 보유 아티팩트 목록이 변경될 때마다 UI를 업데이트합니다
    // 이 메소드는 GameController의 이벤트에 등록됩니다 
    private void OnPlayerDataChanged(PlayerData prev, PlayerData cur)
    {
        // 프레스티지 포인트가 변경되었을 때 UI를 업데이트함
        if (prev.status.prestigePoint != cur.status.prestigePoint)
        {
            artifactUI.SetPrestigePointText(cur.status.prestigePoint);
        }

        // 아티팩트 획득 여부가 변경되었을 때 UI를 업데이트함
        for (int i = 0; i < cur.status.artifactAcquired.Length; i++)
        {
            if (prev.status.artifactAcquired[i] != cur.status.artifactAcquired[i])
            {
                artifactUI.SetArtifactSlotActive(i, cur.status.artifactAcquired[i]);
            }
        }
    }
}
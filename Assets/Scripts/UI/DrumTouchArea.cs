using UnityEngine;
using UnityEngine.EventSystems;

public class DrumTouchArea : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    public DrumInputType drumInputType = DrumInputType.Unknown;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (drumInputType != DrumInputType.Unknown)
        {
            // エンキューされた入力イベントはInputManager側で処理される
            // 独立して呼ばれるためマルチタッチでも同フレーム内の順番・タイミングを保持
            InputManager.Instance.EnqueueDrumInputEvent(
                InputManager.DrumInputEvent.New(0, 0, drumInputType)
            );
        }
    }
}

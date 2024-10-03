using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChronicleDimensionProject.UI
{
    public class RootFigmaView : FigmaView
    {
        [SerializeField, AutoAssignByName("ButtonA")]
        Button _buttonA;

        [SerializeField, AutoAssignByName("ButtonA/Label")]
        TextMeshProUGUI _buttonALabel;

        [SerializeField, AutoAssignByName("ButtonB")]
        Button _buttonB;

        [SerializeField, AutoAssignByName("ButtonB/Label")]
        TextMeshProUGUI _buttonBLabel;
    
        [SerializeField, AutoAssignByName("ImageA")]
        Image _image;
    }
}
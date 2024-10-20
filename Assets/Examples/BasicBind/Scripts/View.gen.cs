using UnityEngine;
using System.Linq;
using System;
using ViewGenerator;
using UnityEngine.UI;

namespace Example.Example1
{
    public partial class View : MonoGenView
    {
        private Image _imgBackground;
        private Button _btnConfirm;
        private Text _btnConfirmText;
        private Button _btnConfirm2;
        private Text _btnConfirmText2;

        public Image ImgBackground => _imgBackground;
        public Button BtnConfirm => _btnConfirm;
        public Text BtnConfirmText => _btnConfirmText;
        public Button BtnConfirm2 => _btnConfirm2;
        public Text BtnConfirmText2 => _btnConfirmText2;

        protected override void InitializeComponent()
        {
            var markers = GetComponentsInChildren<IGenMarker>().Where(x => x.IsInclude).ToList();

            _imgBackground = markers.Find(x => x.Name == "imgBackground").GetNativeObject() as Image;
            _btnConfirm = markers.Find(x => x.Name == "btnConfirm").GetNativeObject() as Button;
            _btnConfirmText = markers.Find(x => x.Name == "btnConfirmText").GetNativeObject() as Text;
            _btnConfirm2 = markers.Find(x => x.Name == "btnConfirm2").GetNativeObject() as Button;
            _btnConfirmText2 = markers.Find(x => x.Name == "btnConfirmText2").GetNativeObject() as Text;

            _btnConfirm.onClick.AddListener( () => btnConfirm_Clicked( _btnConfirm, EventArgs.Empty));
            _btnConfirm2.onClick.AddListener( () => btnConfirm_Clicked( _btnConfirm2, EventArgs.Empty));
        }
    }
}

using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

namespace Example.Example1
{
    public partial class View : MonoGenView
    {
        private Image _imgBackground;
        private Button _btnConfirm;

        public Image ImgBackground => _imgBackground;
        public Button BtnConfirm => _btnConfirm;

        protected override void InitializeComponent()
        {
            var markers = GetComponentsInChildren<IGenMarker>().Where(x => x.IsInclude).ToList();

            _imgBackground = markers.Find(x => x.Name == "imgBackground").GetNativeObject() as Image;
            _btnConfirm = markers.Find(x => x.Name == "btnConfirm").GetNativeObject() as Button;

            _btnConfirm.onClick.AddListener( () => btnConfirm_Clicked( _btnConfirm, EventArgs.Empty));
        }
    }
}

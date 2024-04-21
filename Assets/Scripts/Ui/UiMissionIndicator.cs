using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Dragoraptor.Ui
{
    public class UiMissionIndicator : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] private Image _mainImage;
        [SerializeField] private Image _selectMarker;
        [SerializeField] private int _levelID = -1;
        
        
        public event Action<int> OnClick; 
        
        private readonly Color _notAvialableColor = Color.grey;
        private readonly Color _avialableColor = Color.yellow;
        private readonly Color _finishedColor = Color.green;

        private LevelStatus _status;

        private bool _isSelected;


        public int LevelID => _levelID;


        public LevelStatus Status
        {
            get => _status;
            set
            {
                switch (value)
                {
                    case LevelStatus.Hidden:
                        _mainImage.enabled = false;
                        _selectMarker.enabled = false;
                        break;
                    case LevelStatus.NotAvailable:
                        _mainImage.enabled = true;
                        _mainImage.color = _notAvialableColor;
                        break;
                    case LevelStatus.Available:
                        _mainImage.enabled = true;
                        _mainImage.color = _avialableColor;
                        break;
                    case LevelStatus.Finished:
                        _mainImage.enabled = true;
                        _mainImage.color = _finishedColor;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }

                _status = value;
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                if (_isSelected)
                {
                    if (_status != LevelStatus.Hidden)
                    {
                        _selectMarker.enabled = true;
                    }
                }
                else
                {
                    _selectMarker.enabled = false;
                }
                
            }
        }
        
        
        #region IPointerClickHandler
        
        public void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log($"UiMissionIndicator->OnPointerClick: _levelID = {_levelID}");
            OnClick?.Invoke(_levelID);
        }
        
        #endregion
        
    }
}
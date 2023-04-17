using UnityEngine;
using UnityEngine.UI;

namespace YMSoft.Core.UI
{
    public class UIBottomPanel : MonoBehaviour
    {
        public enum MenuType : int
        {
            None = 0,
            Build, Edit
        }

        [SerializeField] private GameObject _buildMenu;
        [SerializeField] private GameObject _editMenu;
        private GameObject _currentMenu = null;
        private MenuType _menuType = MenuType.None;

        public GameObject CurrentMenu
        {
            get => _currentMenu;
            set
            {
                if (value == null)
                {
                    if (_currentMenu != null)
                        _currentMenu.SetActive(false);

                    _currentMenu = null;
                }
                else
                {
                    if (value == _currentMenu)
                        CurrentMenu = null;
                    else
                    {
                        if (_currentMenu != null)
                            _currentMenu.SetActive(false);

                        _currentMenu = value;
                        _currentMenu.SetActive(true);
                    }
                }
            }
        }

        public void ShowMenu(MenuType type)
        {
            if (type == _menuType) return;

            CurrentMenu = type switch
            {
                MenuType.Build => _buildMenu,
                MenuType.Edit => _editMenu,
                _ => null
            };

            _menuType = CurrentMenu == null ? MenuType.None : type;
        }

    }
}
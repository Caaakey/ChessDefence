using UnityEngine;
using YMSoft.Core.UI;

namespace YMSoft.Core.Managers
{
    public class TileController : DynamicMonoInstance<TileController>
    {
        private const int TILE_LAYER = 1 << 12;

        [SerializeField] private UIBottomPanel _menuPanel;

        public BaseTile SelectTile { get; private set; } = null;

        public void UnSelect()
        {
            _menuPanel.ShowMenu(UIBottomPanel.MenuType.None);

            SelectTile = null;
        }

        public void ShowMenu(UIBottomPanel.MenuType type)
        {
            if (SelectTile == null)
                UnSelect();

            _menuPanel.ShowMenu(type);
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (_menuPanel.CurrentMenu != null &&
                    RectTransformUtility.RectangleContainsScreenPoint(
                        (RectTransform)_menuPanel.transform, Input.mousePosition, MainCamera.Instance.GetCamera))
            {
                return;
            }

            if (FindTiles() is Collider collider)
            {
                BaseTile tile = collider.GetComponent<BaseTile>();
                if (tile == SelectTile)
                    UnSelect();
                else
                {
                    UIBottomPanel.MenuType type = tile.Child != null ?
                        UIBottomPanel.MenuType.Edit : UIBottomPanel.MenuType.Build;

                    SelectTile = tile;
                    _menuPanel.ShowMenu(type);
                }
            }
            else
                UnSelect();
        }


        private Collider FindTiles()
        {
            Ray ray = MainCamera.Instance.GetCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 20f, TILE_LAYER))
                return hit.collider;

            return null;
        }
    }
}
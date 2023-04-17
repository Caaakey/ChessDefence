using UnityEngine;
using UnityEngine.UI;

using YMSoft.Core.Pawns;
using YMSoft.Core.Managers;

namespace YMSoft.Core.UI
{
    public class UIBaseBuildFrame : MonoBehaviour
    {
        private void Awake()
        {
            Button b = GetComponent<Button>();
            b.onClick.AddListener(OnCreatePrefab);
        }

        [SerializeField] private BasePawn _prefab;
        
        private void OnCreatePrefab()
        {
            BaseTile tile = TileController.Instance.SelectTile;
            if (tile == null) return;

            tile.CreateChild(_prefab);

            TileController.Instance.ShowMenu(UIBottomPanel.MenuType.Edit);
        }
    }

}
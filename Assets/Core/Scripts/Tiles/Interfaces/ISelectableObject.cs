
namespace YMSoft.Core
{
    public interface ISelectableObject
    {
        public bool IsSelected { get; }

        public void OnStartSelect();
        public void OnSelected();
        public void OnEndSelect();
    }
}

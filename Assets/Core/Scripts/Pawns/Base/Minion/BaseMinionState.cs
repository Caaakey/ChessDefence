
namespace YMSoft.Core.Pawns
{
    public class BaseMinionState : BasePawnState
    {
        public override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
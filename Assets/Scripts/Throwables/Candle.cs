using UnityEngine;

namespace Throwables
{
    public class Candle : Throwable
    {
        protected override void Interact()
        {
            StageManager.Instance.StageClear();
        }
    }
}


using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 盘子状态
    /// </summary>
    public enum PlateState
    {
        NORMAL,
        OUT_SCREEN
    }

    /// <summary>
    /// 盘子
    /// </summary>
    public class Plate : MonoBehaviour
    {
        public PlateState _state = PlateState.NORMAL;
        private float _exitDropAreaX = -ItemBase.SCREEN_WIDTH / 2 - 200;

        public void fixedUpdate(float dt)
        {
            if (_state == PlateState.NORMAL)
            {
                //可下落状态
                float nextPosX = transform.localPosition.x - ItemBase._moveSpeed;
                transform.localPosition = new Vector2(nextPosX, transform.localPosition.y);
                if (transform.localPosition.x < _exitDropAreaX)
                {
                    _state = PlateState.OUT_SCREEN;
                    gameObject.SetActive(false);
                }
            }
        }

        public void updatePosY(float posY)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, posY);
        }

        public void updatePosX(float posX)
        {
            transform.localPosition = new Vector2(posX, transform.localPosition.y);
        }

        public void destory()
        {
            GameObject.Destroy(gameObject);
        }
    }
}
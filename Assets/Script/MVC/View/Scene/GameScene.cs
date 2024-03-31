
namespace Chengzi
{

    public class GameScene : BaseScene
    {

        public override void onEnter()
        {
            _type = SceneType.GAME;
            base.onEnter();

            ViewManager.Instance.getView(ViewConstant.ViewId.GAME_LOADING);
        }

        public override void onExit()
        {
            base.onExit();
        }
    }
}
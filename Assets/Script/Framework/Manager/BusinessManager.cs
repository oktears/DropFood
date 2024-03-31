
namespace Chengzi
{

    /// <summary>
    /// 业务逻辑管理器
    /// </summary>
    public class BusinessManager : Singleton<BusinessManager>
    {
        public UserBiz _userBiz { get; private set; }

        public GameBiz _gameBiz { get; private set; }

        public PayBiz _payBiz { get; private set; }

        public void init()
        {
            _userBiz = new UserBiz();
            _gameBiz = new GameBiz();
            _payBiz = new PayBiz();
        }
    }
}
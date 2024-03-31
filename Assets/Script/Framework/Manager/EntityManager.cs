using Chengzi;

namespace Chengzi
{

    /// <summary>
    /// 实体层管理器
    /// </summary>
    public class EntityManager : Singleton<EntityManager>
    {
        //用户实体
        public UserEntity _userEntity { get; set; }

        public void init()
        {
            this._userEntity = new UserEntity();
        }
    }
}
 
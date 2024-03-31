
namespace Chengzi
{

#if UNITY_ANDROID || UNITY_STANDALONE_WIN
    public class AndroidContext : AndroidBridge
    {

        protected override string javaClassName 
        {
            get { return "com.chengzi.unitybase.platform.PlatformManager"; }
        }

        /// <summary>
        /// 初始化Native上下文环境
        /// </summary>
        public void initPlatformContext()
        {
            callStatic("init");
        }

        /// <summary>
        /// 检测破解
        /// </summary>
        public void checkCrack()
        {
            // //检测是否包含 http://www.sozao.cn/ 的破解代码
            // if (FileUtils.isExistFile(".appkey")
            //     || FileUtils.isExistFile("libjiagu.so")
            //     || FileUtils.isExistFile("libjiagu_ls.so")
            //     || FileUtils.isExistFile("libjiagu_x86.so")
            //     || FileUtils.isExistFile("zsc.bin"))
            // {
            //     PlatformManager.Instance._isCracked = true;
            // }
        }

    }
#endif
}

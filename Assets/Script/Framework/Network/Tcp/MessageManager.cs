using System.Collections.Generic;

namespace Chengzi
{

    /// <summary>
    /// 消息控制器
    /// </summary>
    public class MessageManager
    {
        /// <summary>
        /// 
        /// </summary>
        public void update()
        {
            updateDelayMessage(_delayMessageList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageList"></param>
        private void updateDelayMessage(List<AbstractMessage> messageList)
        {
            if (messageList.Count > 0)
            {
                foreach (AbstractMessage message in messageList)
                {
                    if (message.updateDelayTime())
                    {
                        if (message.update() == false)
                        {
                            _delayMsgRemoveList.Add(message);
                        }
                    }
                }
            }
            if (_delayMsgRemoveList.Count > 0)
            {
                foreach (AbstractMessage removeMessage in _delayMsgRemoveList)
                {
                    messageList.Remove(removeMessage);
                }
                _delayMsgRemoveList.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void addMessage(AbstractMessage msg)
        {
            if (msg.immediatelyRun())
            {
                if (msg.update())
                {
                }
            }
            else
            {
                _delayMessageList.Add(msg);
            }
        }

        // 延时消息容器
        private List<AbstractMessage> _delayMessageList = new List<AbstractMessage>();
        // 延时消息的删除容器
        private List<AbstractMessage> _delayMsgRemoveList = new List<AbstractMessage>();
    }
}


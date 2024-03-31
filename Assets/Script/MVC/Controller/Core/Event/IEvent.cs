using UnityEngine;

namespace Chengzi
{
    public interface IEvent
    {
        //bool onStart(int eventId, object param);

        bool onStart(int eventId);

        bool onStart(int eventId, int param);

        bool onStart(int eventId, float param);

        bool onStart(int eventId, Collision2D param, bool isTwin);

        bool onStart(int eventId, Collider2D param, bool isTwin);

        //bool onExit(int eventId, object param);

        bool onExit(int eventId);

        bool onExit(int eventId, int param);

        bool onExit(int eventId, float param);

        bool onExit(int eventId, Collision2D param, bool isTwin);

        bool onExit(int eventId, Collider2D param, bool isTwin);

        bool onEvent(int eventId, Collision2D param, bool isTwin);

    }
}
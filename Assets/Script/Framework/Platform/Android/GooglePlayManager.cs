// using GooglePlayGames;
// using GooglePlayGames.BasicApi;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using UnityEngine;
//
// namespace Chengzi
// {
//
//     /// <summary>
//     /// GooglePlaySDK
//     /// </summary>
//     public class GooglePlayManager : MonoSingleton<GooglePlayManager>
//     {
//
//         public bool _isLoginSuccess { get; private set; }
//
//         private static string SCORE_RANK_ID = "CgkIoey486gDEAIQAQ";
//
//         public void init()
//         {
//
//
//             //PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
//             //    // enables saving game progress.
//             //    .EnableSavedGames()
//             //    // registers a callback to handle game invitations received while the game is not running.
//             //    .WithInvitationDelegate(< callback method >)
//             //    // registers a callback for turn based match notifications received while the
//             //    // game is not running.
//             //    .WithMatchDelegate(< callback method >)
//             //    // requests the email address of the player be available.
//             //    // Will bring up a prompt for consent.
//             //    .RequestEmail()
//             //    // requests a server auth code be generated so it can be passed to an
//             //    //  associated back end server application and exchanged for an OAuth token.
//             //    .RequestServerAuthCode(false)
//             // requests an ID token be generated.  This OAuth token can be used to
//             //  identify the player to other se
//
//             //PlayGamesPlatform.InitializeInstance(config);
//             // recommended for debugging:
//             PlayGamesPlatform.DebugLogEnabled = true;
//             // Activate the Google Play Games platform
//             PlayGamesPlatform.Activate();
//         }
//
//         public void login()
//         {
//             // authenticate user:
//             PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
//             {
//                 // handle results
//                 if (result == SignInStatus.Success)
//                 {
//                     _isLoginSuccess = true;
//                 }
//                 Debug.Log("login google play " + _isLoginSuccess);
//
//             });
//         }
//
//         public void postScore(int score)
//         {
//             // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
//             Social.ReportScore(score, SCORE_RANK_ID, (bool success) =>
//             {
//                 // handle success or failure
//                 Debug.Log("post score to google play " + success);
//             });
//         }
//
//         public void showRank()
//         {
//             if (!_isLoginSuccess)
//             {
//                 PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
//                 {
//                     // handle results
//                     if (result == SignInStatus.Success)
//                     {
//                         _isLoginSuccess = true;
//                         PlayGamesPlatform.Instance.ShowLeaderboardUI(SCORE_RANK_ID);
//                     }
//                     Debug.Log("login google play " + _isLoginSuccess);
//                 });
//
//             }
//             else
//             {
//                 PlayGamesPlatform.Instance.ShowLeaderboardUI(SCORE_RANK_ID);
//             }
//         }
//     }
// }

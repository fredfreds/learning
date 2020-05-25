using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Facade
{
    //-----------------------------------------VARIANT 1------------------

    public class Message
    {
        private string msg;

        public Message(string msg)
        {
            this.msg = msg;
        }
    }

    public class NotificationServer
    {
        public Connection Connect(string ip)
        {
            return new Connection();
        }

        public AuthToken Authenticate(string id, string k)
        {
            return new AuthToken();
        }

        public string Send(AuthToken a, Message m, string t)
        {
            return "Send";
        }
    }

    public class NotificationService
    {
        public string Send(string msg, string t)
        {
            NotificationServer n = new NotificationServer();
            Connection c = n.Connect("ip");
            AuthToken a = n.Authenticate("id", "key");
            Message m = new Message(msg);
            return n.Send(a, m, t);
            c.Disconnect();
        }
    }

    public class Connection
    {
        public void Disconnect()
        {

        }
    }

    public class AuthToken
    {

    }

    //-----------------------------------------VARIANT 2------------------

    public class OAuth
    {
        public string RequestToken(string appKey, string appSecret)
        {
            return "requestToken";
        }

        public string GetAccessToken(string requestToken)
        {
            return "accessToken";
        }
    }

    public class Tweet
    {
        public string Data = "First Tweet";
    }

    public class TwitterClient
    {
        public List<Tweet> GetRecentTweets(string accessToken)
        {
            return new List<Tweet>() { new Tweet(), new Tweet() };
        }
    }

    public class TwitterService
    {
        private string appKey;
        private string appSecret;

        public TwitterService(string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
        }

        public string GetTweets()
        {
            TwitterClient c = new TwitterClient();
            List<Tweet> tweets = c.GetRecentTweets(GetAccessToken());

            string t = "";
            foreach (var item in tweets)
            {
                t += item.Data + "\n";
            }
            return t;
        }

        private string GetAccessToken()
        {
            OAuth o = new OAuth();
            string rT = o.RequestToken(appKey, appSecret);
            string aT = o.GetAccessToken(rT);

            return aT;
        }
    }

    public class Facade : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;

        NotificationService s = new NotificationService();
        TwitterService ts;

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            InfoText.text = s.Send("Message", "Target");

            ts = new TwitterService("appKey", "appSecret");
            Info2Text.text = ts.GetTweets();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}
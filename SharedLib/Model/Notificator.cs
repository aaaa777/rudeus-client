//using CommunityToolkit.Maui.Alerts;
//using CommunityToolkit.Maui.Core;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model
{
    /// <summary>
    /// 通知を行うクラス
    /// </summary>
    internal class Notificator
    {
        /*
        public static async void ToastMessage(string payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException("payload");
            }

            CancellationTokenSource cancellationTokenSource = new();

            string text = payload;
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
        */

        public static void Toast(string title, string message)
        {
            new ToastContentBuilder()
                //.AddArgument("action", "viewConversation")
                //.AddArgument("conversationId", 9813)
                .AddText(title)
                .AddText(message)
                .Show();
        }
    }
}

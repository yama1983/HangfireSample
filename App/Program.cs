using System;
using System.Threading;
using Hangfire;
using Jobs;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            // キューを登録するRedisの接続設定
            GlobalConfiguration.Configuration.UseRedisStorage("localhost:6379");

            // キュー登録アクション
            var enqueueActions = new Action<string>[]
            {
                message => BackgroundJob.Enqueue<IJob1>(job => job.Process(message)),
                message => BackgroundJob.Enqueue<IJob2>(job => job.Process(message)),
                message => BackgroundJob.Enqueue<IJob3>(job => job.Process(message)),
                message => BackgroundJob.Enqueue<IJob4>(job => job.Process(message)),
                message => BackgroundJob.Enqueue<IJob5>(job => job.Process(message))
            };

            BackgroundJob.Enqueue<IJob1>(job => job.Process("a"));

            // キューに送るメッセージ
            var messages = new []
            {
                "Are you open?",
                "Consume soon after opening.",
                "Refrigerate after opening.",
                "How may I help you?",
                "Could you help me?",
                "Yes, that one.",
                "When does the sale start?",
                "What is the arrival time?",
                "Where is the Taxi stand?"
            };

            var random = new Random();

            // デモ用なのでループさせてたくさんEnqueueする
            while (true)
            {
                var i = random.Next(0, enqueueActions.Length);
                var j = random.Next(0, messages.Length);

                // キュー登録を実行
                enqueueActions[i](messages[j]);

                Console.WriteLine($"ジョブ: {i + 1}, メッセージ: \"{messages[j]}\" をキュー登録しました。");

                Thread.Sleep(1000);
            }
        }
    }
}
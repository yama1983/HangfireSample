using System;
using Hangfire;
using Jobs;

namespace Server
{
    public class CronJob
    {
        private static TimeZoneInfo Jst { get; } = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

        public static void Register()
        {
            // 計画実行ジョブの登録
            // 1インターフェース1ジョブ扱いで同じものを2度実行すると後勝ちになる。
            RecurringJob.AddOrUpdate<IPlanningJob1>(job => job.Process("process 1 minute. */1 * * * *"),   "*/1 * * * *", Jst);
            RecurringJob.AddOrUpdate<IPlanningJob2>(job => job.Process("process 1 minute. Cron.Minutely"), Cron.Minutely, Jst);
        }
    }
}

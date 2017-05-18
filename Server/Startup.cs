using System;
using Hangfire;
using Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // バッチジョブクラスの設定
            services.AddSingleton<IJob1, Job1>();
            services.AddSingleton<IJob2, Job2>();
            services.AddSingleton<IJob3, Job3>();
            services.AddSingleton<IJob4, Job4>();
            services.AddSingleton<IJob5, Job5>();
            services.AddSingleton<IPlanningJob1, PlanningJob1>();
            services.AddSingleton<IPlanningJob2, PlanningJob2>();
            services.AddSingleton<IPlanningJob3, PlanningJob3>();

            // ストレージの設定をします。
            // キューを登録するストレージへの接続設定
            // Redis 接続設定の書き方はこちら https://github.com/StackExchange/StackExchange.Redis/blob/master/docs/Configuration.md
            services.AddHangfire(config
                => config.UseRedisStorage("localhost:6379"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // ログの設定
            // サンプルではNLogを使っていますが、要件に合うものを選択してください。
            loggerFactory
                .AddNLog()
                .ConfigureNLog("D:\\www\\Hangfire\\NLogConfig.xml");

            app.AddNLogWeb();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ワーカーの処理が失敗した場合にリトライを試みる回数の設定です。
            // 実行する処理ごとにリトライ回数を設定する場合は、GlobalJobFilters ではなく、対象のメソッドに [AutomaticRetry(Attempts = x)] を指定してください。
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 5 });

            // ワーカーを起動する設定です。
            //  BackgroundJobServerOptions
            //      ServerName             : サーバー名の設定です。
            //      WorkerCount            : バックグラウンドに起動する Worker の数です。デフォルト値は Environment.ProcessorCount * 5 ですが、最大値は20です。
            //      Queues                 : 処理するキューの種類（タグ）を指定します。デフォルト値は "default" のみです。
            //      ShutdownTimeout        : シャットダウン時に900秒だけジョブの終了を待つ設定です。
            //                               一番重いジョブで想定される処理時間以上の数値に設定してください。
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                ServerName      = "SampleServer01",
                ShutdownTimeout = TimeSpan.FromSeconds(900)
            });

            // ダッシュボードを使用する設定です。
            app.UseHangfireDashboard();

            // 計画実行ジョブを登録します。
            CronJob.Register();

            // とりあえずダッシュボードに遷移するように指定しています。
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<html><head><meta http-equiv=\"refresh\" content =\"0;URL=./hangfire\" ></head><body></body></html>");
            });
        }
    }
}

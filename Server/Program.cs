using System.Diagnostics;
using System.IO;
using Topshelf;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pathToContentRoot = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            HostFactory.Run(host =>
            {
                host.EnableServiceRecovery(recovery => recovery.RestartService(0));

                host.Service<WebHostService>(webHostService =>
                {
                    webHostService.ConstructUsing(setting => new WebHostService(pathToContentRoot));
                    webHostService.WhenStarted(service => service.Start());
                    webHostService.WhenStopped(service => service.Stop());
                });

                host.RunAsLocalSystem();

                // 遅延実行
                host.StartAutomaticallyDelayed();

                //サービス名・説明の登録
                host.SetServiceName("HangfireSample01");
                host.SetDisplayName("HangfireSample01");
                host.SetDescription("Hangfire の サンプル。");
            });
        }
    }
}

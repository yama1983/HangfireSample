using Microsoft.AspNetCore.Hosting;

namespace Server
{
    public class WebHostService
    {
        private IWebHost WebHost { get; set; }
        private string ContentRoot { get; }

        public WebHostService(string contentRoot)
        {
            ContentRoot = contentRoot;
        }

        public void Start()
        {
            WebHost = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(ContentRoot)
                .UseUrls("http://*:80")
                .UseStartup<Startup>()
                .Build();

            WebHost.Start();
        }

        public void Stop()
        {
            WebHost?.Dispose();
        }
    }
}
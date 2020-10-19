using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAppVue
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowCredentials()
                                          .WithOrigins(new string[] { "http://localhost:8080" });
                                  });
            });
            services.AddAntiforgery(options =>
            {
                // Set Cookie properties using CookieBuilder properties†.
                options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "XSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = @"front/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //pattern: "{controller=Home}/{action=Index}/{id?}");
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "front";
                if (env.IsDevelopment())
                {
                    // 開発環境では npm run serve をして 8080 ポートへ自動的にリダイレクトしてくれるようにする。
                    spa.UseProxyToSpaDevelopmentServer(async () =>
                    {
                        var pi = new ProcessStartInfo
                        {
                            FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "cmd" : "yarn",
                            Arguments = $"{(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "/c yarn " : "")} serve",
                            WorkingDirectory = "front",
                            RedirectStandardError = true,
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                        };
                        var p = Process.Start(pi);
                        var lf = app.ApplicationServices.GetService<ILoggerFactory>();
                        var logger = lf.CreateLogger("npm");
                        var tcs = new TaskCompletionSource<int>();
                        _ = Task.Run(() =>
                        {
                            var line = "";
                            while ((line = p.StandardOutput.ReadLine()) != null)
                            {
                                if (line.Contains("DONE  Compiled successfully in ")) // 開発用サーバーの起動待ち
                                {
                                    tcs.SetResult(0);
                                }

                                logger.LogInformation(line);
                            }
                        });
                        _ = Task.Run(() =>
                        {
                            var line = "";
                            while ((line = p.StandardError.ReadLine()) != null)
                            {
                                //logger.LogError(line);
                                //logger.LogDebug(line);
                                logger.LogWarning(line);
                            }
                        });
                        await Task.WhenAny(Task.Delay(20000), tcs.Task);
                        return new Uri("http://localhost:8080");
                    });
                }
            });
        }
    }
}

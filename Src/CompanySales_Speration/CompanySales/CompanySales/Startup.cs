using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanySales.Repository.Business;
using CompanySales.Repository.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace CompanySales
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // AddNewtonsoftJson ʹ�� Newtonsoft ����΢��� System.Text.Json
            services.AddControllers()
                .AddNewtonsoftJson(set =>
                {
                    // Use the default property (Pascal) casing
                    set.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    set.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            // ע���������
            services.AddCors(opt =>
            {
                // ����ȫ�ֿ������global_cors �ǹ������ƣ�UseCors��Ҫʹ�õ�
                opt.AddPolicy("global_cors", cor =>
                {
                    // ����������Դ�� CORS ������κη�����http �� https��
                    cor.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });

            // ���ܵ���Կ����������16λ����Կ�ڡ�appsettings.json�������ļ���
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["token:key"]));

            // �����֤�������ڶ��û���֤���൱�ڵ�¼����
            services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,

                    // �Ƿ���֤�䷢��
                    ValidateIssuer = true,
                    // �䷢�ߵ����ƣ��ڡ�appsettings.json�������ļ���
                    ValidIssuer = Configuration["token:issuer"],

                    // �Ƿ���֤������
                    ValidateAudience = true,
                    // ���������ƣ��ڡ�appsettings.json�������ļ���
                    ValidAudience = Configuration["token:audience"],

                    // �Ƿ������С����ڡ�ֵ��
                    RequireExpirationTime = true,
                    // �Ƿ���������֤�ڼ���֤������
                    ValidateLifetime = true
                };
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            , ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ���չ�������Ӧ�ÿ�����򣬱����� UseRouting ǰӦ��
            app.UseCors("global_cors");

            app.UseRouting();

            // �ܵ���Ӧ����֤�м��
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // ���� MyHttpContext
            var accessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            MyHttpContext.Configure(accessor);

            // ������־������Ҫ��װ Microsoft.Extensions.Logging.Log4Net.AspNetCore ��
            loggerFactory.AddLog4Net();
        }
    }
}

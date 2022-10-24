using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using TradeSpendDashboard.Controllers;
using TradeSpendDashboard.Data.Repository;
using TradeSpendDashboard.Data.Repository.Interface;
using TradeSpendDashboard.Data.Services;
using TradeSpendDashboard.Data.Services.Interface;
using TradeSpendDashboard.Extensions;
using TradeSpendDashboard.Helper;
using TradeSpendDashboard.Model.AppSettings;
using TradeSpendDashboard.Models.AppSettings;
using TradeSpendDashboard.Models.Entity;
using TradeSpendDashboard.Models.Entity.Master;
using TradeSpendDashboard.Services.MasterData;
using TradeSpendDashboard.Services.MasterData.interfaces;
using Rotativa.AspNetCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using TradeSpendDashboard.Data.Repository.Interface.Transaction;
using TradeSpendDashboard.Data.Repository.Transaction;
using TradeSpendDashboard.Data.Services.Interface.Transaction;
using TradeSpendDashboard.Data.Services.Transaction;

namespace TradeSpendDashboard
{
    public class Startup
    {
        IdentityServerOptions identityServerOpt;
        Application application;
        IWebHostEnvironment _environment;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
            identityServerOpt = new IdentityServerOptions();
            application = new Application();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Startup));
            RegisterServices(services);
            RegisterAuthentications(services);
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));
            //services.AddAntiforgery();
            services.AddHangfireServer();
            services.AddMvc();
            //services.Remove(services.First(x => x.ServiceType == typeof(IAntiforgery)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs, Microsoft.AspNetCore.Hosting.IHostingEnvironment env2)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseHangfireDashboard();

            //backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            //RotativaConfiguration.Setup(env2);

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() },
                IgnoreAntiforgeryToken = true//,
                //AppPath = System.Web//VirtualPathUtility.ToAbsolute("~")
            });
            app.UseHangfireServer();

            HangfireJobScheduler.ScheduleRecurringJobs();
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 2 });
            RecurringJob.AddOrUpdate<CallApiHelper>("Sending Email", x => x.SendingEmailReminder(), Cron.Daily(22), TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            //RecurringJob.AddOrUpdate<CallApiHelper>("Hierarchy Business", x => x.UpdateHierarchyBusiness(), Cron.Daily());
            //RecurringJob.AddOrUpdate<CallApiHelper>("Hierarchy Product", x => x.UpdateHierarchyProduct(), Cron.Daily());
            //RecurringJob.AddOrUpdate<CallApiHelper>("Job Read Staging RDNDO", x => x.ReadStagingDirectory(), Cron.MinuteInterval(30));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();
            });
        }

        private void RegisterAuthentications(IServiceCollection services)
        {
            var UserLogin = "";
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            Log.Logger.Information($"ClientId {identityServerOpt.ClientId}");

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = identityServerOpt.Authority;
                    options.ClientId = identityServerOpt.ClientId;
                    options.ClientSecret = identityServerOpt.ClientSecret;
                    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                    options.SaveTokens = true;
                    options.RequireHttpsMetadata = identityServerOpt.RequireHttpsMetadata;
                    options.SignedOutRedirectUri = identityServerOpt.SignedOutRedirectUri;

                    if (!string.IsNullOrWhiteSpace(identityServerOpt.Scope))
                    {
                        foreach (var scope in identityServerOpt.Scope.Split(","))
                        {
                            options.Scope.Add(scope);
                        }
                    }
                    options.MetadataAddress = identityServerOpt.Authority + "/.well-known/openid-configuration";
                })
                  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                  {
                      options.Cookie.Name = "FFI.Cookies";
                      options.Events = new CookieAuthenticationEvents
                      {
                          OnValidatePrincipal = async cookieContext =>
                          {
                              var items = cookieContext.Properties.Items;

                              if (items.ContainsKey(".Token.expires_at"))
                              {
                                  var expire = DateTime.Parse(items[".Token.expires_at"]); //DateTime.Now.AddDays(10);//DateTime.Parse(items[".Token.expires_at"]);
                                  if (expire < DateTime.Now)
                                  {
                                      cookieContext.RejectPrincipal();
                                      cookieContext.Response.StatusCode = 401;
                                      cookieContext.Response.Headers.Remove("Set-Cookie");
                                  }
                              }

                              if (items.ContainsKey(".Token.access_token"))
                              {
                                  var accessToken = items[".Token.access_token"];

                                  var httpClient = new HttpClient();
                                  var discovery = await httpClient.GetDiscoveryDocumentAsync(identityServerOpt.Authority);
                                  if (discovery.IsError)
                                      throw new Exception(discovery.Error);

                                  var userInfoResponse = await httpClient.GetUserInfoAsync(new UserInfoRequest
                                  {
                                      Address = discovery.UserInfoEndpoint,
                                      Token = accessToken
                                  });

                                  if (userInfoResponse.IsError)
                                  {
                                      cookieContext.Response.Redirect("/Error=" + "Error");
                                  }

                                  try
                                  {
                                      var claims = new ClaimsIdentity(cookieContext.Principal.Claims);
                                      claims.AddClaims(userInfoResponse.Claims);
                                      claims.AddClaim(new Claim("access_token", accessToken));
                                      cookieContext.Principal.AddIdentity(claims);

                                      UserLogin = userInfoResponse.Claims.Where(w => w.Type.Equals("user_code")).FirstOrDefault().Value;
                                      //var userRoleString = "1";
                                      var userRoleString = StringRoleId(UserLogin);

                                      if (userRoleString == "0")
                                      {
                                          if (!cookieContext.Request.Path.Value.Contains("/Unauthorized"))
                                              cookieContext.Response.Redirect("/Error/Unauthorized");
                                      }
                                      else
                                      {
                                          claims.AddClaim(new Claim("RoleId", userRoleString));
                                          cookieContext.Principal.AddIdentity(claims);
                                      }

                                  }
                                  catch (Exception e)
                                  {
                                      cookieContext.Response.Redirect("/Errorasd=" + UserLogin + e.Message);
                                  }
                              }
                          }
                      };
                  });
        }

        private void RegisterServices(IServiceCollection services)
        {
            var applicationSection = Configuration.GetSection("Application");
            applicationSection.Bind(application);

            // Register DB Context
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<TradeSpendDashboardContext>((options) =>
            {
                options.UseSqlServer(connectionString, sql =>
                {
                    sql.MigrationsAssembly(migrationsAssembly);
                    sql.CommandTimeout(application.DBCommandTimeout ?? 30); // default 30 seconds
                });
                options.EnableSensitiveDataLogging(application.SQLLogging);
            });

            Application _app = new Application();
            Configuration.GetSection("Application").Bind(_app);
            services.Configure<Application>(Configuration.GetSection(nameof(Application)));

            var identityServerOptSection = Configuration.GetSection("IdentityServerOptions");
            identityServerOptSection.Bind(identityServerOpt);
            services.Configure<IdentityServerOptions>(identityServerOptSection);

            services.Configure<IdentityServerOptions>(Configuration.GetSection(nameof(IdentityServerOptions)));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly(), typeof(IRepository<>));
            services.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly(), typeof(ICustomRepository<>));
            services.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly(), typeof(IService<>));

            //Global
            services.AddScoped<IGlobalRepository, GlobalRepository>();
            services.AddScoped<IGlobalService, GlobalService>();

            //Master Data Trade Spend
            services.AddScoped<IMasterChannelRepository, MasterChannelRepository>();
            services.AddScoped<IMasterChannelService, MasterChannelService>();
            services.AddScoped<IMasterBudgetOwnerRepository, MasterBudgetOwnerRepository>();
            services.AddScoped<IMasterBudgetOwnerService, MasterBudgetOwnerService>();
            services.AddScoped<IMasterBudgetOwnerMapRepository, MasterBudgetOwnerMapRepository>();
            services.AddScoped<IMasterBudgetOwnerMapService, MasterBudgetOwnerMapService>();
            services.AddScoped<IMasterProfitCenterRepository, MasterProfitCenterRepository>();
            services.AddScoped<IMasterProfitCenterService, MasterProfitCenterService>();
            services.AddScoped<IMasterCategoryRepository, MasterCategoryRepository>();
            services.AddScoped<IMasterCategoryService, MasterCategoryService>();
            services.AddScoped<IMasterCategoryMapRepository, MasterCategoryMapRepository>();
            services.AddScoped<IMasterCategoryMapService, MasterCategoryMapService>();
            services.AddScoped<IMasterGLRepository, MasterGLRepository>();
            services.AddScoped<IMasterGLService, MasterGLService>();
            services.AddScoped<IMasterGLTypeRepository, MasterGLTypeRepository>();
            services.AddScoped<IMasterGLTypeService, MasterGLTypeService>();

            //services.AddScoped<IMasterReasonDocumentRepository, MasterReasonDocumentRepository>();
            //services.AddScoped<IMasterReasonDocumentService, MasterReasonDocumentService>();

            //services.AddScoped<IMasterDocumentRepository, MasterDocumentRepository>();
            //services.AddScoped<IMasterDocumentService, MasterDocumentService>();

            //services.AddScoped<IMasterSLARepository, MasterSLARepository>();
            //services.AddScoped<IMasterSLAService, MasterSLAService>();

            //Staging Trade Spend
            services.AddScoped<IMasterCustomerRepository, MasterCustomerRepository>();
            services.AddScoped<IMasterCustomerService, MasterCustomerService>();
            services.AddScoped<IMasterCustomerLevelRepository, MasterCustomerLevelRepository>();
            services.AddScoped<IMasterCustomerLevelService, MasterCustomerLevelService>();

            //Transaction Trade Spend
            services.AddScoped<IUploadRepository, UploadRepository>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IActualRepository, ActualRepository>();
            services.AddScoped<IActualService, ActualService>();
            services.AddScoped<IUpdateRepository, UpdateRepository>();
            services.AddScoped<IUpdateService, UpdateService>();

            //services.AddScoped<IMappingNotaReturRepository, MappingNotaReturRepository>();
            //services.AddScoped<IMappingNotaReturService, MappingNotaReturService>();
            //services.AddScoped<IMappingBillingRepository, MappingBillingRepository>();
            //services.AddScoped<IMappingBillingService, MappingBillingService>();
            //services.AddScoped<IMappingClearingRepository, MappingClearingRepository>();
            //services.AddScoped<IMappingClearingService, MappingClearingService>();
            //services.AddScoped<IRequestGTRepository, RequestGTRepository>();
            //services.AddScoped<IRequestGTService, RequestGTService>();
            //services.AddScoped<IRequestGTDetailRepository, RequestGTDetailRepository>();
            //services.AddScoped<IRequestGTDetailService, RequestGTDetailService>();
            //services.AddScoped<IRequestMTRepository, RequestMTRepository>();
            //services.AddScoped<IRequestMTService, RequestMTService>();
            //services.AddScoped<IRequestMTDetailRepository, RequestMTDetailRepository>();
            //services.AddScoped<IRequestMTDetailService, RequestMTDetailService>();


            ////// Add all service for Master Data


            //services.AddScoped<ICustomerPriceGroupService, CustomerPriceGroupService>();
            //services.AddScoped<ICustomerPriceGroupRepository, CustomerPriceGroupRepository>();

            //MasterProductGroup Repo
            //services.AddScoped<IMasterProductGroupRepository, MasterProductGroupRepository>();
            //services.AddScoped<IMasterProductGroupService, MasterProductGroupService>();

            //services.AddScoped<IMasterEmailHistoryRepository, MasterEmailHistoryRepository>();
            //services.AddScoped<IMasterFormatEmailRepository, MasterFormatEmailRepository>();

            //services.AddScoped<IMasterProductGroupDetailRepository, MasterProductGroupDetailRepository>();
            //services.AddScoped<IMasterProductGroupDetailService, MasterProductGroupDetailService>();


            //services.AddScoped<IProductGroupDetailRepository, ProductGroupDetailRepository>();
            //services.AddScoped<IProductGroupDetailService, ProductGroupDetailService>();


            //services.AddScoped<IHierarchyProductRepository, HierarchyProductRepository>();
            //services.AddScoped<IHierarchyProductService, HierarchyProductService>();

            //services.AddScoped<IMasterSubChannelRepository, MasterSubChannelRepository>();
            //services.AddScoped<IMasterSubChannelService, MasterSubChannelService>();

            services.AddScoped<IMasterRoleRepository, MasterRoleRepository>();
            services.AddScoped<IMasterRoleService, MasterRoleService>();
            services.AddScoped<IMasterRoleBudgetOwnerRepository, MasterRoleBudgetOwnerRepository>();
            services.AddScoped<IMasterRoleBudgetOwnerService, MasterRoleBudgetOwnerService>();
            services.AddScoped<IMasterUsersRepository, MasterUsersRepository>();
            services.AddScoped<IMasterUsersService, MasterUsersService>();
            services.AddScoped<IMasterUsersRoleRepository, MasterUsersRoleRepository>();
            services.AddScoped<IMasterUsersRoleService, MasterUsersRoleService>();
            services.AddScoped<IMasterUsersSpendingRepository, MasterUsersSpendingRepository>();
            services.AddScoped<IMasterUsersSpendingService, MasterUsersSpendingService>();

            //report
            services.AddScoped<IReportRepository, ReportRepository>();

            //services.AddScoped<IMasterUsersCustomerRepository, MasterUsersCustomerRepository>();
            //services.AddScoped<IMasterUsersCustomerService, MasterUsersCustomerService>();

            //services.AddScoped<IMasterOutletRepository, MasterOutletRepository>();
            //services.AddScoped<IMasterGroupOutletRepository, MasterGroupOutletRepository>();
            //services.AddScoped<IMasterGroupOutletService, MasterGroupOutletService>();


            //services.AddHttpClient<IApiMasterOutletService, ApiMasterOutletService>();
            //services.AddHttpClient<IApiProductServices, ApiProductServices>();
            //services.AddHttpClient<IApiUserZoneServices, ApiUsersZoneServices>();
            //services.AddHttpClient<IApiHierarchyBusinessServices, ApiHierarchyBusinessServices>();
            services.AddHttpClient<IUserServices, UserServices>();
            //services.AddHttpClient<IApiUoMServices, ApiUoMServices>();
            //services.AddHttpClient<IApiPriceServices, ApiPriceServices>();

            //services.AddHttpClient<IMailService, MailService>();
            //services.AddScoped<IMasterFormatEmailRepository, MasterFormatEmailRepository>();
            //services.AddScoped<IMasterFormatEmailService, MasterFormatEmailService>();

            //services.AddScoped<IFlowRepository, FlowRepository>();
            //services.AddScoped<IFlowProcessRepository, FlowProcessRepository>();
            //services.AddScoped<IFlowProcessStatusRepository, FlowProcessStatusRepository>();
            //services.AddScoped<IFlowProcessStatusNextRepository, FlowProcessStatusNextRepository>();

            //services.AddScoped<IFlowProcessStatusEmailAssignRepository, FlowProcessStatusEmailAssignRepository>();
            //services.AddScoped<IFlowProcessStatusEmailAssignService, FlowProcessStatusEmailAssignService>();

            //services.AddScoped<IFlowService, FlowService>();
            //services.AddScoped<IFlowProcessService, FlowProcessService>();
            //services.AddScoped<IFlowProcessStatusService, FlowProcessStatusService>();
            //services.AddScoped<IFlowProcessStatusNextService, FlowProcessStatusNextService>();
            //services.AddScoped<IFlowProcessStatusEmailAssignService, FlowProcessStatusEmailAssignService>();


            //services.AddScoped<IMasterParameterRepository, MasterParameterRepository>();
            //services.AddScoped<IMasterParameterService, MasterParameterService>();
            ////services.AddHttpClient<IApiPageableService, ApiPageableService>();

            services.AddScoped<CallApiHelper>();
            services.AddScoped<ExcelHelper>();
            services.AddScoped<DynamicHelper>();
            //services.AddScoped<ButtonHelper>();
            services.AddScoped<MailHelper>();
            services.AddScoped<ReportHelper>();
            //services.AddScoped<DynamicCollectionHelper>();
            services.AddScoped<AppHelper>();
            //services.AddScoped<MasterProductGroup>();


        }

        private string StringRoleId(string userLogin)
        {
            var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var application = settings.GetSection("Application");
            var conn = application["ConnectionStrings"];
            var RoleId = "0";

            SqlConnection sqlCon = new SqlConnection(conn);
            using (sqlCon)
            {
                MasterUsersRole allRow = new MasterUsersRole();
                var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<MasterUsersRole, ExpandoObject>());
                var mapper = new Mapper(mapperConfig);

                using (sqlCon)
                {
                    if (sqlCon.State != ConnectionState.Open)
                        sqlCon.Open();
                    var sql = @"select top 1 * from dbo.MasterUsersRole where UserCode = '" + userLogin + "'";
                    using (SqlCommand command = new SqlCommand(sql, sqlCon))
                    {

                        using (var dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                var retObject = new ExpandoObject();
                                var dataRow = new ExpandoObject() as IDictionary<string, object>;
                                for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                                    dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);

                                retObject = (ExpandoObject)dataRow;
                                var resultData = mapper.Map<MasterUsersRole>(retObject);
                                allRow = resultData;
                            }
                        }
                        RoleId = allRow.RoleId.ToString();
                        return RoleId;
                    }
                }
            }
        }
    }
}


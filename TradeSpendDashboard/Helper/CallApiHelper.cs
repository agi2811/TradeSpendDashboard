using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Amazon.Textract;
using Amazon.Textract.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using TradeSpendDashboard.Model.AppSettings;
using TradeSpendDashboard.Model.DTO;
using TradeSpendDashboard.Models.AppSettings;
using TradeSpendDashboard.Models.DTO;
using TradeSpendDashboard.Models.DTO.MasterData;
using TradeSpendDashboard.Models.Entity.Transaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Helper
{
    public class CallApiHelper
    {
        public string _urlApi;
        public string _baseUrl;
        public readonly Application _app;
        public readonly string _token;
        public readonly HttpClient _client;
        public IdentityServerOptions _identityServerOpt;
        public readonly string _hostMasterData;
        public readonly string _hostInvoiceData;
        public readonly string _hostIdentity;
        public readonly string _BaseUrl;
        public readonly string _ConnectionStrings;
        public readonly string _UploadStagingDir;
        public readonly string _UploadTargetDir;
        public readonly string _UrlApiUser;

        private static TimeZoneInfo timeZoneId { get { return TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); } }

        public CallApiHelper()
        {
            var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var application = settings.GetSection("Application");
            _hostMasterData = application["HostMasterData"];
            _hostInvoiceData = application["HostInvoiceData"];
            _hostIdentity = application["HostIdentity"];
            _baseUrl = application["BaseUrl"];
            _ConnectionStrings = application["ConnectionStrings"];
            _UploadStagingDir = application["UploadStagingDir"];
            _UploadTargetDir = application["UploadTargetDir"];

            var identityServerOptions = settings.GetSection("IdentityServerOptions");
            _UrlApiUser = identityServerOptions["UrlApiUser"];
            _identityServerOpt = new IdentityServerOptions();
            _app = new Application();
            _client = HttpClientHelper.GetCient().GetAwaiter().GetResult();
        }

        public async Task<string> CallingPostApi(HttpContent parameter)
        {
            var content = "";
            var url = _baseUrl + _urlApi;
            var baseAddress = new System.Uri(url);
            var Res = await _client.PostAsync(baseAddress, parameter);
            if (Res.IsSuccessStatusCode)
            {
                content = Res.Content.ReadAsStringAsync().Result;
            }
            return content;
        }

        public List<T> GetDataFromSqlToList<T>(string sql) where T : class
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(_ConnectionStrings);
                List<T> allRow = new List<T>();
                var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<T, ExpandoObject>());
                var mapper = new Mapper(mapperConfig);

                using (sqlCon)
                {
                    if (sqlCon.State != ConnectionState.Open)
                        sqlCon.Open();
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
                                var resultData = mapper.Map<T>(retObject);
                                allRow.Add(resultData);
                            }
                        }

                        return allRow;
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return null;
                throw;
            }
        }

        public T GetDataFromSqlToSingle<T>(string sql) where T : class
        {
            SqlConnection sqlCon = new SqlConnection(_ConnectionStrings);
            var retObject = new ExpandoObject();
            using (var cmd = sqlCon.CreateCommand())
            {
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                            dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);

                        retObject = (ExpandoObject)dataRow;
                    }
                }

                var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<T, ExpandoObject>());
                var mapper = new Mapper(mapperConfig);
                T result = mapper.Map<T>(retObject);
                return result;
            }
        }

        public void ExecuteCommand(string sql)
        {
            if (sql != "")
            {
                SqlConnection sqlCon = new SqlConnection(_ConnectionStrings);
                using (sqlCon)
                {
                    sqlCon.Open();
                    using (SqlCommand command = new SqlCommand(sql, sqlCon))
                    {
                        command.ExecuteScalar();
                        Console.WriteLine(command);
                    }
                }
            }
        }

        public void updateStatusHistory(long reqId = 0, string Status = "PROCESSING")
        {
            var sql = @"";
            if (Status == "PROCESSING")
            {
                var startTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneId).ToString("yyyy-MM-dd HH:mm:ss");
                sql = @"UPDATE dbo.HistoryClaim SET [Status] = 'GENERATED', ProcessStart = '" + startTime + "' WHERE Id = " + reqId.ToString();
            }
            else
            {
                var endTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneId).ToString("yyyy-MM-dd HH:mm:ss");
                sql = @"UPDATE dbo.HistoryClaim SET [Status] = 'GENERATED', ProcessEnd = '" + endTime + "' WHERE Id = " + reqId.ToString();

            }
            ExecuteCommand(sql);
        }

        public async Task<string> CallingGetApi()
        {
            var content = "";
            var url = _baseUrl + _urlApi;
            var baseAddress = new System.Uri(url);
            var Res = await _client.GetAsync(baseAddress);
            if (Res.IsSuccessStatusCode)
            {
                content = Res.Content.ReadAsStringAsync().Result;
            }
            return content;
        }

        // Sending Email
        public async Task<bool> SendingEmailReminder()
        {
            var sqlFull = @"dbo.JobSendingEmail";
            var DataEmail = GetDataFromSqlToList<dynamic>(sqlFull);
            var toEmail = "";
            var ccEmail = "";
            // Set Dummy Email

            if (DataEmail != null)
            {
                if (DataEmail.Count > 0)
                {
                    foreach (var item in DataEmail)
                    {
                        //SendMailAws(item.re, item.cc, item.subject,item.body); ali.mutasal@iforce.co.id
                        //SendMailAws("agi.giyanto@iforce.co.id", "agi.giyanto@iforce.co.id", item.subject, item.body);
                        //"agi.giyanto@iforce.co.id;fany.syafira@frieslandcampina.com;nurhasni.fadilah@frieslandcampina.com;" "tedy.persada@iforce.co.id"
                        toEmail = _UrlApiUser == "dna/api/users" ? "agi.giyanto@iforce.co.id;fany.syafira@frieslandcampina.com;nurhasni.fadilah@frieslandcampina.com;" : item.re;
                        ccEmail = _UrlApiUser == "dna/api/users" ? "" : ""; //"tedy.persada@iforce.co.id;" : item.cc;
                        SendMailAws(toEmail, item.re, ccEmail, item.cc, item.subject, item.body);
                    }
                }
            }       

            return true;
        }

        //public void GetOutstandingEffectiveDateGuideline()
        //{
        //    var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        //    var sqlFull = @"SP_GET_EFFECTIVE_GUIDELINE '" + currentDate + "'";
        //    var ListEfectiveDate = GetDataFromSqlToList<dynamic>(sqlFull);
        //    if (ListEfectiveDate.Count > 0)
        //    {
        //        foreach (var item in ListEfectiveDate)
        //        {
        //            SendMailAws(item.To, item.Cc, item.Content, item.Subject);
        //        }
        //    }
        //}

        public string Get(string secretName)
        {
            var config = new AmazonSecretsManagerConfig { RegionEndpoint = RegionEndpoint.EUWest1 };
            var client = new AmazonSecretsManagerClient(config);

            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            GetSecretValueResponse response = null;
            try
            {
                response = Task.Run(async () => await client.GetSecretValueAsync(request)).Result;
            }
            catch (ResourceNotFoundException)
            {
                Console.WriteLine("The requested secret " + secretName + " was not found");
            }
            catch (InvalidRequestException e)
            {
                Console.WriteLine("The request was invalid due to: " + e.Message);
            }
            catch (Amazon.SecretsManager.Model.InvalidParameterException e)
            {
                Console.WriteLine("The request had invalid params: " + e.Message);
            }

            return response?.SecretString;
        }

        public void SendMailAws(string to, string toInsrt, string cc, string ccInsrt, string subject, string body)
        {
            JObject result = JObject.Parse(Get("SMTP-Dev-Secret"));

            var Username = result["SMTP Username"].ToString();
            var Password = result["SMTP Password"].ToString();

            try
            {
                MailMessage mailMessage = new MailMessage();

                if (!string.IsNullOrEmpty(to))
                {
                    var receivers = to.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var address in receivers)
                    {
                        mailMessage.To.Add(address);
                    }
                }

                if (!string.IsNullOrEmpty(cc))
                {
                    foreach (var address in cc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (to.IndexOf(address) == -1)
                            mailMessage.CC.Add(address);
                    }
                }


                mailMessage.From = new MailAddress("FFI.HelpDesk.ICT@frieslandcampina.com", "Product Return - Product Return Notification");
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                var smtp = new System.Net.Mail.SmtpClient();
                {
                    smtp.Host = "email-smtp.eu-west-1.amazonaws.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(Username, Password);
                    smtp.Timeout = 100000;
                }
                smtp.Send(mailMessage);
                InsertSendMailHistory("", 0, "", toInsrt, ccInsrt, subject, body, 1, "");
            }
            catch (Exception ex)
            {
                InsertSendMailHistory("", 0, "", toInsrt, ccInsrt, subject, body, 0, ex.Message + " " + ex.StackTrace);
                throw new Exception(ex.Message + " " + ex.StackTrace);
            }
        }

        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static string emailFromAddress = "dum.testing.1@gmail.com"; //Sender Email Address  
        static string password = "P@ssw0rd123!"; //Sender Password  
        static string emailToAddress = "agi.giyanto@iforce.co.id"; //ali.mutasal@iforce.co.id Receiver Email Address  
        static string subject = "Hello";
        static string body = "Hello, This is Email sending test using gmail.";
        public void SendEmailByGmail()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.UseDefaultCredentials = false;
                    smtp.Send(mail);
                }
            }
        }

        public async Task<ValidationDTO> ReadStagingDirectory()
        {
            var validationResult = new ValidationDTO();
            var ErrorMessageResult = new ErrorMessage();
            var jobStatus = CheckJobStatus();
            if (jobStatus.Status == "0")
            {
                string[] dirs = Directory.GetFiles($"{_UploadStagingDir}\\NRB\\", "*.pdf");
                Console.WriteLine("The number of files starting with c is {0}.", dirs.Length);
                foreach (string path in dirs)
                {
                    UpdateJobStatus("1");
                    Console.WriteLine(path);
                    var GetDocumentAnalysis = await GenerateOCR("FORMS", path);
                    var document = new TextractDocument(GetDocumentAnalysis);
                    //var ID = _requestMTRepository.CreateIDOCRIDM().ID;
                    DataTable dt = new DataTable("TempOCRNRBForm");
                    dt.Columns.Add("IsActive");
                    dt.Columns.Add("CreatedBy");
                    dt.Columns.Add("CreatedDate");
                    dt.Columns.Add("UpdatedBy");
                    dt.Columns.Add("UpdatedDate");
                    dt.Columns.Add("Key");
                    dt.Columns.Add("Value");
                    dt.Columns.Add("ID");
                    dt.Columns.Add("CustId");

                    int idxdist = document.Pages[0].Lines[21].Text.Trim().Length > 6 ? 20 : 21;
                    var key = document.Pages[0].Lines[11].Text.Trim();
                    var custid = document.Pages[0].Lines[idxdist].Text.Trim();
                    var Delivery_note_number = document.Pages[0].Lines[5].Text.Trim();
                    var Delivery_Date = document.Pages[0].Lines[8].Text.Trim();
                    var Customer_PO_no = document.Pages[0].Lines[11].Text.Trim();
                    var Customer_PO_date = document.Pages[0].Lines[14].Text.Trim();
                    var Our_order_no_date = document.Pages[0].Lines[17].Text.Trim();
                    var Our_ship_to_party_no = document.Pages[0].Lines[idxdist].Text.Trim();

                    //var key = document.Pages[0].Lines[12].Text.Trim();
                    //var Delivery_note_number = document.Pages[0].Lines[6].Text.Trim();
                    //var Delivery_Date = document.Pages[0].Lines[9].Text.Trim();
                    //var Customer_PO_no = document.Pages[0].Lines[12].Text.Trim();
                    //var Customer_PO_date = document.Pages[0].Lines[15].Text.Trim();
                    //var Our_order_no_date = document.Pages[0].Lines[18].Text.Trim();
                    //var Our_ship_to_party_no = document.Pages[0].Lines[22].Text.Trim();

                    DataRow DataRow0 = dt.NewRow();
                    DataRow0["IsActive"] = true;
                    DataRow0["CreatedBy"] = "System";
                    DataRow0["CreatedDate"] = DateTime.Now;
                    DataRow0["UpdatedBy"] = "System";
                    DataRow0["UpdatedDate"] = DateTime.Now;
                    DataRow0["ID"] = key;
                    DataRow0["CustId"] = custid;
                    DataRow0["Key"] = "Delivery_Note_Number";
                    DataRow0["Value"] = Delivery_note_number;
                    dt.Rows.Add(DataRow0);

                    DataRow DataRow1 = dt.NewRow();
                    DataRow1["IsActive"] = true;
                    DataRow1["CreatedBy"] = "System";
                    DataRow1["CreatedDate"] = DateTime.Now;
                    DataRow1["UpdatedBy"] = "System";
                    DataRow1["UpdatedDate"] = DateTime.Now;
                    DataRow1["ID"] = key;
                    DataRow1["CustId"] = custid;
                    DataRow1["Key"] = "Delivery_Date";
                    DataRow1["Value"] = Delivery_Date;
                    dt.Rows.Add(DataRow1);

                    DataRow DataRow2 = dt.NewRow();
                    DataRow2["IsActive"] = true;
                    DataRow2["CreatedBy"] = "System";
                    DataRow2["CreatedDate"] = DateTime.Now;
                    DataRow2["UpdatedBy"] = "System";
                    DataRow2["UpdatedDate"] = DateTime.Now;
                    DataRow2["ID"] = key;
                    DataRow2["CustId"] = custid;
                    DataRow2["Key"] = "Customer_PO_no";
                    DataRow2["Value"] = Customer_PO_no;
                    dt.Rows.Add(DataRow2);

                    DataRow DataRow3 = dt.NewRow();
                    DataRow3["IsActive"] = true;
                    DataRow3["CreatedBy"] = "System";
                    DataRow3["CreatedDate"] = DateTime.Now;
                    DataRow3["UpdatedBy"] = "System";
                    DataRow3["UpdatedDate"] = DateTime.Now;
                    DataRow3["ID"] = key;
                    DataRow3["CustId"] = custid;
                    DataRow3["Key"] = "Customer_PO_date";
                    DataRow3["Value"] = Customer_PO_date;
                    dt.Rows.Add(DataRow3);

                    DataRow DataRow4 = dt.NewRow();
                    DataRow4["IsActive"] = true;
                    DataRow4["CreatedBy"] = "System";
                    DataRow4["CreatedDate"] = DateTime.Now;
                    DataRow4["UpdatedBy"] = "System";
                    DataRow4["UpdatedDate"] = DateTime.Now;
                    DataRow4["ID"] = key;
                    DataRow4["CustId"] = custid;
                    DataRow4["Key"] = "Our_order_no_date";
                    DataRow4["Value"] = Our_order_no_date;
                    dt.Rows.Add(DataRow4);

                    DataRow DataRow5 = dt.NewRow();
                    DataRow5["IsActive"] = true;
                    DataRow5["CreatedBy"] = "System";
                    DataRow5["CreatedDate"] = DateTime.Now;
                    DataRow5["UpdatedBy"] = "System";
                    DataRow5["UpdatedDate"] = DateTime.Now;
                    DataRow5["ID"] = key;
                    DataRow5["CustId"] = custid;
                    DataRow5["Key"] = "Our_ship_to_party_no";
                    DataRow5["Value"] = Our_ship_to_party_no;
                    dt.Rows.Add(DataRow5);

                    TruncateTempOCRNRB(key, custid);

                    string tempName = "dbo.TempOCRNRBForm";

                    try
                    {

                        var conn = _ConnectionStrings;
                        using (SqlConnection sqlCon = new SqlConnection(conn))
                        {
                            sqlCon.Open();
                            using (SqlBulkCopy connect = new SqlBulkCopy(sqlCon))
                            {
                                connect.DestinationTableName = tempName;
                                connect.BatchSize = 5000;
                                connect.BulkCopyTimeout = 7000;
                                for (var i = 0; i <= 8; i++)
                                {
                                    connect.ColumnMappings.Add(i, i);
                                }
                                connect.WriteToServer(dt);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        var m = e.Message;
                        throw;
                    }

                    var checkRdnDo = SpCheckRdnDo(key, custid).FirstOrDefault();
                    if (checkRdnDo.Status == "0")
                    {
                        InsertDataAttachmentOCRNRB(path, key);
                        ErrorMessageResult = SpImportOCRNRB(key, custid).FirstOrDefault();
                        validationResult.Result = ErrorMessageResult.Status != "0" ? false : true;
                        validationResult.Message = ErrorMessageResult.Message;
                        validationResult.ID = ErrorMessageResult.ID;
                    }
                }

                UpdateJobStatus("0");
                validationResult.Result = true;
                validationResult.Message = "Job is already run";
                validationResult.ID = 0;
                return validationResult;
            }
            else
            {
                UpdateJobStatus("0");
                validationResult.Result = false;
                validationResult.Message = "Failed to run the job because job is being run";
                validationResult.ID = 0;
                return validationResult;
            }
        }

        public List<ErrorMessage> SpCheckRdnDo(string key, string custid)
        {
            var sql = $"exec [dbo].[CheckFileRDNExsist] '{key}', '{custid}'";
            var result = GetDataFromSqlToList<ErrorMessage>(sql);
            return result;
        }

        public void TruncateTempOCRNRB(string key, string custid)
        {
            var sql = $"DELETE FROM dbo.TempOCRNRBForm WHERE ID = '{key}' AND CustId = '{custid}'";
            ExecuteCommand(sql);
        }

        public async Task<List<GetDocumentAnalysisResponse>> GenerateOCR(string type, string path)
        {
            string bucket = "textract-product-return";
            //string file = "IDM-SENTUL.pdf";
            ////public static string file = "NRB-IDM-1.pdf";
            ////public static string file = "NRB-IDM.pdf";
            //string filedownload = "test-download.pdf";
            var filename = Path.GetFileName(path);
            System.IO.MemoryStream file = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(path));
            //System.IO.File.Delete(path);

            MemoryStream stream = new MemoryStream();
            file.CopyTo(stream);

            RegionEndpoint bucketRegion = RegionEndpoint.APSoutheast1;

            IAmazonS3 s3Client = new AmazonS3Client(bucketRegion);

            var fileTransferUtility = new TransferUtility(s3Client);
            fileTransferUtility.Upload(stream, bucket, filename);

            // Close the stream file
            stream.Close();

            AmazonTextractClient abcdclient = new AmazonTextractClient();

            StartDocumentAnalysisRequest analyzeDocumentRequest = new StartDocumentAnalysisRequest
            {
                DocumentLocation = new Amazon.Textract.Model.DocumentLocation()
                {
                    S3Object = new Amazon.Textract.Model.S3Object()
                    {
                        Bucket = bucket,
                        Name = filename
                    }

                },
                FeatureTypes = new List<string>
                {
                    //FeatureType.FORMS,
					//FeatureType.TABLES,
					type

                }
            };

            var analyzeDocumentResponse = await abcdclient.StartDocumentAnalysisAsync(analyzeDocumentRequest);

            var GetDocumentAnalysisRequest = new GetDocumentAnalysisRequest();
            GetDocumentAnalysisRequest.JobId = analyzeDocumentResponse.JobId;

            var GetDocumentAnalysis = await abcdclient.GetDocumentAnalysisAsync(GetDocumentAnalysisRequest);
            while (GetDocumentAnalysis.JobStatus.Value != "SUCCEEDED")
            {
                GetDocumentAnalysis = await abcdclient.GetDocumentAnalysisAsync(GetDocumentAnalysisRequest);
            }
            var GetDocumentAnalysisResponse = new List<GetDocumentAnalysisResponse>();
            GetDocumentAnalysisResponse.Add(GetDocumentAnalysis);

            return GetDocumentAnalysisResponse;
        }

        public void InsertDataAttachmentOCRNRB(string path, string ID)
        {
            var filename = UploadedFilePdf(path, "RdnDo", "OCR_" + ID, 0).GetAwaiter().GetResult();
            var key = "OCR_" + ID;
            var sql = $"exec [dbo].[SP_InsertFileAttachment] 'System', 0, '{filename}', '{key}'";
            ExecuteCommand(sql);
        }

        public async Task<string> UploadedFilePdf(string files, string dir, string defaultFileNamexlsx, long requestId)
        {
            try
            {
                var ext = Path.GetExtension(files);
                var pathName = dir;
                var path = $"{_UploadTargetDir}\\{pathName}";
                bool isExists = System.IO.Directory.Exists(path);
                bool isFileExists = System.IO.File.Exists(files);
                var fileName = Path.GetFileName(files);
                var fullPath = "";

                if (isFileExists)
                {

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(path);

                    fullPath = Path.Combine(path, defaultFileNamexlsx + "_" + "System" + "_" + DateTime.Now.ToString("yyyyMMdd") + ext);

                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                    //System.IO.MemoryStream file = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(files));

                    //using (FileStream output = System.IO.File.Create(fullPath))
                    //{
                    //    await file.CopyToAsync(output);
                    //}

                    File.Move(files, fullPath);
                    return fullPath;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public List<ErrorMessage> SpImportOCRNRB(string key, string custid)
        {
            var result = GetDataFromSqlToList<ErrorMessage>($"exec [dbo].[UpdateRequestIDOCRNRB] '{key}', '{custid}'");
            return result;
        }

        public ErrorMessage CheckJobStatus()
        {
            var sql = $"exec dbo.Sp_Check_Job_Running";
            var data = GetDataFromSqlToSingle<ErrorMessage>(sql);
            return data;
        }

        public ErrorMessage UpdateJobStatus(string status = "1")
        {
            var sql = $"exec dbo.Sp_Update_Job_Status '{status}'";
            var data = GetDataFromSqlToSingle<ErrorMessage>(sql);
            return data;
        }

        public ErrorMessage InsertSendMailHistory(string type, 
                                                  int typeID,
                                                  string profileName,
                                                  string sendToEmail,
                                                  string sendCcEmail,
                                                  string subjectEmail,
                                                  string bodyEmail,
                                                  int isSuccessToSend,
                                                  string errorCode)
        {
            var bodyEmailrep = bodyEmail.Replace("\'", "\"");
            var sql = $"exec dbo.Sp_Insert_Send_Mail_History '{type}', {typeID}, '{profileName}', '{sendToEmail}', '{sendCcEmail}', '{subjectEmail}', '{bodyEmailrep}', {isSuccessToSend}, '{errorCode}'";
            var data = GetDataFromSqlToSingle<ErrorMessage>(sql);
            return data;
        }

        public ErrorMessage Sp_Process_Master_Distributor()
        {
            var result = GetDataFromSqlToSingle<ErrorMessage>($"exec dbo.SP_Proccess_Master_Distributor");
            return result;
        }
    }
}
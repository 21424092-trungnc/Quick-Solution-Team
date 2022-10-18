using Business.Entities;
using Business.Entities.Domain;
using CMS_Solution.WebRuby.ViewModel;
using Module.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using log4net;

namespace CMS_Solution.WebRuby.Common
{
    public static class CommonWebsite
    {
        public static string UrlWebsite = ConfigurationManager.AppSettings["URLWEBSITE"];
        public static string FTPWEBSITE = ConfigurationManager.AppSettings["FTPWEBSITE"];
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static UrlRecord GetBySlugCached(string slug)
        {
            using (var DungChungServiceClient = new DungChungServiceClient())
            {
                var temp = DungChungServiceClient.GetUrlRecord(slug);
                if (temp != null && temp.Data != null && temp.Data.resultObject != null)
                {
                    return temp.Data.resultObject;
                }
                return null;
            }
        }
        public static IEnumerable<SelectListItem> GetCBODungChung(CBO_DungChungParam model)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            using (var DungChungServiceClient = new DungChungServiceClient())
            {
                var query = DungChungServiceClient.CBO_DungChung_GetAll(model);
                if (query != null && query.resultObject != null && query.resultObject.Any())
                {
                    list = query.resultObject.Select(m => new SelectListItem()
                    {
                        Value = m.Ma1.ToString(),
                        Text = m.Ten1
                    }).ToList();
                }
            }
            return list;
        }
        public static ProductParam PrepareSearchProductAttributeModel(SearchAttributesViewModel model, ProductPagingFilteringModel command)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (command == null)
                throw new ArgumentNullException("command");
            var result = new ProductParam();
            result.ProductCategoryId = model.CateoryId;
            result.ManufacturerId = model.ManufacturerId;
            if (command.PageIndex <= 0)
                result.PageIndex = 1;
            else
                result.PageIndex = command.PageNumber;
            result.PageSize = command.PageSize > 0 ? command.PageSize : 9;
            if (model.searchPrice.Any())
            {
                result.PriceFrom = model.searchPrice.Min(m => m.PriceFrom);
                result.PriceTo = model.searchPrice.Max(m => m.PriceTo);
            }
            if (model.searchPar.Any())
            {
                foreach (var attr in model.searchPar)
                {
                    result.ProductAttributeId += attr.Id + ",";
                }
            }
            return result;
        }
        public static Files SaveFileUpload(string root, string url, HttpPostedFileBase file)
        {
            var fileinfo = new Files();
            if (file != null && file.ContentLength > 0)
            {
                DateTime now = DateTime.Now;
                string month = now.Month.ToString();
                string year = now.Year.ToString();
                string pathFile = Path.Combine(root, url, year, month);
                DirectoryInfo dir = new DirectoryInfo(pathFile);
                if (!dir.Exists)
                {
                    Directory.CreateDirectory(pathFile);
                }
                var fname = Path.GetFileName(file.FileName);
                var originalName = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(fname), string.Format("{0:ddHHmmssfff}", now), Path.GetExtension(fname));
                fileinfo.TenGoc = fname;
                fileinfo.TenUpload = originalName;
                url = Path.Combine(url, year, month, originalName);
                pathFile = Path.Combine(root, url);
                file.SaveAs(pathFile);
            }
            fileinfo.Url = url;
            return fileinfo;
        }

        public static Files SaveFileWithFTP(HttpPostedFileBase file)
        {
            var fileinfo = new Files();
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fname = Path.GetFileName(file.FileName);
                    var originalName = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(fname), string.Format("{0:ddHHmmssfff}", DateTime.Now), Path.GetExtension(fname));

                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(FTPWEBSITE + originalName));
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    // This example assumes the FTP site uses anonymous logon.
                    // request.Credentials = new NetworkCredential("anonymous", "janeDoe@contoso.com");

                    byte[] fileContents;
                    using (StreamReader sourceStream = new StreamReader(file.InputStream))
                    {
                        fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                    }
                    request.ContentLength = fileContents.Length;
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(fileContents, 0, fileContents.Length);
                    }
                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == FtpStatusCode.CommandOK)
                        {
                            fileinfo.TenGoc = fname;
                            fileinfo.TenUpload = originalName;
                            fileinfo.Url = originalName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return fileinfo;
        }

        public static string GetAuthWithPortal(AuthViewModel jsonObj)
        {
            try
            {
                string apiURL = ConfigurationManager.AppSettings["URLAPIPORTAL"];
                string apiFuntion = ConfigurationManager.AppSettings["api_Auth"];
                var client = new RestClient(apiURL);
                var request = new RestRequest(apiFuntion, Method.POST);
                request.AddHeader("Content-Type", "application/json;charset=UTF-8");
                request.AddHeader("Accept", "application/json");
                request.RequestFormat = DataFormat.Json;
                request.AddBody(jsonObj); // jsonObj sẽ được tự động chuyển thành chuỗi json tương ứng
                var response = client.Execute(request);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);
                    if (obj.data != null)
                    {
                        return obj.data.tokenType + " " + obj.data.accessToken ?? "";
                    }
                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static Files UploadFile(HttpPostedFileBase file)
        {
            var fileinfo = new Files();
            try
            {
                string apiURL = ConfigurationManager.AppSettings["URLAPIPORTAL"];
                string apiFuntion = ConfigurationManager.AppSettings["api_Upload"];
                //Get request token
                AuthViewModel jsonObj = new AuthViewModel() {
                    user_name = ConfigurationManager.AppSettings["acc_user_name"],
                    password = ConfigurationManager.AppSettings["acc_password"],
                    platform = ConfigurationManager.AppSettings["acc_platform"]
                };
                string token = GetAuthWithPortal(jsonObj);
                string base64File = string.Empty;
                if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(token))
                {
                    byte[] binaryFile = new byte[file.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(file.InputStream))
                    {
                        binaryFile = theReader.ReadBytes(file.ContentLength);
                    }
                    base64File = Convert.ToBase64String(binaryFile);

                    var fname = Path.GetFileName(file.FileName);
                    var extensionFile = Path.GetExtension(fname);

                    FileDataModel fileModel = new FileDataModel()
                    {
                        folder = ConfigurationManager.AppSettings["folderUpload"],
                        base64 = "data:application/" + extensionFile.Substring(1) + ";base64," + base64File
                    };

                    var client = new RestClient(apiURL);
                    var request = new RestRequest(apiFuntion, Method.POST);
                    request.AddHeader("Content-Type", "application/json;charset=UTF-8");
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Authorization", token);
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(fileModel); // jsonObj sẽ được tự động chuyển thành chuỗi json tương ứng
                    var response = client.Execute(request);
                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        var obj = JsonConvert.DeserializeObject<dynamic>(response.Content);
                        if (obj.data !=null && obj.status != null)
                        {
                            fileinfo.Url = obj.data;
                            fileinfo.TenGoc = fname;
                        }
                    }
                    
                }
                return fileinfo;
            }
            catch (Exception ex)
            {
                return fileinfo;
            }
        }
        //public static string ConvertToMoney(string money)
        //{

        //}
    }

    public static class SendMail
    {
        private static DungChungServiceClient _DungChungSrv;
        public static readonly ILog _logger = LogManager.GetLogger(typeof(CommonWebsite));
        public static object SendMailSync(string mailTo, string subject, string body)
        {
            _DungChungSrv = new DungChungServiceClient();
            try
            {
                //result = temp.Data.resultObject;
                var mailUsername = "devrubyfitness@gmail.com";
                var mailPassword = "Des123456@";
                var mailhost = "smtp.gmail.com";
                var mailport = 587;
                var basicCredential = new NetworkCredential(mailUsername.Split('@')[0], mailPassword);
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    MailAddress fromAddress = new MailAddress(mailUsername);
                    using (MailMessage message = new MailMessage())
                    {
                        smtpClient.Port = mailport;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Host = mailhost;
                        smtpClient.Credentials = basicCredential;
                        smtpClient.EnableSsl = true;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        message.From = fromAddress;
                        message.Subject = subject;
                        message.Body = body;
                        message.To.Add(mailTo);
                        message.IsBodyHtml = true;
                        smtpClient.Send(message);
                    }
                }
                return new { status = true, message = "Gửi mail thành công", _catch = "" };
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new { status = false, message = "Gửi mail không thành công", _catch = ex.Message };
            }
        }

        public static bool CMS_ContactUs_Send(ContactUsMap model)
        {
            try
            {
                StringBuilder _bodyEmail = new StringBuilder();
                string html = "";
                html += "<p style='font-weight:bold'>Ruby-Fitness";
                html += "</p>";
                html += "<p style='font-weight:bold'>Cảm ơn bạn đã liên hệ với chúng tôi!";
                html += "<p style='font-weight:bold'>Người gửi: " + model.FullName + "</p>";
                html += "<p style='font-weight:bold'>Số điện thoại: " + model.PhoneNumber + "</p>";
                html += "<p style='font-weight:bold'>Email: " + model.Email + "</p>";
                html += "<p style='font-weight:bold'>Nội dung: " + model.ContentSupport + "</p>";
                _bodyEmail.Append(html);
                SendMailSync(model.Email, "Contact Us", _bodyEmail.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
     
    }
}
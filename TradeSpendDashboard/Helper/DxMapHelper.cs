using TradeSpendDashboard.Model.AppSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TradeSpendDashboard.Helper
{
    public class DxMapHelper
    {
        public static string GetEditorTypeField(string field)
        {
            switch (field)
            {
                case "Datetime":
                    return "dxDateBox";
                case "Date":
                    return "dxDateBox";
                case "SelectBox":
                    return "dxSelectBox";
                case "TextArea":
                    return "dxTextArea";
                case "TextBox":
                    return "dxTextBox";
                default:
                    return "dxTextBox";
            }
        }


          public static string GetDataTypeField(string field)
        {
            switch (field)
            {
                case "Datetime":
                    return "date";
                case "Date":
                    return "date";
                case "SelectBox":
                    return "string";
                case "TextArea":
                    return "string";
                case "TextBox":
                    return "string";
                default:
                    return "string";
            }
        }

        public static string GetFormatField(string field)
        {
            switch (field)
            {
                case "Datetime":
                    return "yyyy-MM-dd";
                case "Date":
                    return "yyyy-MM-dd";
                case "SelectBox":
                    return "";
                case "TextArea":
                    return "";
                case "TextBox":
                    return "";
                default:
                    return "";
            }
        }

        public static string GetDefaultValueField(string field)
        {
            switch (field)
            {
                case "Datetime":
                    return DateTime.Now.ToString("yyyy-MM-dd");
                case "Date":
                    return DateTime.Now.ToString("yyyy-MM-dd");
                case "SelectBox":
                    return "";
                case "TextArea":
                    return "";
                case "TextBox":
                    return "";
                default:
                    return "";
            }
        }

    }

}

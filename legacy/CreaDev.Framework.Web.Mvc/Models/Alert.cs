using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaDev.Framework.Web.Mvc.Models
{
    
    public class Alert
    {
        public enum Type
        {
            Warning = 1,
            Info = 2,
            Success = 3,
            Error = 4,
            Hide = 5,
        }

        public const string WARNING_CSS = "alert alert-warning";
        public const string INFORMATION_CSS = "alert alert-info";
        public const string SUCCESS_CSS = "alert alert-success";
        public const string ERORR_CSS = "alert alert-danger";
        public const string HIDE_CSS = "alert hide";

        [JsonProperty("isAutoHide")]
        public bool IsAutoHide { get; set; }

        [JsonProperty("dissmisable")]
        public bool Dissmisable { get; set; }

        [JsonProperty("showIcon")]
        public bool ShowIcon { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("alertCSS")]
        public string AlertCSS
        {
            get
            {
                switch (AlertType)
                {
                    case Type.Warning:
                        return WARNING_CSS;
                    case Type.Info:
                        return INFORMATION_CSS;
                    case Type.Success:
                        return SUCCESS_CSS;
                    case Type.Error:
                        return ERORR_CSS;
                    case Type.Hide:
                        return HIDE_CSS;
                    default:
                        return HIDE_CSS;
                }
            }
        }

        [JsonProperty("alertType")]
        public Type AlertType { get; set; }

        [JsonProperty("alertTypeMetronic")]
        public string AlertTypeMetronic
        {
            get
            {
                switch (AlertType)
                {
                    case Type.Warning:
                        return "warning";
                    case Type.Info:
                        return "info";
                    case Type.Success:
                        return "success";
                    case Type.Error:
                        return "danger";
                    default:
                        return "";
                }
            }
        }

        public Alert()
        {

        }
        public Alert(string message, Type type, bool isAutoHide = false, bool dismissable = true, bool showIcon = false)
        {
            this.Message = message;
            this.IsAutoHide = isAutoHide;
            this.Dissmisable = dismissable;
            this.AlertType = type;
            this.ShowIcon = showIcon;

            if (showIcon)
                this.Message = GetFontawsomeIconHtml(type) + message;
        }

        public Alert(List<string> messages, Type type)
        {
            this.Dissmisable = true;
            this.AlertType = type;

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='font-droid'>");
            foreach (var item in messages)
            {
                sb.Append("<li>");
                sb.Append(item);
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            Message = sb.ToString();
        }


        public static string GetFontawsomeIconHtml(Type type)
        {

            string icon = "fa-check";

            switch (type)
            {
                case Type.Warning:
                    icon = "fa-warning";
                    break;
                case Type.Info:
                    icon = "fa-info";
                    break;
                case Type.Success:
                    icon = "fa-check";
                    break;
                case Type.Error:
                    icon = "fa-remove";
                    break;
                case Type.Hide:
                    break;
                default:
                    break;
            }

            return $"<i class=\"fa {icon} fa-2x\"></i> ";
        }

    }
}

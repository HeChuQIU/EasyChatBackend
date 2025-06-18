using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace EasyChatBackend.Models
{
    public record ResponseVo<T> : ResponseVo
    {
        public static ResponseVo<T> Success(T data, string info = "请求成功") =>
            new("200", info, "success", data);

        private ResponseVo(string Code,
            string Info,
            string Status,
            T Data) : base(Code, Info, Status)
        {
            this.Status = Status;
            this.Code = Code;
            this.Info = Info;
            this.Data = Data;
        }

        [Required, JsonPropertyName("data")]
        public T Data
        {
            get;
            init;
        }
    }
}

namespace EasyChatBackend.Models
{
    public record ResponseVo
    {
        public static ResponseVo Success(string info = "请求成功") => new("200", info, "success");

        public static ResponseVo Error(string code, string info, string status = "error") => new(code, info, status);

        public static ResponseVo BusinessError(string info) => new("600", info, "error");

        protected ResponseVo(string Code,
            string Info, string Status)
        {
            this.Code = Code;
            this.Info = Info;
            this.Status = Status;
        }

        [Required, JsonPropertyName("code")]
        public string Code
        {
            get;
            init;
        }

        [Required, JsonPropertyName("info")]
        public string Info
        {
            get;
            init;
        }

        [Required, JsonPropertyName("status")]
        public string Status
        {
            get;
            init;
        }

        public void Deconstruct(out string Code, out string Info, out string Status)
        {
            Code = this.Code;
            Info = this.Info;
            Status = this.Status;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Responses
{
    public class ApiResponse<T> where T: IDto
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
            ErrorMessage = "";
            IsSuccess = true;
        }

        public ApiResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccess = false;
        }
    }
}

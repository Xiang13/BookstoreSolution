﻿namespace BookstoreApi.Models.Common
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static ApiResponse SuccessResult(object data) =>
            new ApiResponse { Success = true, Data = data };

        public static ApiResponse FailResult(string message) =>
            new ApiResponse { Success = false, Message = message };
    }
}

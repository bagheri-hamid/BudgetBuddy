﻿using WebApi.Interfaces;

namespace WebApi.ViewModels;

public class Response<T>(int statusCode, string message, bool isSuccess, T data)
    : IResponse<T>
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public bool IsSuccess { get; set; } = isSuccess;
    public T Data { get; set; } = data;
}
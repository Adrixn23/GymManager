using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Business.LogicResults
{
    public class  OperationResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }

        public List<string> Errors { get; set; } = new();

        public string Message { get; set; }


        public static OperationResult<T> Ok(T Data, string Message)
        {
            var Result = new OperationResult<T>();
            Result.Success = true;
            Result.Message = Message;
            Result.Data = Data;
           
            return Result;
        }


        public static OperationResult<T> Fail(string message)
        {
            var result = new OperationResult<T>();
            result.Success = false;
            result.Message = message;
            result.Errors.Add(message);
           
            return result;
        }
    }
}

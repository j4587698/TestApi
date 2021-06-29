namespace TestApi
{
    public class ReturnCode
    {
        public int Code { get; set; }

        public string Message { get; set; }
        
        public object ReturnValue { get; set; }

        public ReturnCode()
        {
            
        }

        public ReturnCode(int code, string message)
        {
            Code = code;
            Message = message;
        }
        
        public ReturnCode(int code, string message, object returnValue)
        {
            Code = code;
            Message = message;
            ReturnValue = returnValue;
        }

        public static ReturnCode Success()
        {
            return new ReturnCode(200, "处理成功");
        }
        
        public static ReturnCode Success(string message)
        {
            return new ReturnCode(200, message);
        }

        public static ReturnCode Success(object returnValue)
        {
            return new ReturnCode(200, "处理成功", returnValue);
        }

        public static ReturnCode Fail(int code, string message)
        {
            return new ReturnCode(code, message);
        }
    }
}
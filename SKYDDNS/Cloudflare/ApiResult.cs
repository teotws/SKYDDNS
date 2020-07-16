namespace SKYDDNS.Cloudflare
{
    /// <summary>
    /// Api返回结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误
        /// </summary>
        public object[] Errors { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public object[] Messages { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Result { get; set; }
    }

    public class ApiResult : ApiResult<object> { }

}

namespace Demo.Products.Common
{
    public struct NotFound { }

    public struct Success { }
    public struct Success<T>(T value) where T : new()
    {
        public T Value { get; set; } = value;
    }
    public struct Failed
    {
        public Exception? Exception { get; set; }
        public string? Error { get; set; }
    }
}

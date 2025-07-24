public class Application
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string Type { get; set; } = "request"; // request / offer / complaint
    public string Message { get; set; }
    public string Status { get; set; } = "submitted"; // submitted / completed
}

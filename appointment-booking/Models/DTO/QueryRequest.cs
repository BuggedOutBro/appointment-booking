namespace appointment_booking.Models.DTO;
public class QueryRequest
{
    public DateTime Date { get; set; }
    public string[] Products { get; set; }
    public string Language { get; set; }
    public string Rating { get; set; }
}


namespace Mango.Web.Models
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSucces { get; set; } = true;
        public string? ErrorMessage { get; set; }
    }
}

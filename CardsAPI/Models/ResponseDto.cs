namespace CardsAPI.Models
{
    public class ResponseDto
    {

        public int Status { get; set; }
        public string Message { get; set; }

        public ResponseDto(int statusCode, string message)
        {
            Status = statusCode;
            Message = message;
        }
    }
}

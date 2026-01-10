namespace api.Shared.Dtos
{
    public class ResultDto
    {
        public int Code { get; set; }
        public List<string> Messages { get; set; }

        public ResultDto()
        {
            Messages = new List<string>();
        }
    }
}

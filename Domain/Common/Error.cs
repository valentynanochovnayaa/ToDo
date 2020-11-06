namespace Domain.Common
{
    public class Error
    {
        public ErrorsEnum Key { get; set; }
        public string Description { get; set; }

        public Error()
        {
            
        }

        public Error(ErrorsEnum key, string description)
        {
            key = Key;
            description = Description;
        }
    }
}
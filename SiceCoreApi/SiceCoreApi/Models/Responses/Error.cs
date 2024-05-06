using System.ComponentModel.DataAnnotations;

namespace SiceCoreApi.Models.Responses
{
    public class Error
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Message { get; set; }

        public string Target { get; set; }

        public IEnumerable<Error> Details { get; set; }

        public InnerError InnerError { get; set; }
    }
}

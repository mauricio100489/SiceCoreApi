using System.ComponentModel.DataAnnotations;

namespace SiceCoreApi.Models.Responses
{
    public abstract class InnerError
    {
        public string Code { get; set; }

        [Required]
        public abstract string ErrorType { get; }
    }
}

namespace SiceCoreApi.Models.Security
{
    public class UvwMenu
    {
        public string UserId { get; set; }
        public int IdOptionMenu { get; set; }
        public int IdOption { get; set; }
        public string OptionName { get; set; }
        public string OptionRoute { get; set; }
        public string OptionIcon { get; set; }
        public string OptionIconColor { get; set; }
        public string OptionType { get; set; }
        public int? ParentOption { get; set; }
        public int OptionOrderMenu { get; set; }
    }
}

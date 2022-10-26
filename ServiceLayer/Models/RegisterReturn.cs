using ServiceLayer.Enum;

namespace ServiceLayer.Models
{
    public class RegisterReturn
    {
        public RegisterEnum RegisterEnum{ get; set; }
        public string Status { get; set; }
        public string Message{ get; set; }

    }
}

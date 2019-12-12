using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Models
{
    public class NewSummary
    {
        public NewSummary(bool isCreated)
        {
            this.IsCreated = isCreated;
        }

        public NewSummary(bool status, string msg)
            : this(status)
        {
            this.Message = msg;
        }

        public string Message { get; set; }

        public bool IsCreated { get; set; }
    }
}
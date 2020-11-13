using System;
using WebAppVue.Models.Entity;

namespace WebAppVue.Models
{
    public class ErrorViewModel : BaseModel
    {
        public ErrorViewModel() : base() { }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

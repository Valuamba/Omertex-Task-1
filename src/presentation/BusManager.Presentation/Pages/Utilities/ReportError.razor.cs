using Microsoft.AspNetCore.Components;

namespace BusManager.Presentation.Pages.Utilities
{
    public partial class ReportError
    {
        [Parameter]
        public int ErrorCode { get; set; }
        [Parameter]
        public string ErrorDescription { get; set; }
    }
}

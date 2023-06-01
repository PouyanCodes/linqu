
namespace linqu.Core.Interfaces
{
    public interface IViewRenderService
    {
        string RenderToStringAsync(string viewName, object model);
    }
}

using Microsoft.AspNetCore.Components;
namespace DemoBlazorServer.Components;

public partial class TabControl
{
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public RenderFragment<TabPage>? TitleWrapper { get; set; }

    public TabPage? ActivePage { get; set; }

    List<TabPage> Pages = new List<TabPage>();

    public void AddPage(TabPage page)
    {
        Pages.Add(page);
        if (Pages.Count == 1)
            ActivePage = page;
        StateHasChanged();
    }

    public string GetActiveClass(TabPage page)
    {
        return page == ActivePage ? "active" : "";
    }

    public void ActiveTab(TabPage page)
    {
        ActivePage = page;
    }
}


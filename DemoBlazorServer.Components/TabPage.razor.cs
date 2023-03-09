using Microsoft.AspNetCore.Components;
namespace DemoBlazorServer.Components;

public partial class TabPage
{
    [CascadingParameter]
    [EditorRequired]
    public TabControl? Parent { get; set; }

    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    [EditorRequired]
    public string? Title { get; set; }

    protected override void OnInitialized()
    {
        if (Parent == null)
            throw new ArgumentNullException(nameof(Parent), "TabPage must exist within a TabControl");
        Parent.AddPage(this);
    }

}


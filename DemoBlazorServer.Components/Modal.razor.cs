using Microsoft.AspNetCore.Components;
namespace DemoBlazorServer.Components;

public partial class Modal
{
	[Parameter]
	[EditorRequired]
	public string Title { get; set; } = string.Empty;

	[Parameter]
	[EditorRequired]
	public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public EventCallback OnValidate { get; set; }

    [Parameter]
    public string ValidText { get; set; } = "Confirmer";

    [Parameter]
    public string CancelText { get; set; } = "Annuler";

	public bool IsClose { get; set; } = true;

	public void Close()
	{
		IsClose = true;
	}

    public void Open()
    {
        IsClose = false;
    }

	private async Task OnValidateHandler() {
		Close();
        await OnValidate.InvokeAsync();
	}

	private string GetDisplayClass() {
		return IsClose ? "d-none" : "d-block";

    }

}


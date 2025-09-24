public interface IClosableUI
{
    InputActionType HideAction { get; }
    InputActionType[] DisabledActionsOnShow { get; }
    InputActionType[] DisabledActionsOnHide { get; }
    void Show();
    void Hide();
}

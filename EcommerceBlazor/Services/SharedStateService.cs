namespace EcommerceBlazor.Services;

public class SharedStateService
{
    public event Action OnChange;
    private int _totalCartCount = 0;

    public int TotalCartCount
    {
        get => _totalCartCount;
        set
        {
            _totalCartCount = value;
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

}

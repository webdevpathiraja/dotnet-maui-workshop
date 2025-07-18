using MonkeyFinder.Services;
using System.Diagnostics;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;
    public ObservableCollection<Monkey> Monkeys { get; } = new();
    public Command GetMonkeysCommand { get; }

    public MonkeysViewModel(MonkeyService monkeyService)
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
        GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
    }

    async Task GetMonkeysAsync()
    {
        Debug.WriteLine("=== GetMonkeysAsync called ===");

        if (IsBusy)
        {
            Debug.WriteLine("Already busy, returning");
            return;
        }

        try
        {
            IsBusy = true;
            Debug.WriteLine("Starting to fetch monkeys...");

            var monkeys = await monkeyService.GetMonkeys();
            Debug.WriteLine($"Received {monkeys?.Count ?? 0} monkeys from service");

            if (monkeys != null)
            {
                if (Monkeys.Count != 0)
                    Monkeys.Clear();

                foreach (var monkey in monkeys)
                {
                    Debug.WriteLine($"Adding monkey: {monkey.Name}");
                    Monkeys.Add(monkey);
                }

                Debug.WriteLine($"Total monkeys in collection: {Monkeys.Count}");
            }
            else
            {
                Debug.WriteLine("Monkeys list is null!");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception in GetMonkeysAsync: {ex}");
            await Shell.Current.DisplayAlert("Error", $"Unable to load monkeys: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            Debug.WriteLine("=== GetMonkeysAsync finished ===");
        }
    }
}
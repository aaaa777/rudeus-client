using System.Net.Http;

namespace rudeus_client;

public partial class MainPage : ContentPage
{
	int count = 0;

    private static HttpClient sharedClient;

    public MainPage()
	{
		InitializeComponent();
        MainPage.sharedClient = new HttpClient()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
        };
    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

    private void OnInitClicked(object sender, EventArgs e)
    {
        using HttpResponseMessage response = sharedClient.GetAsync($"todos/{count}").Result;

		Console.WriteLine(response.Content);
        var jsonResponse = response.Content.ReadAsStringAsync().Result;
		Init.Text = jsonResponse;
        

	}
}


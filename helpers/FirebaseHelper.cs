using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DotNetEnv;
using Google.Apis.Auth.OAuth2;

public static class FirebaseHelper
{
    private static readonly HttpClient client = new HttpClient();

    static FirebaseHelper()
    {
        Env.Load();
    }

    public static async Task SendPushNotificationToTopicAsync(
        string topic,
        string title,
        string body
    )
    {
        var accessToken = await GetAccessTokenAsync();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            accessToken
        );
        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

        var message = new
        {
            message = new { topic = topic, notification = new { title = title, body = body } },
        };

        var json = JsonSerializer.Serialize(message);

        var response = await client.PostAsync(
            "https://fcm.googleapis.com/v1/projects/exam1-c1a2e/messages:send",
            new StringContent(json, Encoding.UTF8, "application/json")
        );

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error sending FCM: {error}");
        }
    }

    private static async Task<string> GetAccessTokenAsync()
    {
        var base64 = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIAL_B64");
        var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));

        GoogleCredential credential;
        using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        {
            credential = GoogleCredential
                .FromStream(stream)
                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
        }

        return await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
    }
}

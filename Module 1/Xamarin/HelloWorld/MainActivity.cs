using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using GreetingsLibrary;

namespace HelloWorld
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EditText userNameInput = FindViewById<EditText>(Resource.Id.UserNameInput);
            TextView userGreeted = FindViewById<TextView>(Resource.Id.UserGreeted);
            Button greetButton = FindViewById<Button>(Resource.Id.GreetButton);

            greetButton.Click += (sender, e) =>
            {
                userGreeted.Text = GreetingService.GreetPerson(userNameInput.Text);
            };
        }
    }
}
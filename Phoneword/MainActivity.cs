using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;

namespace Phoneword
{
	[Activity (Label = "Phone Word", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			EditText phoneText = FindViewById<EditText> (Resource.Id.PhoneNumberText);
			Button translate = FindViewById<Button> (Resource.Id.TranslateButton);
			Button call = FindViewById<Button> (Resource.Id.CallButton);
			call.Enabled = false;
			string number = string.Empty;

			translate.Click += (object sender, EventArgs e) => {
				number = Phoneword.PhoneTranslator.ToNumber (phoneText.Text);
				if (String.IsNullOrWhiteSpace (number)) {
					call.Text = "Call";
					call.Enabled = false;
				} else {
					call.Text = "Call " + number;
					call.Enabled = true;
				}
			};
				

				call.Click += (object sender, EventArgs e) =>
			{
				// On "Call" button click, try to dial phone number.
				var callDialog = new AlertDialog.Builder(this);
				callDialog.SetMessage("Call " + number + "?");
				callDialog.SetNeutralButton("Call", delegate {
					// Create intent to dial phone
					var callIntent = new Intent(Intent.ActionCall);
					callIntent.SetData(Android.Net.Uri.Parse("tel:" + number));
					StartActivity(callIntent);
				});
				callDialog.SetNegativeButton("Cancel", delegate { });

				// Show the alert dialog to the user and wait for response.
				callDialog.Show();
			};
		}
	}
}



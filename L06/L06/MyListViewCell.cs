using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace L05_2
{
	
	public class MyListViewCell : ViewCell
	{
		private class MyListViewCellButton : Button { }
		public MyListViewCell()
		{
			var nameLabel = new Label
			{
				Text = "Name",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold,
				VerticalTextAlignment = TextAlignment.Center
			};
			nameLabel.SetBinding(Label.TextProperty, new Binding("Name"));

			var addressLabel = new Label
			{
				Text = "Address",
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				VerticalTextAlignment = TextAlignment.Center
			};
			addressLabel.SetBinding(Label.TextProperty, new Binding("Address"));

			var callButton = new MyListViewCellButton
			{
				Text = "Call",
				BackgroundColor = Color.Gray,
				
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				
			};
			callButton.SetBinding(Button.CommandParameterProperty, new Binding("Tel"));
		    callButton.Clicked +=async (sender, e) =>
		    {
		        var b = (Button) sender;
		        var t = b.CommandParameter;
		        Debug.WriteLine("clicked" + t);
#if (DEBUG)

                if (Device.OS == TargetPlatform.iOS)
		        {
                    await ((((b.ParentView as StackLayout).ParentView as ListView).ParentView as StackLayout).ParentView as ContentPage).DisplayAlert("Alert",$"iOS模擬器無法打電話\r點擊的電話為:{t}","OK");
		            return;
		        }
#endif
                Device.OpenUri(new Uri($"tel:{t}"));
		    };

			View = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,

				Padding = new Thickness(6, 10, 6, 10),
				Children = {
					new StackLayout {
						WidthRequest = 280,
						Orientation = StackOrientation.Vertical,
						Children = { nameLabel, addressLabel }
					},
					callButton
				}
			};

			//透過各平台自訂的程式碼抓取不同平台所獲得的螢幕寬度。
			Debug.WriteLine(App.ScreenWidth);
		}
	}
}

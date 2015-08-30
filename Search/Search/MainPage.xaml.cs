using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Search
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void searchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                btnNavigateUrl_Click_1(sender,e);
            }
        }

        private void btnNavigateUrl_Click_1(object sender, RoutedEventArgs e)
        {
            // 导航到指定的 url
            string pre = "https://www.google.com.hk/#newwindow=1&safe=strict&q=";
            string searchString = searchBox.Text;//获得搜索的字符
            string uri = pre + UrlEncode(searchString);
            webView.Navigate(new Uri(uri, UriKind.Absolute));
            webView.NavigationFailed += webView_NavigationFailed;
        }

        async void webView_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            await new MessageDialog(e.WebErrorStatus.ToString()).ShowAsync();
        }

        public static string UrlEncode(string str)//中文字符urlencode
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }
    }

}

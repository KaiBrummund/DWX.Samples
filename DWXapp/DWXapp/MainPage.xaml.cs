using PCLStorage;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DWXapp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public async void OnLoadClicked(object sender, EventArgs args)
        {
            var cc = CrossConnectivity.Current;

            //if check connectivity, check connection type & ping host
            if (cc.IsConnected &&
                cc.ConnectionTypes.Contains(Plugin.Connectivity.Abstractions.ConnectionType.WiFi)
                && await cc.IsRemoteReachable("whatthecommit.com"))
            {
                //load commit message
                var httpclient = new HttpClient();
                var commitmsg = await httpclient.GetStringAsync("http://whatthecommit.com/index.txt"); //should be wrapped in try catch
                commitLabel.Text = commitmsg;

                //store commitmsg
                var rootFolder = FileSystem.Current.LocalStorage;
                var folder = await rootFolder.CreateFolderAsync("commitmsgs", CreationCollisionOption.OpenIfExists);
                var file = await folder.CreateFileAsync("msg.txt", CreationCollisionOption.ReplaceExisting);
                await file.WriteAllTextAsync(commitmsg);
            }
            else
            {
                //try to load cached commitmsg
                var rootFolder = FileSystem.Current.LocalStorage;
                var folder = await rootFolder.CreateFolderAsync("commitmsgs", CreationCollisionOption.OpenIfExists);
                //check if cached msg exists
                if (await folder.CheckExistsAsync("msg.txt") == ExistenceCheckResult.FileExists)
                {
                    var file = await folder.GetFileAsync("msg.txt");
                    commitLabel.Text = await file.ReadAllTextAsync();
                }
                else
                {
                    commitLabel.Text = "No cached commit message :(";
                }
            }



        }
    }
}

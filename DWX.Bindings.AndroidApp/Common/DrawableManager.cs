using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Java.Net;
using Java.Lang;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace DWX.Bindings.AndroidApp.Common

{
    public class DrawableManager
    {
        private readonly Dictionary<string, Drawable> _drawableMap;
        private static DrawableManager _instance;
        private DrawableManager()
        {
            _drawableMap = new Dictionary<string, Drawable>();
        }
        public static DrawableManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DrawableManager();

                return _instance;
            }
        }

        private async Task<Drawable> FetchDrawableAsync(string urlString)
        {
            Drawable dout;
            if (_drawableMap.ContainsKey(urlString))
            {
                if (_drawableMap.TryGetValue(urlString, out dout))
                {
                    return dout;
                }
            }

            try
            {
                Stream inputs = await FetchAsync(urlString);
                Drawable drawable = Drawable.CreateFromStream(inputs, "src");


                if (drawable != null && !_drawableMap.ContainsKey(urlString))
                {
                    _drawableMap.Add(urlString, drawable);
                }

                return drawable;
            }
            catch (MalformedURLException e)
            {
                return null;
            }
            catch (IOException e)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        public void FetchDrawableOnThread(string urlString, ImageView imageView)
        {
            if (_drawableMap.ContainsKey(urlString))
            {
                Drawable dout;
                if (_drawableMap.TryGetValue(urlString, out dout))
                {
                    imageView.SetImageDrawable(dout);
                }

            }
            else
            {

                mHandler handler = new mHandler();
                handler.MyImageView = imageView;

                System.Threading.Thread thread = new System.Threading.Thread(async () =>
                {
                    //TODO : set imageView to a "pending" image
                    Drawable drawable = await FetchDrawableAsync(urlString);
                    Message message = handler.ObtainMessage(1, drawable);
                    handler.SendMessage(message);
                });
                thread.Start();
            }
        }

        private async Task<Stream> FetchAsync(string urlString)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(urlString);
            return await response.Content.ReadAsStreamAsync();
        }
    }
    class mHandler : Handler
    {
        public ImageView MyImageView { get; set; }
        public override void HandleMessage(Message message)
        {
            MyImageView.SetImageDrawable((Drawable)message.Obj);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Podcaster
{
    [Serializable, XmlRoot("Library")]
    public class Library : ObservableObject
    {

        private ObservableCollection<BasePodcast> _Podcasts = new ObservableCollection<BasePodcast>();
        public ObservableCollection<BasePodcast> Podcasts
        {
            get { return _Podcasts; }
            set
            {
                if (value != _Podcasts)
                {
                    _Podcasts = value;
                    OnPropertyChanged("Podcasts");
                }
            }
        }

        public void Save(string filename)
        {
            Serializers.SerializeObject(this, filename);
        }

        public async static Task<Library> Open(string filename)
        {
            var data = await ApplicationData.Current.LocalFolder.GetFolderAsync("userData");
            var file = Path.Combine(data.Path, "userData.xml");
            var podcasts = Serializers.DeseralizeObject<Library>(file);
            if (podcasts != null)
            {
                return podcasts;
            }
            return null;
        }
    }

    public class Episode : ObservableObject
    {
        public string title { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string subtitle { get; set; }
        public string summary { get; set; }
        public string season { get; set; }
        public string episode { get; set; }
        public DateTimeOffset pubDate { get; set; }
        public string category { get; set; }
        public TimeSpan duration { get; set; }

        private bool _HasListened;
        public bool HasListened
        {
            get { return _HasListened; }
            set
            {
                if (value != _HasListened)
                {
                    _HasListened = value;
                    OnPropertyChanged("HasListened");
                }
            }
        }

        private int _CurrentListenTime;
        public int CurrentListenTime
        {
            get { return _CurrentListenTime; }
            set
            {
                if (value != _CurrentListenTime)
                {
                    _CurrentListenTime = value;
                    OnPropertyChanged("CurrentListenTime");
                }
            }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (value != _Title)
                {
                    _Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private int _PodcastId;
        public int PodcastId
        {
            get { return _PodcastId; }
            set
            {
                if (value != _PodcastId)
                {
                    _PodcastId = value;
                    OnPropertyChanged("PodcastId");
                }
            }
        }

        private string _StreamURL;
        public string StreamURL
        {
            get { return _StreamURL; }
            set
            {
                if (value != _StreamURL)
                {
                    _StreamURL = value;
                    OnPropertyChanged("StreamURL");
                }
            }
        }
    }

    public class Serializers
    {
        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <returns>True if successful</returns>
        public static bool SerializeObject<T>(T serializableObject, string filename)
        {
            if (serializableObject == null) return false;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(filename);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Deserialize an xmlFile into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T DeseralizeObject<T>(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return default;
            T objectOut = default;

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filename);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                    }
                }
            }
            catch
            {

            }
            return objectOut;
        }
    }

}

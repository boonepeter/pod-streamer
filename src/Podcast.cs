using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Windows.UI.Xaml.Media.Imaging;

namespace Podcaster
{
    public class Podcast : BasePodcast
    {
        private BitmapImage _AlbumArt;
        public BitmapImage AlbumArt
        {
            get { return _AlbumArt; }
            set
            {
                if (value != _AlbumArt)
                {
                    _AlbumArt = value;
                    OnPropertyChanged("AlbumArt");
                }
            }
        }

        private ObservableCollection<Episode> _Episodes;
        public ObservableCollection<Episode> Episodes
        {
            get { return _Episodes; }
            set
            {
                if (value != _Episodes)
                {
                    _Episodes = value;
                    OnPropertyChanged("Episodes");
                }
            }
        }

        public Podcast()
        {

        }

        public void UpdateEpisodes()
        {
            if (feedUrl != null)
            {
                Episodes = new ObservableCollection<Episode>(Searcher.GetAllEpisodes(feedUrl));
            }
        }
    }

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
            Serializers.SerializeObject(Podcasts, filename);
        }

        public static Library Open(string filename)
        {
            var podcasts = Serializers.DeseralizeObject<List<BasePodcast>>(filename);
            if (podcasts != null)
            {
                Library library = new Library();
                library.Podcasts = new ObservableCollection<BasePodcast>(podcasts);
                return library;
            }
            return null;
        }
    }

    public class Episode : ObservableObject
    {
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

﻿namespace KinectOnViolin.DataModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Samples.Kinect.ControlsBasics.Common;
    using System.Globalization;

    public sealed class DataSource
    {
        private static DataSource sampleDataSource = new DataSource();

        private ObservableCollection<SampleDataCollection> allGroups = new ObservableCollection<SampleDataCollection>();

        private static Uri darkGrayImage = new Uri("Assets/DarkGray.png", UriKind.Relative);
        private static Uri mediumGrayImage = new Uri("assets/mediumGray.png", UriKind.Relative);
        private static Uri lightGrayImage = new Uri("assets/lightGray.png", UriKind.Relative);

        public DataSource(){
            string itemContent = string.Format(
                                    CultureInfo.CurrentCulture,
                                    "Item Content: {0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}\n\n{0}",
                                    "Curabitur class aliquam vestibulum nam curae maecenas sed integer cras phasellus suspendisse quisque donec dis praesent accumsan bibendum pellentesque condimentum adipiscing etiam consequat vivamus dictumst aliquam duis convallis scelerisque est parturient ullamcorper aliquet fusce suspendisse nunc hac eleifend amet blandit facilisi condimentum commodo scelerisque faucibus aenean ullamcorper ante mauris dignissim consectetuer nullam lorem vestibulum habitant conubia elementum pellentesque morbi facilisis arcu sollicitudin diam cubilia aptent vestibulum auctor eget dapibus pellentesque inceptos leo egestas interdum nulla consectetuer suspendisse adipiscing pellentesque proin lobortis sollicitudin augue elit mus congue fermentum parturient fringilla euismod feugiat");

            var group1 = new SampleDataCollection(
                    "Group-1",
                    "Group Title: 3",
                    "Group Subtitle: 3",
                    DataSource.mediumGrayImage,
                    "Group Description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor scelerisque lorem in vehicula. Aliquam tincidunt, lacus ut sagittis tristique, turpis massa volutpat augue, eu rutrum ligula ante a ante");

            SampleDataItem a = new SampleDataItem(
                         "Group-1-Item-3",
                         "Violin1",
                         "\n\nTry with both buttons and gestures",
                         DataSource.darkGrayImage,
                         string.Empty,
                         itemContent="1",
                         group1,
                         typeof(HybridPage));
            SampleDataItem b = new SampleDataItem(
                       "Group-1-Item-3",
                       "Violin2",
                       "\n\nTry with both buttons and gestures",
                       DataSource.darkGrayImage,
                       string.Empty,
                      itemContent = "2",
                       group1,
                       typeof(HybridPage));
            group1.Items.Add(a);
            group1.Items.Add(b);
            this.AllGroups.Add(group1);
        }

        public ObservableCollection<SampleDataCollection> AllGroups
        {
            get { return this.allGroups; }
        }

        public static SampleDataCollection GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = sampleDataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }

        public static SampleDataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = sampleDataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1)
            {
                return matches.First();
            }

            return null;
        }
    }

    /// <summary>
    /// Base class for <see cref="SampleDataItem"/> and <see cref="SampleDataCollection"/> that
    /// defines properties common to both.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:FileHeaderFileNameDocumentationMustMatchTypeName", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]
    public abstract class SampleDataCommon : BindableBase
    {
        /// <summary>
        /// baseUri for image loading purposes
        /// </summary>
        private static Uri baseUri = new Uri("pack://application:,,,/");

        /// <summary>
        /// Field to store uniqueId
        /// </summary>
        private string uniqueId = string.Empty;

        /// <summary>
        /// Field to store title
        /// </summary>
        private string title = string.Empty;

        /// <summary>
        /// Field to store subtitle
        /// </summary>
        private string subtitle = string.Empty;

        /// <summary>
        /// Field to store description
        /// </summary>
        private string description = string.Empty;

        /// <summary>
        /// Field to store image
        /// </summary>
        private ImageSource image = null;

        /// <summary>
        /// Field to store image path
        /// </summary>
        private Uri imagePath = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDataCommon" /> class.
        /// </summary>
        protected SampleDataCommon(string uniqueId, string title, string subtitle, Uri imagePath, string description)
        {
            this.uniqueId = uniqueId;
            this.title = title;
            this.subtitle = subtitle;
            this.description = description;
            this.imagePath = imagePath;
        }

        /// <summary>
        /// Gets or sets UniqueId.
        /// </summary>
        public string UniqueId
        {
            get { return this.uniqueId; }
            set { this.SetProperty(ref this.uniqueId, value); }
        }

        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        public string Subtitle
        {
            get { return this.subtitle; }
            set { this.SetProperty(ref this.subtitle, value); }
        }

        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        public ImageSource Image
        {
            get
            {
                if (this.image == null && this.imagePath != null)
                {
                    this.image = new BitmapImage(new Uri(SampleDataCommon.baseUri, this.imagePath));
                }

                return this.image;
            }

            set
            {
                this.imagePath = null;
                this.SetProperty(ref this.image, value);
            }
        }

        public String getTitle(SampleDataItem e)
        {
            return e.Title;
        }

        public void SetImage(Uri path)
        {
            this.image = null;
            this.imagePath = path;
            this.OnPropertyChanged("Image");
        }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "File is from Windows Store template")]
    public class SampleDataItem : SampleDataCommon
    {
        private string content = string.Empty;
        private SampleDataCollection group;
        private Type navigationPage;

        public SampleDataItem(string uniqueId, string title, string subtitle, Uri imagePath, string description, string content, SampleDataCollection group)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDataItem" /> class.
        /// </summary>
        public SampleDataItem(string uniqueId, string title, string subtitle, Uri imagePath, string description, string content, SampleDataCollection group, Type navigationPage)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.content = content;
            this.group = group;
            this.navigationPage = navigationPage;
        }

        public string Content
        {
            get { return this.content; }
            set { this.SetProperty(ref this.content, value); }
        }

        public SampleDataCollection Group
        {
            get { return this.group; }
            set { this.SetProperty(ref this.group, value); }
        }

        public Type NavigationPage
        {
            get { return this.navigationPage; }
            set { this.SetProperty(ref this.navigationPage, value); }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataCollection : SampleDataCommon, IEnumerable
    {
        private ObservableCollection<SampleDataItem> items = new ObservableCollection<SampleDataItem>();
        private ObservableCollection<SampleDataItem> topItem = new ObservableCollection<SampleDataItem>();

        public SampleDataCollection(string uniqueId, string title, string subtitle, Uri imagePath, string description)
            : base(uniqueId, title, subtitle, imagePath, description)
        {
            this.Items.CollectionChanged += this.ItemsCollectionChanged;
        }

        public ObservableCollection<SampleDataItem> Items
        {
            get { return this.items; }
        }

        public ObservableCollection<SampleDataItem> TopItems
        {
            get { return this.topItem; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        private void ItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        if (this.TopItems.Count > 12)
                        {
                            this.TopItems.RemoveAt(12);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        this.TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        this.TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        this.TopItems.Insert(e.NewStartingIndex, this.Items[e.NewStartingIndex]);
                        this.TopItems.RemoveAt(12);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems.RemoveAt(e.OldStartingIndex);
                        if (this.Items.Count >= 12)
                        {
                            this.TopItems.Add(this.Items[11]);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        this.TopItems[e.OldStartingIndex] = this.Items[e.OldStartingIndex];
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.TopItems.Clear();
                    while (this.TopItems.Count < this.Items.Count && this.TopItems.Count < 12)
                    {
                        this.TopItems.Add(this.Items[this.TopItems.Count]);
                    }

                    break;
            }
        }
    }
}

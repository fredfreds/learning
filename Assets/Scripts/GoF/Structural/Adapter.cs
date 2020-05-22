using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GoF.Adapter
{
    //---------------------------------------------VARIANT 1-----------------

    public interface IFilter
    {
        string Apply(ImageObj image);
    }

    public class BWFilter : IFilter
    {
        public string Apply(ImageObj image)
        {
            return $"BW Filter for {image.Name}";
        }
    }

    public class RandomFilterAdapterComposition : IFilter
    {
        private RandomFilter randomFilter;

        public RandomFilterAdapterComposition(RandomFilter randomFilter)
        {
            this.randomFilter = randomFilter;
        }

        public string Apply(ImageObj image)
        {
            return randomFilter.Filter(image);
        }
    }

    public class RandomFilterAdapterInheritance : RandomFilter, IFilter
    {
        public string Apply(ImageObj image)
        {
            return Filter(image);
        }
    }

    public class RandomFilter
    {
        public string Filter(ImageObj image)
        {
            return $"Random Filter for {image.Name}";
        }
    }

    public class ImageObj
    {
        public string Name = "Default Image";
    }

    public class ImageView
    {
        private ImageObj image;

        public ImageView(ImageObj image)
        {
            this.image = image;
        }

        public string ApplyFilter(IFilter filter)
        {
            return filter.Apply(image);
        }
    }

    //---------------------------------------------VARIANT 2-----------------

    public class GmailClient
    {
        public string Connect()
        {
            return "Connecting to Gmail";
        }

        public string GetEmail()
        {
            return "Downloading";
        }

        public string Disconnect()
        {
            return "Disconnecting";
        }
    }

    public interface IEmailProvider
    {
        string DownloadEmails();
    }

    public class EmailClient
    {
        private List<IEmailProvider> providers = new List<IEmailProvider>();

        public void Add(IEmailProvider provider)
        {
            providers.Add(provider);
        }

        public string DownloadEmails()
        {
            string t = "";
            foreach (var item in providers)
            {
                t += item.DownloadEmails() + "\n";
            }

            return t;
        }
    }

    public class GmailAdapter : IEmailProvider
    {
        private GmailClient gmail;

        public GmailAdapter(GmailClient gmail)
        {
            this.gmail = gmail;
        }

        public string DownloadEmails()
        {

            return gmail.Connect() + "\n" + gmail.GetEmail() + "\n" + gmail.Disconnect();
        }
    }

    public class Adapter : MonoBehaviour
    {
        public Button BuildButton;
        public Text InfoText;
        public Text Info2Text;
        public Text Info3Text;
        public Text Info4Text;

        ImageView iv;
        ImageObj io;
        BWFilter bwf = new BWFilter();
        RandomFilter rf = new RandomFilter();
        RandomFilterAdapterComposition rc;
        RandomFilterAdapterInheritance ri = new RandomFilterAdapterInheritance();

        IEmailProvider provider;
        EmailClient emailClient = new EmailClient();

        private void OnEnable()
        {
            BuildButton.onClick.AddListener(Build);
        }

        private void Build()
        {
            io = new ImageObj();
            iv = new ImageView(io);
            InfoText.text = iv.ApplyFilter(bwf);

            rc = new RandomFilterAdapterComposition(rf);
            Info2Text.text = iv.ApplyFilter(rc);

            Info3Text.text = iv.ApplyFilter(ri);

            provider = new GmailAdapter(new GmailClient());
            emailClient.Add(provider);
            Info4Text.text = emailClient.DownloadEmails();
        }

        private void OnDisable()
        {
            BuildButton.onClick.RemoveListener(Build);
        }
    }
}
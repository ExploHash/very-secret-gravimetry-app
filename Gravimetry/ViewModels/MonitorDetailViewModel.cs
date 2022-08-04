using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gravimetry.Models;
using Gravimetry.Services;
using System.Collections.Generic;
using System.Web;

namespace Gravimetry.ViewModels
{
    public class MonitorDetailViewModel: BaseViewModel, IQueryAttributable
    {
        readonly MonitorService _monitorService = new MonitorService();


        public MonitorDetailViewModel()
        {
        }

        private SiteMonitor _siteMonitor;

        public SiteMonitor siteMonitor
        {
            get { return _siteMonitor; }
            set
            {
                _siteMonitor = value;
                OnPropertyChanged();
            }
        }

        int _monitorId;

        public int MonitorId
        {
            get
            {
                return _monitorId;
            }
            set
            {
                _monitorId = value;
                LoadMonitor(value);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            MonitorId = int.Parse(HttpUtility.UrlDecode(query["MonitorId"]));
        }

        public async Task LoadMonitor(int monitorId)
        {
            try
            {
               siteMonitor = await _monitorService.GetMonitor(monitorId); //grab from api
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}

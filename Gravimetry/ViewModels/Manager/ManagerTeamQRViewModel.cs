using Xamarin.Forms;
using QRCoder;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace Gravimetry.ViewModels.Manager
{
    public class ManagerTeamQRViewModel : BaseViewModel, IQueryAttributable //Needs to be based of BaseViewModel for stupid notify event handling
    {

        public ManagerTeamQRViewModel()
        {
           
        }
        
        int _teamId;

        public int TeamId
        {
            get
            {
                return _teamId;
            }
            set
            {
                _teamId = value;
                LoadQR(value);
            }
        }

        ImageSource _qrCodeImage;

        public ImageSource QRCodeImage
        {
            get
            {
                return _qrCodeImage;
            }
            set
            {
                _qrCodeImage = value;
                OnPropertyChanged();
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            TeamId = int.Parse(HttpUtility.UrlDecode(query["TeamId"]));
        }

        public void LoadQR(int teamId)
        {
            //Generate qr code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode($"{teamId}", QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);
            QRCodeImage = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        }
    }
}

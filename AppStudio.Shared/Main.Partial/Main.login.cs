using System;
using AppStudio.ViewModels;
using AppStudio.Wat;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using NotificationsExtensions.ToastContent;
using Windows.UI.Notifications;
using System.Threading.Tasks;

namespace AppStudio.Views
{
    public sealed partial class MainPage : Page
    {
        private void loginParameters(NotifyEventArgs e)
        {
            //hay un error entre el javascript y el c# y dispara dos veces lo mismo
            //la idea es que si se pasa exactamente el mismo mensaje descarte el segundo
            try
            {
                var valido = true;

                valido = (varControlEvitarDobleLlamado == "" || varControlEvitarDobleLlamado != e.Value);
                varControlEvitarDobleLlamado = e.Value;

                if (valido)
                {
                    var valor = e.Value.Split(',');
                    var tipo = valor[0];

                    if (tipo.Equals("Ingreso"))
                    {
                        ingresarSistema(valor);
                    }
                    else if (tipo.Equals("Push"))
                    {
                        push(valor);
                    }
                    else if (tipo.Equals("Alert"))
                    {
                        alert(valor);
                    }
                    else if (tipo.Equals("Toast"))
                    {
                        toast(valor);
                    }

                    timer();
                }
                //mostrarMensaje(tipo);
                //toast(tipo);
            }
            catch (Exception ex)
            {
                
            }
        }

        private async void alert(string[] valor)
        {
            try
            {
                var mensaje = valor[1];
                var dialog = new MessageDialog(mensaje);
                await dialog.ShowAsync();
            }
            catch (Exception e) { }
        }

        private async void toast(string[] valor)
        {
            try
            {
                var mensaje = valor[1];
                AppStudio.ToastNotifications.DisplayPackageImageToast.Display(mensaje);
            }
            catch (Exception e) { }
        }

       
        private async void ingresarSistema(string[] valor)
        {                        
            var usuario = valor[1];
            var contrasenia = valor[2];
            auth = new Hefesoft.Standard.Util.Auth.Auth();

            var token = await auth.sigIn(new Hefesoft.Standard.Util.table.Usuario() { UserName = usuario, Password = contrasenia });
            var pushEntity = new Hefesoft.Standard.Util.table.PushAzureService() { key = Hefesoft.Standard.Static.Variables_Globales.PushId };
            var webApiHubId = await auth.registerPushService(pushEntity);

            pushEntity.key = Hefesoft.Standard.Static.Variables_Globales.pushChannelUri;
            pushEntity.idhubazure = Hefesoft.Standard.Static.Variables_Globales.PushId;
            pushEntity.platform = "wns";
            pushEntity.tag = "Pacientes, Odontologia";
            await auth.updatePushService(pushEntity);
        }

        public async void push(string[] valor)
        {
            var a = valor[1];
            var mensaje = valor[2];               

            await auth.NotifyService(new Hefesoft.Standard.Util.table.NotifyMessage()
            {
                to_tag = a,
                platform = "wns",
                mensaje = mensaje
            });            
        } 

        public bool lanzado { get; set; }

        public Hefesoft.Standard.Util.Auth.Auth auth { get; set; }

        public static string varControlEvitarDobleLlamado { get; set; }

        public void timer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += (m, n) => 
            {
                varControlEvitarDobleLlamado = "";
                dispatcherTimer.Stop();            
            };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);            
            dispatcherTimer.Start();
        }

        public DispatcherTimer dispatcherTimer { get; set; }
    }
}

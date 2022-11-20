using EasySave.ViewModels;
using System;
using System.Linq;
using System.Windows.Media;

namespace EasySave.Commands
{
    public class SelectColorCommand : CommandBase
    {
        private readonly string _SelectedButton;

        public SelectColorCommand(string SelectedButton)
        {
            //Set the button for testing, later we will take the button name  to enter in the switch case
            _SelectedButton = SelectedButton;
        }

        

        public override void Execute(object parameter)
        {
            using (var dialog = new System.Windows.Forms.ColorDialog())
            {
                // Keeps the user from selecting a custom color.
                dialog.AllowFullOpen = true;
                // Allows the user to get help. (The default is false.)
                dialog.ShowHelp = true;
                // Sets the initial color select to the current text color.
                dialog.Color = System.Drawing.Color.FromArgb(120, 255, 0, 0);

                //// Update the text box color if the user clicks OK 
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Create a byte table who contains RGB from dialog returned color
                    byte[] color = new byte[3] { dialog.Color.R, dialog.Color.G, dialog.Color.B };
                    //Return a web formated string (red => #FF0000)
                    string ColorPicked = "#" + BitConverter.ToString(color).Replace("-", string.Empty);
                    switch (_SelectedButton)
                    {
                        case "Primary":
                            //Update Dynamic Resource (Color > PrimaryColor)
                            System.Windows.Application.Current.Resources["PrimaryColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorPicked));
                            break;
                        case "Secondary":
                            //Update Dynamic Resource (Color > SecondaryColor)
                            System.Windows.Application.Current.Resources["SecondaryColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorPicked)); ;
                            break;
                        case "Font":
                            //Update Dynamic Resource (Color > FontColor)
                            System.Windows.Application.Current.Resources["FontColor"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorPicked)); ;
                            break;
                        default:
                            break;
                    }
                    
                }

            }
        }

    }
}

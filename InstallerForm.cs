using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WindowsExtensionSearchImageByGoogle.Helpers;
using WindowsExtensionSearchImageByGoogle.Helpers.DesktopHelpers;

namespace WindowsExtensionSearchImageByGoogle
{
    public partial class InstallerForm : Form
    {
        public InstallerForm()
        {
            InitializeComponent();
        }

        private void InstallerForm_Load(object sender, EventArgs e)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Text += $@" v{version.Major}.{version.Minor}.{version.Build}";
            
            foreach (var type in Enum.GetValues(typeof(ImageFileType))) 
                fileTypesListBox.Items.Add(type, true);
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            var menuText = menuTitleTextBox.Text;
            var includeFileName = includeFileNameCheckBox.Checked;
            var allUsers = installForAllUsersCheckBox.Checked;
            var resizeOnUpload = resizeOnUploadCheckbox.Checked;
            var types = fileTypesListBox.CheckedItems.Cast<ImageFileType>().ToArray();
            Install(menuText, includeFileName, allUsers, resizeOnUpload, types);
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            var allUsers = installForAllUsersCheckBox.Checked;
            var types = fileTypesListBox.CheckedItems.Cast<ImageFileType>().ToArray();
            Uninstall(allUsers, types);
        }
        
        private static void Install(string menuText, bool includeFileName, bool allUsers, bool resizeOnUpload,
            IEnumerable<ImageFileType> types)
        {
            try
            {
                var desktopContextMenu = new DesktopContextMenu();
                desktopContextMenu.InstallHandler(menuText, includeFileName, allUsers, resizeOnUpload, types);
            }
            catch (Exception ex)
            {
                ErrorMsgBox(
                    "Installation failed",
                    "Could not add context menu entries to Windows Explorer.\n\n" +
                    ex.Message);
                return;
            }

            InfoMsgBox(
                "Installation succeeded",
                "Context menu entries were added to Windows Explorer. " +
                "Remember to reinstall the program if you move or rename it!");
        }

        private static void Uninstall(bool allUsers, ImageFileType[] types)
        {
            try
            {
                var desktopContextMenu = new DesktopContextMenu();
                desktopContextMenu.UninstallHandler(allUsers, types);
            }
            catch (Exception ex)
            {
                ErrorMsgBox(
                    "Uninstallation failed",
                    "Could not remove context menu entries from Windows Explorer.\n\n" +
                    ex.Message);
                return;
            }

            InfoMsgBox(
                "Uninstallation succeeded",
                "Context menu entries were removed from Windows Explorer.");
        }

        private static void InfoMsgBox(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void ErrorMsgBox(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
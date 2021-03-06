﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Win32;

namespace WindowsExtensionSearchImageByGoogle.Helpers.DesktopHelpers
{
    public class DesktopContextMenu
    {
        private const string ShellKeyPathFormat = @"Software\Classes\SystemFileAssociations\{0}\shell";
        private const string VerbName = "ImageSearchInWeb";
        private const string CommandKey = "command";

        private readonly Dictionary<ImageFileType, string[]> _fileTypeMap =
            new Dictionary<ImageFileType, string[]>
            {
                {ImageFileType.JPG, new[] {".jpg", ".jpe", ".jpeg", ".jfif"}},
                {ImageFileType.GIF, new[] {".gif"}},
                {ImageFileType.PNG, new[] {".png"}},
                {ImageFileType.BMP, new[] {".bmp"}}
            };

        /// <summary>
        /// Creates a shell command to run this program.
        /// </summary>
        /// <param name="includeFileName">Whether to include the image file name when uploading</param>
        /// <param name="resizeOnUpload">Whether to resize large images when uploading</param>
        /// <returns>The shell command string</returns>
        private string CreateProgramCommand(bool includeFileName, bool resizeOnUpload)
        {
            var exePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var command = exePath + " search \"%1\"";
            
            if (includeFileName) 
                command += " -n";
            if (resizeOnUpload) 
                command += " -r";
            
            return command;
        }

        /// <summary>
        /// Opens the shell key corresponding to this program,
        /// with read/write permissions.
        /// </summary>
        /// <param name="allUsers">true if installing for all users, false if for current user</param>
        /// <param name="fileType">File extension (".jpg", ".png", etc)</param>
        /// <returns>Registry key object for the specified user/file type</returns>
        private RegistryKey GetShellKey(bool allUsers, string fileType)
        {
            var hiveKey = allUsers ? Registry.LocalMachine : Registry.CurrentUser;
            var shellPath = string.Format(ShellKeyPathFormat, fileType);
            var shellKey = hiveKey.CreateSubKey(shellPath);
            return shellKey;
        }

        /// <summary>
        /// Adds the program to the Windows Explorer context menu.
        /// </summary>
        /// <param name="menuText">The text to display on the context menu</param>
        /// <param name="includeFileName">Whether to include the image file name when uploading</param>
        /// <param name="allUsers">Whether to install for all users</param>
        /// <param name="resizeOnUpload">Whether to resize large images when uploading</param>
        /// <param name="types">Image file types to install the handler for</param>
        public void InstallHandler(string menuText, bool includeFileName, bool allUsers, bool resizeOnUpload,
            IEnumerable<ImageFileType> types)
        {
            var command = CreateProgramCommand(includeFileName, resizeOnUpload);
            foreach (var fileType in types)
            foreach (var typeExt in _fileTypeMap[fileType])
            {
                using var shellKey = GetShellKey(allUsers, typeExt);
                using var verbKey = shellKey.CreateSubKey(VerbName);

                if (verbKey == null) 
                    continue;

                using var cmdKey = verbKey.CreateSubKey(CommandKey);
                verbKey.SetValue("", menuText);
                cmdKey?.SetValue("", command);
            }
        }

        /// <summary>
        /// Removes the program from the Windows Explorer context menu.
        /// </summary>
        /// <param name="allUsers">Whether to uninstall for all users</param>
        /// <param name="types">Image file types to uninstall the handler for</param>
        public void UninstallHandler(bool allUsers, ImageFileType[] types)
        {
            foreach (var fileType in types)
            foreach (var typeExt in _fileTypeMap[fileType])
            {
                using var shellKey = GetShellKey(allUsers, typeExt);
                shellKey?.DeleteSubKeyTree(VerbName, false);
            }
        }
    }
}
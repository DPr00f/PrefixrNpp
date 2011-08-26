using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NppPluginNET;

namespace Prefixr
{
    class Main
    {
        #region " Fields "
        internal const string PluginName = "Prefixr";
        static string iniFilePath = null;
        static bool someSetting = false;
        static int idMyDlg = -1;
        static Bitmap tbBmp = Properties.Resources.prefixr;
        static Bitmap tbBmp_tbTab = Properties.Resources.prefixr_bmp;
        #endregion

        #region " StartUp/CleanUp "
        internal static void CommandMenuInit()
        {
            StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            iniFilePath = sbIniFilePath.ToString();
            if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
            iniFilePath = Path.Combine(iniFilePath, PluginName + ".ini");
            someSetting = (Win32.GetPrivateProfileInt("Prefixr", "PrefixrPluginNppKey", 0, iniFilePath) != 0);

            PluginBase.SetCommand(0, "Perform Prefixr", startPrefixr, new ShortcutKey(true, true, false, Keys.P));
            idMyDlg = 0;
        }
        internal static void SetToolBarIcon()
        {
            toolbarIcons tbIcons = new toolbarIcons();
            tbIcons.hToolbarBmp = tbBmp.GetHbitmap();
            IntPtr pTbIcons = Marshal.AllocHGlobal(Marshal.SizeOf(tbIcons));
            Marshal.StructureToPtr(tbIcons, pTbIcons, false);
            Win32.SendMessage(PluginBase.nppData._nppHandle, NppMsg.NPPM_ADDTOOLBARICON, PluginBase._funcItems.Items[idMyDlg]._cmdID, pTbIcons);
            Marshal.FreeHGlobal(pTbIcons);
        }
        internal static void PluginCleanUp()
        {
            Win32.WritePrivateProfileString("Prefixr", "PrefixrPluginNppKey", someSetting ? "1" : "0", iniFilePath);
        }
        #endregion

        #region " Menu functions "
        internal static String getSelText(IntPtr scintilla, ref bool selected)
        {
            string text = "";
            int start = (int)Win32.SendMessage(scintilla, SciMsg.SCI_GETSELECTIONNSTART, 0, 0);
            int end = (int)Win32.SendMessage(scintilla, SciMsg.SCI_GETSELECTIONNEND, 0, 0);
            int length = end - start +1;
            if (length > 1)
            {
                StringBuilder sb = new StringBuilder(length);
                Win32.SendMessage(scintilla, SciMsg.SCI_GETSELTEXT, 0, sb);
                text = sb.ToString();
                selected = true;
            }
            else
            {
                length = (int)Win32.SendMessage(scintilla, SciMsg.SCI_GETTEXTLENGTH, 0, 0) + 1;
                StringBuilder sb = new StringBuilder(length);
                Win32.SendMessage(scintilla, SciMsg.SCI_SETSELECTIONNSTART, 0, 0);
                Win32.SendMessage(scintilla, SciMsg.SCI_SETSELECTIONNEND, 0, length);
                Win32.SendMessage(scintilla, SciMsg.SCI_GETSELTEXT, 0, sb);
                text = sb.ToString();
                selected = false;
            }

            return text;
        }
        internal static void startPrefixr()
        {
            IntPtr curScintilla = PluginBase.GetCurrentScintilla();
            bool selected = false;
            string css = getSelText(curScintilla, ref selected);
            if (selected)
            {
                Win32.SendMessage(curScintilla, SciMsg.SCI_REPLACESEL, 0, css.getCss());
            }
            else
            {
                Win32.SendMessage(curScintilla, SciMsg.SCI_SETTEXT, 0, css.getCss());
            }
        }
        #endregion
    }
}
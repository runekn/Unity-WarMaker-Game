using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine;

public class ImportWindow  {

    [DllImport("user32.dll")]
    private static extern void OpenFileDialog();

    public static byte[] ShowImageImportWindow()
    {
        var form = new System.Windows.Forms.OpenFileDialog();
        form.Filter = "PNG Files (.png)|*.png|All Files (*.*)|*.*";
        form.FilterIndex = 1;
        var result = form.ShowDialog();
        return File.ReadAllBytes(form.FileName);
    }
}

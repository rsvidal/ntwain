using NTwain;
using NTwain.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace Sample.WindowsForm
{
    sealed partial class TestForm : Form
    {
        private ImageCodecInfo _tiffCodecInfo; // TiffCodec info
        private TwainSession _twain;
        private bool _stopScan;
        private bool _loadingCaps;
        private List<Image> list = new List<Image>();

        #region setup & cleanup

        /// <summary>
        /// Constructor
        /// </summary>
        public TestForm()
        {
            InitializeComponent();

            // Title
            Text = Text + (NTwain.PlatformInfo.Current.IsApp64Bit ? " (64bit)" : " (32bit)");

            // TiffCodecInfo
            foreach (var enc in ImageCodecInfo.GetImageEncoders())
                if (enc.MimeType == "image/tiff")
                {
                    _tiffCodecInfo = enc;
                    break;
                }
        }

        #region Eventos Create and Closing

        // Created form
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetupTwain();
        }

        /// <summary>
        /// Closing form
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_twain != null)
            {
                if (e.CloseReason == CloseReason.UserClosing && _twain.State > 4)
                    e.Cancel = true;
                else
                    CleanupTwain();                
            }
            base.OnFormClosing(e);
        }

        #endregion

        /// <summary>
        /// Setup Twain
        /// </summary>
        private void SetupTwain()
        {
            var appId = TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetEntryAssembly());
            _twain = new TwainSession(appId);

            // StateChanged event
            _twain.StateChanged += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("State changed to " + _twain.State + " on thread " + Thread.CurrentThread.ManagedThreadId);
            };

            // TransferError event
            _twain.TransferError += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Got xfer error on thread " + Thread.CurrentThread.ManagedThreadId);
            };

            // DataTransferred event
            _twain.DataTransferred += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Transferred data event on thread " + Thread.CurrentThread.ManagedThreadId);

                // example on getting ext image info
                var infos = e.GetExtImageInfo(ExtendedImageInfo.Camera).Where(it => it.ReturnCode == ReturnCode.Success);
                foreach (var it in infos)
                {
                    var values = it.ReadValues();
                    PlatformInfo.Current.Log.Info(string.Format("{0} = {1}", it.InfoID, values.FirstOrDefault()));
                    break;
                }

                // handle image data
                // It's possile get the image from NativeData or FileDataPath
                Image img = null;
                if (e.NativeData != IntPtr.Zero)
                {
                    var stream = e.GetNativeImageStream();
                    if (stream != null)
                        img = Image.FromStream(stream);
                        
                }
                else 
                    if (!string.IsNullOrEmpty(e.FileDataPath))
                        img = new Bitmap(e.FileDataPath);

                // Show image in the picturebox1 control
                if (img != null)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        list.Add(img);
                        comboImages.Items.Add(list.Count);
                        comboImages.Enabled = true;
                    }));
                }
            };

            // SourceDisabled event
            _twain.SourceDisabled += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Source disabled event on thread " + Thread.CurrentThread.ManagedThreadId);
                this.BeginInvoke(new Action(() =>
                {
                    btnStopScan.Enabled = false;
                    btnStartCapture.Enabled = true;
                    panelOptions.Enabled = true;
                    LoadSourceCaps();
                }));
            };

            _twain.TransferReady += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Transferr ready event on thread " + Thread.CurrentThread.ManagedThreadId);
                e.CancelAll = _stopScan;
            };

            // either set sync context and don't worry about threads during events,
            // or don't and use control.invoke during the events yourself
            PlatformInfo.Current.Log.Info("Setup thread = " + Thread.CurrentThread.ManagedThreadId);
            _twain.SynchronizationContext = SynchronizationContext.Current;
            if (_twain.State < 3)
            {
                // use this for internal msg loop
                _twain.Open();
                // use this to hook into current app loop
                //_twain.Open(new WindowsFormsMessageLoopHook(this.Handle));
            }
        }

        /// <summary>
        /// CleanupTwain
        /// </summary>
        private void CleanupTwain()
        {
            if (_twain.State == 4)
                _twain.CurrentSource.Close();

            if (_twain.State == 3)
                _twain.Close();

            // normal close down didn't work, do hard kill
            if (_twain.State > 2) 
                _twain.ForceStepDown(2);            
        }

        #endregion

        #region toolbar

        private void btnSources_DropDownOpening(object sender, EventArgs e)
        {
            if (btnSources.DropDownItems.Count == 2)
            {
                ReloadSourceList();
            }
        }

        private void reloadSourcesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadSourceList();
        }

        private void ReloadSourceList()
        {
            if (_twain.State >= 3)
            {
                while (btnSources.DropDownItems.IndexOf(sepSourceList) > 0)
                {
                    var first = btnSources.DropDownItems[0];
                    first.Click -= SourceMenuItem_Click;
                    btnSources.DropDownItems.Remove(first);
                }
                foreach (var src in _twain)
                {
                    var srcBtn = new ToolStripMenuItem(src.Name);
                    srcBtn.Tag = src;
                    srcBtn.Click += SourceMenuItem_Click;
                    srcBtn.Checked = _twain.CurrentSource != null && _twain.CurrentSource.Name == src.Name;
                    btnSources.DropDownItems.Insert(0, srcBtn);
                }
            }
        }

        private void SourceMenuItem_Click(object sender, EventArgs e)
        {
            // do nothing if source is enabled
            if (_twain.State > 4) { return; }

            if (_twain.State == 4) { _twain.CurrentSource.Close(); }

            foreach (var btn in btnSources.DropDownItems)
            {
                var srcBtn = btn as ToolStripMenuItem;
                if (srcBtn != null) { srcBtn.Checked = false; }
            }

            var curBtn = (sender as ToolStripMenuItem);
            var src = curBtn.Tag as DataSource;
            if (src.Open() == ReturnCode.Success)
            {
                curBtn.Checked = true;
                btnStartCapture.Enabled = true;
                LoadSourceCaps();
            }
        }

        private void btnStartCapture_Click(object sender, EventArgs e)
        {
            if (_twain.State == 4)
            {
                _stopScan = false;

                if (_twain.CurrentSource.Capabilities.CapUIControllable.IsSupported) //.SupportedCaps.Contains(CapabilityId.CapUIControllable))
                {
                    // hide scanner UI if possible
                    if (_twain.CurrentSource.Enable(SourceEnableMode.NoUI, false, this.Handle) == ReturnCode.Success)
                    {
                        btnStopScan.Enabled = true;
                        btnStartCapture.Enabled = false;
                        panelOptions.Enabled = false;
                    }
                }
                else
                {
                    if (_twain.CurrentSource.Enable(SourceEnableMode.ShowUI, true, this.Handle) == ReturnCode.Success)
                    {
                        btnStopScan.Enabled = true;
                        btnStartCapture.Enabled = false;
                        panelOptions.Enabled = false;
                    }
                }
            }
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            _stopScan = true;
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            var img = pictureBox1.Image;

            if (img != null)
            {
                switch (img.PixelFormat)
                {
                    case PixelFormat.Format1bppIndexed:
                        saveFileDialog1.Filter = "tiff files|*.tif";
                        break;

                    default:
                        saveFileDialog1.Filter = "png files|*.png";
                        break;
                }

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (saveFileDialog1.FileName.EndsWith(".tif", StringComparison.OrdinalIgnoreCase))
                    {
                        EncoderParameters tiffParam = new EncoderParameters(1);
                        tiffParam.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionCCITT4);
                        pictureBox1.Image.Save(saveFileDialog1.FileName, _tiffCodecInfo, tiffParam);
                    }
                    else
                    {
                        pictureBox1.Image.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    }
                }
            }
        }

        #endregion

        #region cap load

        private void LoadSourceCaps()
        {
            var src = _twain.CurrentSource;
            _loadingCaps = true;
            
            // DPI
            if (groupDPI.Enabled = src.Capabilities.ICapXResolution.IsSupported && src.Capabilities.ICapYResolution.IsSupported)
            {
                LoadDPI(src.Capabilities.ICapXResolution);
            }
            
            // Depth
            if (groupDepth.Enabled = src.Capabilities.ICapPixelType.IsSupported)
            {
                LoadDepth(src.Capabilities.ICapPixelType);
            }

            // Paper size
            if (groupSize.Enabled = src.Capabilities.ICapSupportedSizes.IsSupported)
            {
                LoadPaperSize(src.Capabilities.ICapSupportedSizes);
                GetCaps();
            }

            // Duplex
            // TODO: find out if this is how duplex works or also needs the other option
            if (groupDuplex.Enabled = src.Capabilities.CapDuplexEnabled.IsSupported)
            {
                LoadDuplex(src.Capabilities.CapDuplexEnabled);
            }

            btnAllSettings.Enabled = src.Capabilities.CapEnableDSUIOnly.IsSupported;
            _loadingCaps = false;
        }

        private void LoadDPI(ICapWrapper<TWFix32> cap)
        {
            // Only allow dpi of certain values for those source that lists everything
            var list = cap.GetValues().Where(dpi => (dpi % 50) == 0).ToList();
            comboDPI.DataSource = list;

            var cur = cap.GetCurrent();
            if (list.Contains(cur))
                comboDPI.SelectedItem = cur;            
        }

        private void LoadDepth(ICapWrapper<PixelType> cap)
        {
            var list = cap.GetValues().ToList();
            comboDepth.DataSource = list;

            var cur = cap.GetCurrent();
            if (list.Contains(cur))
                comboDepth.SelectedItem = cur;

            var labelTest = cap.GetLabel();
            if (!string.IsNullOrEmpty(labelTest))
                groupDepth.Text = labelTest;
        }

        private void LoadPaperSize(ICapWrapper<SupportedSize> cap)
        {
            var list = cap.GetValues().ToList();
            comboSize.DataSource = list;

            var cur = cap.GetCurrent();
            if (list.Contains(cur))
                comboSize.SelectedItem = cur;

            var labelTest = cap.GetLabel();
            if (!string.IsNullOrEmpty(labelTest))
                groupSize.Text = labelTest;
        }

        private void GetCaps()
        {
            var caps = _twain.CurrentSource.Capabilities;

            PlatformInfo.Current.Log.Info("\n\n\n");

            // Show Indicator
            PlatformInfo.Current.Log.Info("CapIndicators: " + caps.CapIndicators.GetCurrent());

            // Brightness
            PlatformInfo.Current.Log.Info("ICapAutoBright: " + caps.CapIndicators.GetCurrent());
            PlatformInfo.Current.Log.Info("ICapBrightness: " + caps.ICapBrightness.GetCurrent());

            // Constrast
            PlatformInfo.Current.Log.Info("ICapContrast: " + caps.ICapContrast.GetCurrent());

            // Physical Height & Width
            PlatformInfo.Current.Log.Info("ICapPhysicalHeight: " + caps.ICapPhysicalHeight.GetCurrent());
            PlatformInfo.Current.Log.Info("ICapPhysicalWidth: " + caps.ICapPhysicalWidth.GetCurrent());

            // Enderezamiento automático
            PlatformInfo.Current.Log.Info("ICapAutomaticDeskew: " + caps.ICapAutomaticDeskew.GetCurrent());

            // Rotación automática
            PlatformInfo.Current.Log.Info("ICapAutomaticRotate: " + caps.ICapAutomaticRotate.GetCurrent());

            // Recorte automático
            PlatformInfo.Current.Log.Info("ICapAutomaticBorderDetection: " + caps.ICapAutomaticBorderDetection.GetCurrent());

            // Autosize
            PlatformInfo.Current.Log.Info("ICapAutoSize: " + caps.ICapAutoSize.GetCurrent());

            // Page size (standard size)
            PlatformInfo.Current.Log.Info("ICapSupportedSizes: " + caps.ICapSupportedSizes.GetCurrent());

            // Measure unit (Inches)
            PlatformInfo.Current.Log.Info("ICapUnits: " + caps.ICapUnits.GetCurrent());

            // Page size (in inches)
            TWFrame fr = caps.ICapFrames.GetCurrent();
            PlatformInfo.Current.Log.Info("Custom size (Left, Top, Right, Bottom): " + fr.Left + ", " + fr.Top + ", " + fr.Right + ", " + fr.Bottom);

            /*
            fr.Left = (float)0;
            fr.Top = (float)0;
            fr.Right = (float)210/10;
            fr.Bottom = (float)297/10; */

            PlatformInfo.Current.Log.Info("\n\n\n");
        }

        private void LoadDuplex(ICapWrapper<BoolType> cap)
        {
            ckDuplex.Checked = cap.GetCurrent() == BoolType.True;
        }

        #endregion

        #region cap events

        private void btnAllSettings_Click(object sender, EventArgs e)
        {
            _twain.CurrentSource.Enable(SourceEnableMode.ShowUIOnly, true, this.Handle);
        }

        private void comboDPI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                var sel = (TWFix32)comboDPI.SelectedItem;
                _twain.CurrentSource.Capabilities.ICapXResolution.SetValue(sel);
                _twain.CurrentSource.Capabilities.ICapYResolution.SetValue(sel);
            }
        }

        private void comboDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                var sel = (PixelType)comboDepth.SelectedItem;
                _twain.CurrentSource.Capabilities.ICapPixelType.SetValue(sel);
            }
        }

        private void comboSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                var sel = (SupportedSize)comboSize.SelectedItem;
                _twain.CurrentSource.Capabilities.ICapSupportedSizes.SetValue(sel);
            }
        }

        private void ckDuplex_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                _twain.CurrentSource.Capabilities.CapDuplexEnabled.SetValue(ckDuplex.Checked ? BoolType.True : BoolType.False);
            }
        }

        private void UpDownNumPages_ValueChanged(object sender, EventArgs e)
        {
            if (!_loadingCaps && _twain.State == 4)
            {
                int numPages = Convert.ToInt32(UpDownNumPages.Value);

                _twain.CurrentSource.Capabilities.CapFeederEnabled.SetValue(BoolType.True);
                _twain.CurrentSource.Capabilities.CapXferCount.SetValue(numPages <= 0 ? -1 : numPages);  // -1: Todas los documentos >0: Solo N documentos
            }
        }

        private void comboImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboImages.SelectedIndex < 0)
                    return;

                PlatformInfo.Current.Log.Info("Selecte image changed to " + comboImages.SelectedIndex);
                pictureBox1.Image = list[comboImages.SelectedIndex];
            }
            catch (Exception ex) { }
        }

        #endregion
    }
}

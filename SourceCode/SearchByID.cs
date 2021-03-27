using System;
using System.Reflection;
using System.Windows.Forms;
using WiiCommon;
using WiiController;
using WiiObjects;

namespace Wii
{
    public partial class SearchByID : WiiSystemBase
    {
        private ThreeDSTSVController threeDSTSVController = null;
        // List item collection
        public ItemCollection serchItemSelected = null;

        public SearchByID()
        {
            InitializeComponent();
            threeDSTSVController = new ThreeDSTSVController();
        }

        private void SearchByID_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Load data search
        /// </summary>
        private void LoadData()
        {
            if (serchItemSelected == null)
                return;
            if (serchItemSelected.ItemList().Count == 0)
            {
                dtgSelectId.Rows.Add(4);
                return;
            }
            foreach (var item in serchItemSelected.ItemList())
            {
                dtgSelectId.Rows.Add(item.ItemValue);
            }
            if (serchItemSelected.ItemList().Count < 4)
            {
                dtgSelectId.Rows.Add(4 - serchItemSelected.ItemList().Count);
            }
        }

        private void btnClosed_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SaveDataSearch();
            btnClosed_Click(null, null);
        }

        /// <summary>
        /// Get data search
        /// </summary>
        private void SaveDataSearch()
        {
            serchItemSelected = new ItemCollection();

            foreach (DataGridViewRow row in dtgSelectId.Rows)
            {
                if (row.Cells[clId.Index].Value == null || string.IsNullOrWhiteSpace(row.Cells[clId.Index].Value.ToString()))
                    continue;

                serchItemSelected.AddItem(new ItemObject
                {
                    Index = row.Index,
                    ItemValue = row.Cells[clId.Index].Value.ToString()
                });
            }

            // Valid data
            if (serchItemSelected.ItemList().Count == 0)
            {
                MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE036)), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            ExportByCheckList(serchItemSelected);
        }


        /// <summary>
        /// Export data by check list 
        /// </summary>
        private void ExportByCheckList(ItemCollection itemSelectedList)
        {
            try
            {
                threeDSTSVController.InsertWiiTmp(itemSelectedList);
            }
            catch (Exception ex)
            {
                ErrorEntity error = new ErrorEntity()
                {
                    LogTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    ErrorMessage = ex.Message,
                    ModuleName = this.GetClassName() + " " + MethodBase.GetCurrentMethod().Name,
                    FilePath = this.logExceptionPath
                };

                LogException(error);

                MessageBox.Show(string.Format(GetResources.GetResourceMesssage(WiiConstant.MSGE038), error.LogTime, error.ModuleName, error.ErrorMessage, error.FilePath), GetResources.GetResourceMesssage(WiiConstant.ERROR_TITLE_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgSelectId_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(dtgSelectId_KeyPress);
            if (dtgSelectId.CurrentCell.ColumnIndex == 0)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dtgSelectId_KeyPress);
                }
            }
        }

        private void dtgSelectId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void CopyFromClipbroad()
        {
            string s = Clipboard.GetText();
            string[] lines = s.Split('\n');

            if (lines.Length == 0)
                return;
            // Current row selected 
            int rowIndex = dtgSelectId.CurrentRow.Index;
            int totalRows = dtgSelectId.Rows.Count;
            for (int i = 0; i < lines.Length; i++)
            {
                int input = -1;
                // Check data is numeric
                if (Int32.TryParse(lines[i], out input))
                {
                    rowIndex++;

                    if (rowIndex >= totalRows)
                    {
                        dtgSelectId.Rows.Add();
                    }
                    dtgSelectId.Rows[rowIndex - 1].Cells[0].Value = lines[i];
                }
            }
        }

        private void dtgSelectId_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                CopyFromClipbroad();
            }
        }

        private void dtgSelectId_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            if (dtgSelectId.Rows.Count < 5)
                dtgSelectId.Rows.Add(5 - dtgSelectId.Rows.Count);
        }
    }
}

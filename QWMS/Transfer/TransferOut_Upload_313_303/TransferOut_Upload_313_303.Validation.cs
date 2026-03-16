using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QCI.QWMS;
using QWMS.Common;
using System.Diagnostics;

namespace QWMS
{
    public partial class TransferOut_Upload_313_303
    {

        #region function ValidateWarehouseTransfer

        private bool ValidateDictionary(Dictionary<string, string> whBuilding, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (whBuilding == null)
                throw new ArgumentNullException(nameof(whBuilding), "whBuilding has a null value.");

            if (whBuilding.Count == 0)
            {
                errorMessage = "whBuilding is empty!!";
                return false;
            }

            return true;
        }

        private bool ValidateWarehouseInput(string fromWarehouse, string toWarehouse, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(fromWarehouse))
            {
                errorMessage = "Source warehouse cannot be empty.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(toWarehouse))
            {
                errorMessage = "Destination warehouse cannot be empty.";
                return false;
            }

            return true;
        }

        private bool ValidateWarehouseExist(string fromWarehouse, string toWarehouse, Dictionary<string, string> whBuilding, out string fromBuilding, out string toBuilding, out string errorMessage)
        {
            errorMessage = string.Empty;
            fromBuilding = null;
            toBuilding = null;

            List<string> invalidWarehouses = new List<string>();

            if (!whBuilding.TryGetValue(fromWarehouse, out fromBuilding))
                invalidWarehouses.Add(fromWarehouse);

            if (!whBuilding.TryGetValue(toWarehouse, out toBuilding))
                invalidWarehouses.Add(toWarehouse);

            if (invalidWarehouses.Count > 0)
            {
                errorMessage = $"Warehouses [{string.Join(", ", invalidWarehouses)}] do not exist in the system.";
                return false;
            }

            return true;
        }

        private bool ValidateBuildingRule(string fromWarehouse, string toWarehouse, string fromBuilding, string toBuilding, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(fromBuilding))
            {
                errorMessage = $"Building of warehouse {fromWarehouse} is invalid. Please double-check the input data!!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(toBuilding))
            {
                errorMessage = $"Building of warehouse {toWarehouse} is invalid. Please double-check the input data!!";
                return false;
            }

            if (string.Equals(fromBuilding, toBuilding, StringComparison.OrdinalIgnoreCase))
            {
                errorMessage = $"Warehouses {fromWarehouse} and {toWarehouse} are in the same building ({fromBuilding}).";
                return false;
            }

            return true;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BakeryDemo
{
    public partial class Form1 : Form
    {
        private readonly ProductRepository _products = new ProductRepository();
        private readonly OrderRepository _orders = new OrderRepository();
        private readonly List<CartItem> _cart = new List<CartItem>();

        public Form1()
        {
            InitializeComponent();
            InitializeCartGrid();
            nudQty.Minimum = 1;
            nudQty.Value = 1;
            LoadProducts();
        }

        private void InitializeCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProduktID", DataPropertyName = "ProduktID", Visible = false });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Product", DataPropertyName = "Name", ReadOnly = true });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "Preis", HeaderText = "Price", DataPropertyName = "Preis", ReadOnly = true });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "Menge", HeaderText = "Qty", DataPropertyName = "Menge" });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn { Name = "Sum", HeaderText = "Sum", DataPropertyName = "Sum", ReadOnly = true });

            dgvCart.CellEndEdit += delegate (object s, DataGridViewCellEventArgs e)
            {
                if (e.ColumnIndex == dgvCart.Columns["Menge"].Index)
                {
                    // Keep Menge >= 1
                    int qty;
                    if (int.TryParse(Convert.ToString(dgvCart.Rows[e.RowIndex].Cells["Menge"].Value), out qty))
                    {
                        if (qty < 1) qty = 1;
                        _cart[e.RowIndex].Menge = qty;
                        RefreshCartBinding();
                    }
                }
                UpdateTotal();
            };

            dgvCart.RowsRemoved += delegate { UpdateTotal(); };
        }

        private void LoadProducts(string search = null)
        {
            dgvProducts.DataSource = _products.GetAll(search);
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void RefreshCartBinding()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart.Select(c => new
            {
                c.ProduktID,
                c.Name,
                Preis = c.Preis,
                Menge = c.Menge,
                Sum = c.Sum
            }).ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null) return;
            var row = dgvProducts.CurrentRow;
            var id = Convert.ToInt32(row.Cells["ProduktID"].Value);
            var name = row.Cells["Name"].Value != null ? row.Cells["Name"].Value.ToString() : string.Empty;
            var price = Convert.ToDecimal(row.Cells["Preis"].Value);
            var qty = (int)nudQty.Value;

            var existing = _cart.FirstOrDefault(c => c.ProduktID == id);
            if (existing != null)
            {
                existing.Menge += qty;
            }
            else
            {
                _cart.Add(new CartItem { ProduktID = id, Name = name, Preis = price, Menge = qty });
            }

            RefreshCartBinding();
            UpdateTotal();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _cart.Clear();
            RefreshCartBinding();
            UpdateTotal();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cart.Count == 0)
                {
                    MessageBox.Show("Cart is empty.");
                    return;
                }

                var total = _cart.Sum(c => c.Sum);
                var orderId = _orders.SaveOrder(_cart, total);

                MessageBox.Show(string.Format("Order #{0} saved. Total: {1:C2}", orderId, total));
                _cart.Clear();
                RefreshCartBinding();
                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving order: " + ex.Message);
            }
        }

        private void UpdateTotal()
        {
            var total = _cart.Sum(c => c.Sum);
            lblTotal.Text = string.Format("Total: {0:C2}", total);
        }
    }
}

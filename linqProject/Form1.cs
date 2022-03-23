using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace linqProject
{
    public partial class Form1 : Form
    {
        companyBD Ent;
        public Form1()
        {
            InitializeComponent();
            Ent = new companyBD();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transfare_GB.Visible = false;
            CP_GB.Visible = false;
            Sup_Per_GB.Visible = false;
            store_GB.Visible = true;
            Item_GB.Visible = false;
            supplier_GB.Visible = false;
            client_GB.Visible = false;
            add_items_GB.Visible = false;
            addClientItems_GB.Visible = false;
            reports_GB.Visible = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            display();
            Transfare_GB.Visible = false;
            CP_GB.Visible = false;
            Sup_Per_GB.Visible = false;
            store_GB.Visible = false;
            Item_GB.Visible = false;
            supplier_GB.Visible = false;
            client_GB.Visible = false;
            add_items_GB.Visible = false;
            addClientItems_GB.Visible = false;

           
        }

        private void storeIdTb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(storeIdTb.Text);
            listBox7.Items.Clear();
            Store st = Ent.Stores.Find(id);
                if(st != null)
                {
                    storeNameTB.Text = st.name;
                    storeManagerTB.Text = st.manager;
                    storeAddressTB.Text = st.address;
                    foreach(Item i in st.Items)
                    {
                    listBox7.Items.Add(i.Item_code + "  " + i.name);
                    }
                }
                else
                {
                    MessageBox.Show("Store Not Found");
                }
                


        }

        private void addStore_Click(object sender, EventArgs e)
        {
            
            
            int st_id = (from store in Ent.Stores
                    where store.name == storeNameTB.Text
                    select store.store_id).Count();
            Store st = new Store();
                if (st_id == 0)
                {
              
                    st.name = storeNameTB.Text;
                    st.address = storeAddressTB.Text;
                    st.manager = storeManagerTB.Text;
                    Ent.Stores.Add(st);
                    Ent.SaveChanges();
                storeIdTb.Items.Add(st.store_id);
                    MessageBox.Show("Store added");
                    storeAddressTB.Text = storeManagerTB.Text = storeNameTB.Text = string.Empty;
                }
                else 
                {
                    MessageBox.Show("Store is exeist");
                }
            


        }

        private void updateStore_Click(object sender, EventArgs e)
        {
            int id = int.Parse(storeIdTb.Text);
            Store st = Ent.Stores.Find(id);
            if(storeAddressTB.Text!="" & storeManagerTB.Text!="" & storeNameTB.Text != "")
            {
                st.address = storeAddressTB.Text;
                st.name = storeNameTB.Text;
                st.manager = storeManagerTB.Text;
                Ent.SaveChanges();
                storeAddressTB.Text = storeManagerTB.Text = storeNameTB.Text = string.Empty;
                MessageBox.Show(" store Updated successfully");
            }
            else
            {
                MessageBox.Show("You must fill all data");
            }
        }


        private void Itembutton_Click(object sender, EventArgs e)
        {

            Transfare_GB.Visible = false;
            CP_GB.Visible = false;
            Sup_Per_GB.Visible = false;
            store_GB.Visible = false;
            Item_GB.Visible = true;
            supplier_GB.Visible = false;
            client_GB.Visible = false;
            add_items_GB.Visible = false;
            addClientItems_GB.Visible = false;
            reports_GB.Visible = false;

        }

        private void item_code_tbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(item_code_tbox.Text);
            Item i = Ent.Items.Find(id);
            addCodeItem.Text = i.Item_code.ToString();
            itemName.Text = i.name;
            Item_unit unitOne = (from unit in Ent.Item_unit
                           where unit.item_code == id
                           && (unit.unit_name =="mm"
                           || unit.unit_name == "mg")
                           select unit).First();
            UvalTB.Text = unitOne.unit_value.ToString();
            if(unitOne.unit_name == "mg")
            {
                mili_Gram.Checked = true;
            }
            else
            {
                mili_meter.Checked = true;
            }
        }

        private void AddItem_Click(object sender, EventArgs e)
        {
            int existsItem = (from item in Ent.Items
                         where item.name == itemName.Text
                         select item.Item_code).Count();

            if (existsItem == 0)
            {

                Item i = new Item();
                i.Item_code = int.Parse(addCodeItem.Text);
                i.name = itemName.Text;
                Ent.Items.Add(i);
                Ent.SaveChanges();
                Item_unit un = new Item_unit();
                un.item_code = i.Item_code;
                if (mili_Gram.Checked == true)
                {
                    un.unit_name = "mg";
                }
                else if(mili_meter.Checked == true)
                {
                    un.unit_name = "mm";
                }
                
                un.unit_value = int.Parse(UvalTB.Text);
                if(grames_GB.Visible == true)
                {
                    foreach (CheckBox ch in grames_GB.Controls)
                    {
                        if (ch.Checked == true)
                        {
                            Item_unit mutiUnit = new Item_unit();
                            mutiUnit.item_code = i.Item_code;
                            mutiUnit.unit_name = ch.Text;
                            if (ch.Text == "g")
                            {
                                mutiUnit.unit_value = un.unit_value / 1000;
                            }
                            else
                            {
                                mutiUnit.unit_value = un.unit_value / 1000000;
                            }
                            Ent.Item_unit.Add(mutiUnit);
                        }
                    }
                }
                else
                {
                    foreach (CheckBox ch in meters_Gb.Controls)
                    {
                        if (ch.Checked == true)
                        {
                            Item_unit mutiUnit = new Item_unit();
                            mutiUnit.item_code = i.Item_code;
                            mutiUnit.unit_name = ch.Text;
                            if (ch.Text == "m")
                            {
                                mutiUnit.unit_value = un.unit_value / 1000;
                               
                            }
                            else
                            {
                                mutiUnit.unit_value = un.unit_value / 1000000;
                            }
                            Ent.Item_unit.Add(mutiUnit);
                        }
                    }
                }
                
                Ent.Item_unit.Add(un);
                Ent.SaveChanges();
                MessageBox.Show("Item Added");
            }
            else
            {
                MessageBox.Show("Item is exeists");
            }
        }

        private void update_Item_Click(object sender, EventArgs e)
        {
            int id = int.Parse(item_code_tbox.Text);
            Item i = Ent.Items.Find(id);
            if(i != null)
            {
                i.Item_code =int.Parse( addCodeItem.Text);
                i.name = itemName.Text;

                Ent.SaveChanges();

                Item_unit un = (from unit in Ent.Item_unit
                               where unit.item_code == id
                               &&(unit.unit_name == "mm"
                               || unit.unit_name == "mg")
                                select unit).First();
                if(un != null)
                {
                    un.unit_value = int.Parse(UvalTB.Text);
                    if (grames_GB.Visible == true)
                    {
                        foreach (CheckBox ch in grames_GB.Controls)
                        {
                            if (ch.Checked == true)
                            {
                                Item_unit mutiUnit = Ent.Item_unit.Find(id, "g");

                                if (mutiUnit != null)
                                {
                                    mutiUnit.unit_value = un.unit_value / 1000;

                                }
                                else
                                {
                                    mutiUnit = Ent.Item_unit.Find(id, "kg");
                                    if (mutiUnit != null)
                                    {
                                        mutiUnit.unit_value = un.unit_value / 1000000;
                                       
                                    }

                                }

                            }
                        }
                    }
                    else
                    {
                        foreach (CheckBox ch in meters_Gb.Controls)
                        {
                            if (ch.Checked == true)
                            {
                                Item_unit mutiUnit = Ent.Item_unit.Find(id, "m");

                                if (mutiUnit != null)
                                {
                                    mutiUnit.unit_value = un.unit_value / 1000;
                                   
                                }
                                else
                                {
                                    mutiUnit = Ent.Item_unit.Find(id, "km");
                                    if (mutiUnit != null)
                                    {
                                        mutiUnit.unit_value = un.unit_value / 1000000;
                                       
                                    }

                                }

                            }
                        }
                    }
                }
                

               
                Ent.SaveChanges();
                MessageBox.Show("Item Updated");
            }
            else
            {
                MessageBox.Show("item not exeists");
            }
        }

        private void supplier_idTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(supplier_idTB.Text);
            Supplier sup = Ent.Suppliers.Find(id);
            if(sup != null)
            {
                supplier_name.Text = sup.name;
                supplier_phone.Text = sup.phone;
                FaxTB.Text = sup.fax;
                E_mailTB.Text = sup.email;
                mailTB.Text = sup.mail;
            }
            else
            {
                MessageBox.Show("Supplier is not exeists");
            }
           
        }

       

        private void mili_meter_CheckedChanged(object sender, EventArgs e)
        {
            if(mili_meter.Checked == true)
            {
                meters_Gb.Visible = true;
                grames_GB.Visible = false;
            }
        }

        private void mili_Gram_CheckedChanged(object sender, EventArgs e)
        {
            if (mili_Gram.Checked == true)
            {
                grames_GB.Visible = true;
                meters_Gb.Visible = false;
            }
        }

        private void supplier_Click(object sender, EventArgs e)
        {
            Transfare_GB.Visible = false;
            CP_GB.Visible = false;
            Sup_Per_GB.Visible = false;
            store_GB.Visible = false;
            Item_GB.Visible = false;
            supplier_GB.Visible = true;
            client_GB.Visible = false;
            add_items_GB.Visible = false;
            addClientItems_GB.Visible = false;
            reports_GB.Visible = false;

        }
        public void display()
        {
            item_code_tbox.Items.Clear();
            storeIdTb.Items.Clear();
            supplier_idTB.Items.Clear();
            Cid_TB.Items.Clear();
            SP_ids.Items.Clear();
            storeNameCB.Items.Clear();
            supplier_names.Items.Clear();
            CP_ids.Items.Clear();
            Store_N_GB.Items.Clear();
            Client_Name_GB.Items.Clear();
            To_stores.Items.Clear();
            from_stores.Items.Clear();
            Truns_Sup_Names.Items.Clear();
           var stores = from store in Ent.Stores select store;
            var items = from i in Ent.Items select i;
            var suppliers = from sup in Ent.Suppliers select sup;
            var clients = from client in Ent.Customers select client;
            var Sup_per_nums = from s in Ent.supplyPermitions select s;
            var Client_Per_nums = from c in Ent.exchangePermitions select c;
            foreach (Item it in items)
            {
                item_code_tbox.Items.Add(it.Item_code);
            }
            foreach (Store st in stores)
            {
                storeIdTb.Items.Add(st.store_id);
            }
            foreach (Supplier sup in suppliers)
            {
                supplier_idTB.Items.Add(sup.Supplier_id);
            }
            foreach (Customer c in clients)
            {
                Cid_TB.Items.Add(c.id);
            }
            foreach(Store s in stores)
            {
                storeNameCB.Items.Add(s.store_id + "  " + s.name);
            }
            foreach (supplyPermition sp in Sup_per_nums)
            {
                SP_ids.Items.Add(sp.id);
            }
          
            
            foreach (Supplier s in suppliers)
            {
                supplier_names.Items.Add(s.name);
            }

            foreach (exchangePermition c in Client_Per_nums)
            {
                CP_ids.Items.Add(c.id);
            }

            foreach (Store s in stores)
            {
                Store_N_GB.Items.Add(s.name);
            }
            foreach (Customer c in clients)
            {
                Client_Name_GB.Items.Add(c.name);
            }
            foreach(Store s in stores)
            {
                from_stores.Items.Add(s.store_id + " " + s.name);
            }

            foreach (Store s in stores)
            {
                To_stores.Items.Add(s.store_id +" "+s.name);
            }
            foreach(Supplier s in suppliers)
            {
                Truns_Sup_Names.Items.Add(s.Supplier_id + "  " + s.name);
            }

        }
        private void AddSuplier_Click(object sender, EventArgs e)
        {
            Supplier sup = new Supplier();
            sup.name = supplier_name.Text;
            sup.phone = supplier_phone.Text;
            sup.mail = mailTB.Text;
            sup.email = E_mailTB.Text;
            sup.fax = FaxTB.Text;
            Ent.Suppliers.Add(sup);
            Ent.SaveChanges();
            MessageBox.Show("Supplier Added");
            display();
            supplier_name.Text = supplier_phone.Text = mailTB.Text = E_mailTB.Text = FaxTB.Text = string.Empty;
        }

        private void Update_spplier_Click(object sender, EventArgs e)
        {
            int id = int.Parse(supplier_idTB.Text);
            Supplier sup = Ent.Suppliers.Find(id);
            if(sup != null)
            {
                sup.name = supplier_name.Text;
                sup.phone = supplier_phone.Text;
                sup.mail = mailTB.Text;
                sup.email = E_mailTB.Text;
                sup.fax = FaxTB.Text;
                Ent.SaveChanges();
                MessageBox.Show("Supplier updated ");
                supplier_name.Text = supplier_phone.Text = mailTB.Text = E_mailTB.Text = FaxTB.Text = string.Empty;

            }
            else
            {
                MessageBox.Show("supplier not found");
            }
        }

        private void Client_Click(object sender, EventArgs e)
        {
            Transfare_GB.Visible = false;
            CP_GB.Visible = false;
            Sup_Per_GB.Visible = false;
            store_GB.Visible = false;
            Item_GB.Visible = false;
            supplier_GB.Visible = false;
            client_GB.Visible = true;
            add_items_GB.Visible = false;
            addClientItems_GB.Visible = false;
            reports_GB.Visible = false;

        }

        private void Cid_TB_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(Cid_TB.Text);
            Customer c = Ent.Customers.Find(id);
            if (c != null)
            {
                Cname_TB.Text = c.name;
                Cmail_TB.Text = c.mail;
                Cphone_TB.Text = c.phone;
                CEmail_TB.Text = c.email;
                Cfax_TB.Text = c.fax;
            }
            else
            {
                MessageBox.Show("Client is not exeists");
            }
        }

        private void Add_Client_Click(object sender, EventArgs e)
        {
            
            Customer c = new Customer();
            c.name = Cname_TB.Text;
            c.mail = Cmail_TB.Text;
            c.phone = Cphone_TB.Text;
            c.email = CEmail_TB.Text;
            c.fax = Cfax_TB.Text;
            Ent.Customers.Add(c);
            MessageBox.Show("Client Added");
            Ent.SaveChanges();
            display();

        }

        private void Update_Client_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Cid_TB.Text);
            Customer cust = (from c in Ent.Customers
                            where c.id == id 
                            select c).First();
            if (cust != null)
            {
                cust.name = Cname_TB.Text;
                cust.mail = Cmail_TB.Text;
                cust.phone = Cphone_TB.Text;
                cust.email = CEmail_TB.Text;
                cust.fax = Cfax_TB.Text;
                Ent.SaveChanges();
                MessageBox.Show("Client Updated");
               
            }
            else
            {
                MessageBox.Show("Client is not exists");
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            Transfare_GB.Visible = false;
            CP_GB.Visible = false;
            Sup_Per_GB.Visible = true;
            store_GB.Visible = false;
            Item_GB.Visible = false;
            supplier_GB.Visible = false;
            client_GB.Visible = false;
            add_items_GB.Visible = false;
            addClientItems_GB.Visible = false;
            reports_GB.Visible = false;


        }

        private void storeNameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string store_id_name = storeNameCB.Text;
            
            string[] parsing = store_id_name.Split(' ');
            int storeId = int.Parse(parsing[0]);
            
           SP_storeTB.Text = parsing[2];
            Store s = (from i in Ent.Stores where i.store_id == storeId select i).First(); 
            var items = from i in Ent.Items
                        where  i.Stores.Any(st=>st.store_id == storeId) 
                        select i;
            listBox1.Items.Clear();
            foreach(Item i in items)
            {
                listBox1.Items.Add(i.name);
            }
        }

        private void SP_ids_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(SP_ids.Text);
            supplyPermition sp = Ent.supplyPermitions.Find(id);
            SP_storeTB.Text = sp.Store.name;
            SP_date.Text = sp.date.ToString();
            supplier_nTB.Text = sp.Supplier.name;
            storeNameCB.Text = "";
            supplier_GB.Text = "";
            per_number.Text = SP_ids.Text;
            listBox2.Items.Clear();
            foreach (var i in sp.item_supplyPermition)
            {
                listBox2.Items.Add(i.Item.name);
            }
           
        }


        private void supplier_names_SelectedIndexChanged(object sender, EventArgs e)
        {
            supplier_nTB.Text = supplier_names.Text;
        }

       
        private void button6_Click(object sender, EventArgs e)
        {
            if(storeNameCB.Text != "")
            {
                supplyPermition s = new supplyPermition();
                s.date = DateTime.Parse(SP_date.Text);
                string store_id_name = storeNameCB.Text;
                string[] parsing = store_id_name.Split(' ');
                int storeId = int.Parse(parsing[0]);
                s.store_id = storeId;

                add_items_GB.Visible = true;
                string sup_name = supplier_nTB.Text;
                int sup_id = (from i in Ent.Suppliers
                              where i.name == sup_name
                              select i.Supplier_id).First();
                s.supplier_id = sup_id;

                var st = Ent.Stores.Where(a => a.name == SP_storeTB.Text).First();
                
                Ent.supplyPermitions.Add(s);
                Ent.SaveChanges();
                
                    listBox1.Items.Clear();
                    foreach (Item i in Ent.Items)
                    {
                        listBox1.Items.Add(i.name);
                    }
                
                display();
            }
            else
            {
                int supply_per_id = int.Parse(SP_ids.Text);
                var s = from i in Ent.item_supplyPermition
                        where i.SP_id == supply_per_id
                        select i.Item;
              
                var st = Ent.Stores.Where(a => a.name == SP_storeTB.Text).First();
                var items = Ent.Items;

                if (s != null)
                {
                    listBox2.Items.Clear();
                    foreach (Item i in s)
                    {
                        listBox2.Items.Add(i.name);
                    }


                    listBox1.Items.Clear();
                    foreach (Item i in items)
                    {
                        listBox1.Items.Add(i.name);
                    }

                }
                

                add_items_GB.Visible = true;
                Quantity_TB.Text = prod_Date_TB.Text = expiry_Date_TB.Text = string.Empty;
            }
            

            
            per_number.Text = (from t in Ent.supplyPermitions orderby t.id descending select t.id).First().ToString();
            add_items_GB.Visible = true;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Ent.SaveChanges();
            MessageBox.Show("Supply Permition Added");

        }

        private void addNewItem_Click_1(object sender, EventArgs e)
        {
            int item_id = (from it in Ent.Items
                           where it.name == itemName_TB.Text
                           select it.Item_code).First();
            item_supplyPermition i = new item_supplyPermition();
           
            i.SP_id = int.Parse(per_number.Text);
            i.item_id = item_id;
            i.quantity = int.Parse(Quantity_TB.Text);
            i.productionDate = DateTime.Parse(prod_Date_TB.Text);
            i.expiryDate = DateTime.Parse(expiry_Date_TB.Text);
            listBox2.Items.Add(itemName_TB.Text);
            Ent.item_supplyPermition.Add(i);
            Item item1 = Ent.Items.Find(item_id);
            string store_name = SP_storeTB.Text;
            
            Store s1 = Ent.Stores.Where(a=>a.name == store_name).First();
            item1.Stores.Add(s1);
            Ent.SaveChanges();
        }


        private void listBox1_DoubleClick_1(object sender, EventArgs e)
        {
            itemName_TB.Text= listBox1.SelectedItem.ToString();
            //MessageBox.Show(listBox1.SelectedItem.ToString());
            Quantity_TB.Text = prod_Date_TB.Text = expiry_Date_TB.Text = string.Empty;

        }

        private void listBox2_DoubleClick_1(object sender, EventArgs e)
        {
            int id = int.Parse(SP_ids.Text);

            string item_name = listBox2.SelectedItem.ToString();
            itemName_TB.Text = item_name;
            Quantity_TB.Text = (from i in Ent.item_supplyPermition
                                where i.Item.name == item_name
                                && (i.SP_id == id)
                                select i.quantity).First().ToString();
            prod_Date_TB.Text = (from i in Ent.item_supplyPermition
                                 where i.Item.name == item_name
                                 && (i.SP_id == id)
                                 select i.productionDate).First().ToString();
            expiry_Date_TB.Text = (from i in Ent.item_supplyPermition
                                   where i.Item.name == item_name
                                   && (i.SP_id == id)
                                   select i.expiryDate).First().ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int Iid = (from i in Ent.Items where i.name == itemName_TB.Text select i.Item_code).First();
            int Sid = int.Parse(per_number.Text);
            item_supplyPermition it = Ent.item_supplyPermition.Where(a => a.item_id == Iid && a.SP_id == Sid).First();
            if(it != null)
            {
                it.quantity = int.Parse(Quantity_TB.Text);
                it.productionDate = DateTime.Parse(prod_Date_TB.Text);
                it.expiryDate = DateTime.Parse(expiry_Date_TB.Text);

                Ent.SaveChanges();
                MessageBox.Show("Item updated");
            }
            else
            {
                MessageBox.Show("Item not exists");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            int supply_per_id = int.Parse(SP_ids.Text);
            var s = from i in Ent.item_supplyPermition
                    where i.SP_id == supply_per_id
                    select i.Item;

            var st = Ent.Stores.Where(a => a.name == SP_storeTB.Text).First();
           // var items = st.Items;

            if (s != null)
            {
                listBox2.Items.Clear();
                foreach (Item i in s)
                {
                    listBox2.Items.Add(i.name);
                }

                listBox1.Items.Clear();
                foreach (Item i in Ent.Items)
                {
                    listBox1.Items.Add(i.name);
                }
            }
            add_items_GB.Visible = true;
            Quantity_TB.Text = prod_Date_TB.Text = expiry_Date_TB.Text = string.Empty;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int Iid = (from i in Ent.Items where i.name == itemName_TB.Text select i.Item_code).First();
            int Sid = int.Parse(per_number.Text);
            item_supplyPermition it = Ent.item_supplyPermition.Where(a => a.item_id == Iid && a.SP_id == Sid).First();
            Ent.item_supplyPermition.Remove(it);
            Ent.SaveChanges();
            MessageBox.Show("item Deleted");
            var s = from i in Ent.item_supplyPermition
                    where i.SP_id == Sid
                    select i.Item;
            listBox2.Items.Clear();
            foreach (Item i in s)
            {
                listBox2.Items.Add(i.name);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(SP_ids.Text);
            supplyPermition sp = Ent.supplyPermitions.Find(id);
            if(sp != null)
            {
                int Sid = (from i in Ent.Stores where i.name == SP_storeTB.Text select i.store_id).First();
                sp.store_id = Sid;
                int Supid = (from i in Ent.Suppliers where i.name == supplier_nTB.Text select i.Supplier_id).First();
                sp.supplier_id = Supid;
                sp.date = DateTime.Parse(SP_date.Text);
                MessageBox.Show("Permition Updated");
            }
            else
            {
                MessageBox.Show("Permition deleted");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Transfare_GB.Visible = false;
            CP_GB.Visible = true;
            Sup_Per_GB.Visible = false;
            store_GB.Visible = false;
            Item_GB.Visible = false;
            supplier_GB.Visible = false;
            client_GB.Visible = false;
            add_items_GB.Visible = false;
            addClientItems_GB.Visible = false;
            reports_GB.Visible = false;


        }

        private void CP_ids_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(CP_ids.Text);
            exchangePermition s = Ent.exchangePermitions.Find(id);
            if(s != null)
            {
                store_name_TB.Text = s.Store.name;
                Client_Name_TB.Text = s.Customer.name;
                Cdate.Text = s.date.ToString();
            }
        }

        private void Store_N_GB_SelectedIndexChanged(object sender, EventArgs e)
        {
            store_name_TB.Text = Store_N_GB.Text;
        }

        private void Client_Name_TB_TextChanged(object sender, EventArgs e)
        {

        }

        private void Client_Name_GB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Client_Name_TB.Text = Client_Name_GB.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            exchangePermition st = new exchangePermition();
            st.store_id = (from i in Ent.Stores
                           where i.name == store_name_TB.Text
                           select i.store_id).First();
            st.date = DateTime.Parse(Cdate.Text);
            st.client_id = (from i in Ent.Customers
                            where i.name == Client_Name_TB.Text
                            select i.id).First();
            Ent.exchangePermitions.Add(st);
            Ent.SaveChanges();
            display();
            addClientItems_GB.Visible = true;
        }

        private void listBox4_DoubleClick(object sender, EventArgs e)
        {
            string s = listBox4.SelectedItem.ToString();
            CIN_TB.Text = s;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int item_id = (from it in Ent.Items
                           where it.name == CIN_TB.Text
                           select it.Item_code).First();
            item_exchamgePermition i = new item_exchamgePermition();

            i.EXP_id = int.Parse(EP_num_TB.Text);
            i.item_id = item_id;
            i.quantity = int.Parse(C_quantity_TB.Text);

            listBox3.Items.Add(CIN_TB.Text);
            Ent.item_exchamgePermition.Add(i);

            Ent.SaveChanges();
            MessageBox.Show("Item Added");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            addClientItems_GB.Visible = true;
            EP_num_TB.Text = CP_ids.Text;
            int id = int.Parse(CP_ids.Text);

            var items = from i in Ent.item_exchamgePermition
                        where i.EXP_id == id
                        select i.Item;
            listBox3.Items.Clear();
            foreach (Item i in items)
            {
                listBox3.Items.Add(i.name);
            }
            listBox4.Items.Clear();
            var items2 = Ent.Items.Where(a => a.Stores.Any(b => b.name == store_name_TB.Text));
            foreach (Item i in items2)
            {
                listBox4.Items.Add(i.name);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        { 
            int id = int.Parse(EP_num_TB.Text);
            item_exchamgePermition x = (from i in Ent.item_exchamgePermition
                                        where i.EXP_id == id
                                        && (i.Item.name == CIN_TB.Text)
                                        select i).First();
            if (x != null)
            {
                x.quantity = int.Parse(C_quantity_TB.Text);
                Ent.SaveChanges();
                MessageBox.Show("item Updated");
            }
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {
            string itemName = listBox3.SelectedItem.ToString();
            CIN_TB.Text = itemName;
            int id = int.Parse(EP_num_TB.Text);
            item_exchamgePermition x = (from i in Ent.item_exchamgePermition
                                        where i.EXP_id == id
                                        && (i.Item.name == CIN_TB.Text)
                                        select i).First();
            if (x != null)
            {
                C_quantity_TB.Text = x.quantity.ToString(); 
                Ent.SaveChanges();
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            int Iid = (from i in Ent.Items where i.name == CIN_TB.Text select i.Item_code).First();
            int Sid = int.Parse(EP_num_TB.Text);
            item_exchamgePermition it = Ent.item_exchamgePermition.Where(a => a.item_id == Iid && a.EXP_id == Sid).First();
            Ent.item_exchamgePermition.Remove(it);
            Ent.SaveChanges();
            MessageBox.Show("item Deleted");
            var s = from i in Ent.item_exchamgePermition
                    where i.EXP_id == Sid
                    select i.Item;
            listBox3.Items.Clear();
            foreach (Item i in s)
            {
                listBox3.Items.Add(i.name);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int id = int.Parse(CP_ids.Text);
            exchangePermition x = Ent.exchangePermitions.Find(id);
            if(x != null)
            {
                x.Store.name = store_name_TB.Text;
                x.Customer.name = Client_Name_TB.Text;
                x.date = DateTime.Parse(Cdate.Text);
                MessageBox.Show("Permition Updated");
            }
        }

        private void Transfare_Items_Click(object sender, EventArgs e)
        {
            Transfare_GB.Visible = true;
            CP_GB.Visible = false;
            Sup_Per_GB.Visible = false;
            store_GB.Visible = false;
            Item_GB.Visible = false;
            supplier_GB.Visible = false;
            client_GB.Visible = false;
            add_items_GB.Visible = false;
            addClientItems_GB.Visible = false;
            reports_GB.Visible = false;
        }

        private void from_stores_SelectedIndexChanged(object sender, EventArgs e)
        {
            string storedata = from_stores.SelectedItem.ToString();
            string[] store_id_name = storedata.Split(' ');
            int store_id = int.Parse(store_id_name[0]);
            string store_name = store_id_name[1];
            listBox5.Items.Clear();
            var items = Ent.Stores.Find(store_id).Items;
            foreach(Item i in items)
            {
                listBox5.Items.Add(i.Item_code +"  "+i.name);
            }
           
            Store selected_Store = Ent.Stores.Find(store_id);
            To_stores.Items.Clear();
            var stores = Ent.Stores.Where(a=>a.store_id != selected_Store.store_id).ToList();
            foreach(Store s in stores) {
                To_stores.Items.Add(s.store_id + "  " + s.name);
            }
        }

        private void To_stores_SelectedIndexChanged(object sender, EventArgs e)
        {
            string storedata = To_stores.SelectedItem.ToString();
            string[] store_id_name = storedata.Split(' ');
            int store_id = int.Parse(store_id_name[0]);
            string store_name = store_id_name[1];
            

            Store selected_Store = Ent.Stores.Find(store_id);
            from_stores.Items.Clear();
            var stores = Ent.Stores.Where(a => a.store_id != selected_Store.store_id).ToList();
            foreach (Store s in stores)
            {
                from_stores.Items.Add(s.store_id + "  " + s.name);
            }
        }

        private void listBox5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string storedata = from_stores.Text;
            string[] store_id_name = storedata.Split(' ');
            int store_id = int.Parse(store_id_name[0]);
            string store_name = store_id_name[1];

            string item_selected = listBox5.SelectedItem.ToString();
            string[] item_id_name = item_selected.Split(' ');
            int item_id = int.Parse(item_id_name[0]);
            T_product_id.Text = item_id.ToString();
            Item item = Ent.Items.Find(item_id);
            tt_prod_dates.Items.Clear();
            if (item.Stores.Where(a=>a.store_id == store_id).Count() != 0)
            {
                foreach(item_supplyPermition i in item.item_supplyPermition)
                {
                    tt_prod_dates.Items.Add(i.productionDate);
                }
            }
            
           


        }

        private void button16_Click(object sender, EventArgs e)
        {
            int item_id = int.Parse(T_product_id.Text);
            string to_store_data = To_stores.Text;
            string[] store_id_name = to_store_data.Split(' ');
            int store_id = int.Parse(store_id_name[0]);

            string from_store_data = from_stores.Text;
            string[] Fstore_id_name = from_store_data.Split(' ');
            int Fstore_id = int.Parse(Fstore_id_name[0]);

            string supplier_data = Truns_Sup_Names.Text;
            string[] suplier_name_id = supplier_data.Split(' ');
            int Tsupplier_id = int.Parse(suplier_name_id[0]);

            Store s = Ent.Stores.Find(store_id);
            var i = Ent.Items.Where(a => a.Stores.Select(b=>b.store_id == store_id).FirstOrDefault()).Where(c=>c.Item_code==item_id);
            if (i.Count() == 0)
            {
                // s.Items.Add(i);
                Item it = Ent.Items.Find(item_id);
                Ent.Stores.Find(store_id).Items.Add(it);
                Ent.SaveChanges();
            }
            transfare T = new transfare();
            T.item_code = item_id;
            T.to_store = store_id;
            T.from_store = Fstore_id;
            T.quantity = int.Parse(TQuantity_TB.Text);
            T.productionDate = DateTime.Parse(T_prod_date_TB.Text);
            T.expiryDate = DateTime.Parse(T_expi_date_TB.Text);
            T.supplier_id = Tsupplier_id;
            Ent.transfares.Add(T);
            Ent.SaveChanges();
            MessageBox.Show("Item Transfared");
            listBox6.Items.Add(listBox5.SelectedItem);
            TQuantity_TB.Text = T_prod_date_TB.Text = T_expi_date_TB.Text = T_product_id.Text = string.Empty;
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tt_prod_dates_SelectedIndexChanged(object sender, EventArgs e)
        {
            T_prod_date_TB.Text = tt_prod_dates.SelectedItem.ToString();
            DateTime p = DateTime.Parse(T_prod_date_TB.Text);
            int item_id = int.Parse(T_product_id.Text);
            string to_store_data = To_stores.Text;
            string[] store_id_name = to_store_data.Split(' ');
            int store_id = int.Parse(store_id_name[0]);
            Item item = Ent.Items.Find(item_id);
            Store s = Ent.Stores.Find(store_id);
            
            if (s != null)
            {
                item_supplyPermition i = item.item_supplyPermition.Where(a => a.productionDate == p).First();
                T_expi_date_TB.Text = i.expiryDate.ToString();
               
            }


        }

        private void button17_Click(object sender, EventArgs e)
        {
            reports_GB.Visible = true;
            Transfare_GB.Visible = false;
            CP_GB.Visible = false;
            Sup_Per_GB.Visible = false;
            store_GB.Visible = false;
            Item_GB.Visible = false;
            supplier_GB.Visible = false;
            client_GB.Visible = false;
            add_items_GB.Visible = false;
            addClientItems_GB.Visible = false;
            store_report_date.Items.Clear();
            
            Report_timeInStore.Items.Clear();
            foreach(supplyPermition s in Ent.supplyPermitions)
            {
                store_report_date.Items.Add(s.date);
                
            }
            report_store_name.Items.Clear();
            report_item_Sname.Items.Clear();
            foreach (Store s in Ent.Stores)
            {
                report_store_name.Items.Add(s.name);
                report_item_Sname.Items.Add(s.name);
                Report_timeInStore.Items.Add(s.name);
            }
            Report_item_name.Items.Clear();
            foreach(Item s in Ent.Items)
            {
                Report_item_name.Items.Add(s.Item_code+"  "+s.name);
            }
            Transaction_items.Items.Clear();
            foreach(Item i in Ent.Items)
            {
                Transaction_items.Items.Add(i.Item_code + "  " + i.name);
            }
            
            
            
        }


        private void store_report_date_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Parse(store_report_date.SelectedItem.ToString());
            listBox8.Items.Clear();
            var sp_per = Ent.supplyPermitions.Where(a => a.date == date && a.Store.name==report_store_name.Text);
            foreach(supplyPermition i in sp_per)
            {
                foreach(item_supplyPermition s in i.item_supplyPermition)
                {
                    listBox8.Items.Add(s.item_id+"   "+s.Item.name + "   " + s.quantity + "   " + s.productionDate + "   " + s.expiryDate);
                }
            }
        }

        private void report_store_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            Store s = Ent.Stores.Where(a => a.name == report_store_name.SelectedItem.ToString()).First();
            store_report_date.Items.Clear();
            foreach (supplyPermition p in Ent.supplyPermitions)
            {
                if(p.Store.name == report_store_name.SelectedItem.ToString())
                {
                    store_report_date.Items.Add(p.date);
                }
                
            }
            store_id.Text = "Store ID: "+s.store_id.ToString();
            store_manager.Text = "Manager: "+s.manager;
            store_address.Text = "Address: "+s.address;
            listBox8.Items.Clear();
            foreach(supplyPermition t in s.supplyPermitions)
            {
                
                foreach(item_supplyPermition x in t.item_supplyPermition)
                {
                    listBox8.Items.Add(x.item_id+"   "+x.Item.name + "   " + x.quantity + "   " + x.productionDate + "   " + x.expiryDate);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox9.Items.Clear();
           // DateTime d = DateTime.Parse(Repirt_item_date.SelectedItem.ToString());
            int code = int.Parse(Report_item_name.SelectedItem.ToString().Split(' ')[0]);
            var list = Ent.item_supplyPermition.Where(a => a.item_id == code);
            foreach(item_supplyPermition i in list)
            {
                listBox9.Items.Add($" {i.supplyPermition.store_id}{i.supplyPermition.Store.name} {i.productionDate.Date} {i.expiryDate.Date} {i.supplyPermition.Supplier.name}{i.Item.name} {i.quantity}");

            }
        }

        private void Report_item_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox9.Items.Clear();
            int  code = int.Parse(Report_item_name.SelectedItem.ToString().Split(' ')[0]);
            

            foreach (item_supplyPermition i in Ent.item_supplyPermition)
            {
                if(i.item_id == code)
                {
                    listBox9.Items.Add($" {i.supplyPermition.store_id} {i.supplyPermition.Store.name} {i.quantity}    {i.productionDate}  {i.expiryDate}   {i.supplyPermition.Supplier.name}");
                }
            }
           
        }

        private void report_item_Sname_SelectedIndexChanged(object sender, EventArgs e)
        {
            int code = int.Parse(Report_item_name.Text.ToString().Split(' ')[0]);
            var sp = Ent.item_supplyPermition.Where(a => a.item_id == code & a.supplyPermition.Store.name == report_item_Sname.SelectedItem.ToString());
            foreach(item_supplyPermition s in sp)
            {
                listBox9.Items.Add($" {s.supplyPermition.store_id} {s.supplyPermition.Store.name} {s.quantity}    {s.productionDate}  {s.expiryDate}   {s.supplyPermition.Supplier.name}");

            }
        }

        private void Report_timeInStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sp = Ent.supplyPermitions.Where(a => a.Store.name == Report_timeInStore.SelectedItem.ToString());
            listBox10.Items.Clear();
            foreach(supplyPermition s in sp)
            {
                var date = DateTime.Now.Day- s.date.Day;
                foreach(item_supplyPermition x in s.item_supplyPermition)
                {
                    listBox10.Items.Add(x.item_id + "    " + x.Item.name + "    " + x.quantity + "    " + date);
                }
            }
        }

        private void Transaction_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            Transaction_store.Items.Clear();
            int code = int.Parse(Transaction_items.SelectedItem.ToString().Split(' ')[0]);
            
            foreach(Store i in Ent.Stores)
            {
              var stores= i.transfares.Where(a=>a.Item.Item_code==code).Select(a=>a.Store).Distinct();
                foreach(Store x in stores)
                {
                    Transaction_store.Items.Add(x.name);
                }
            }
        }

        private void Transaction_store_SelectedIndexChanged(object sender, EventArgs e)
        {
            int code = int.Parse(Transaction_items.Text.ToString().Split(' ')[0]);
            int store_id = Ent.Stores.Where(a => a.name == Transaction_store.SelectedItem.ToString()).Select(a => a.store_id).First();
            var transactions = Ent.transfares.Where(b => b.from_store == store_id & b.item_code == code);
            foreach(transfare i in transactions)
            {
                listBox11.Items.Add(i.Item.name + i.Store.name + i.Store1.name + i.productionDate.ToShortDateString() + " " + i.expiryDate.ToShortDateString() + "   "+ i.quantity);
            }
        }

        private void showItems_B_Click(object sender, EventArgs e)
        {
            int month = int.Parse(month_TB.Text);
           
          
            var items = Ent.item_supplyPermition.Where(a => a.expiryDate.Year >= DateTime.Now.Year).OrderBy(a=>a.expiryDate);
            listBox12.Items.Clear();
            foreach (item_supplyPermition i in items)
            {
                

                int M = i.expiryDate.Month - month;
                if(M >=0)
                {
                    listBox12.Items.Add(i.Item.name + " " + i.supplyPermition.Store.name + " " + M);
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            Store s = Ent.Stores.Where(a => a.name == report_store_name.Text.ToString()).First();
            store_report_date.Items.Clear();
            
            listBox8.Items.Clear();
            foreach (supplyPermition t in s.supplyPermitions)
            {
                listBox8.Items.Clear();

                foreach (item_supplyPermition x in t.item_supplyPermition)
                {
                    if (dateTimePicker1.Value <t.date & dateTimePicker2.Value>t.date)
                    {
                        listBox8.Items.Add(x.item_id + "   " + x.Item.name + "   " + x.quantity + "   " + x.productionDate + "   " + x.expiryDate);

                    }
                }
            }
            
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            listBox9.Items.Clear();
            int code = int.Parse(Report_item_name.Text.ToString().Split(' ')[0]);


            foreach (item_supplyPermition i in Ent.item_supplyPermition)
            {
                if (i.item_id == code)
                {
                    if(dateTimePicker3.Value < i.supplyPermition.date && dateTimePicker4.Value > i.supplyPermition.date)
                    {
                        listBox9.Items.Add($" {i.supplyPermition.store_id} {i.supplyPermition.Store.name} {i.quantity}    {i.productionDate}  {i.expiryDate}   {i.supplyPermition.Supplier.name}");

                    }
                }
            }
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            Transaction_store.Items.Clear();
            int code = int.Parse(Transaction_items.Text.ToString().Split(' ')[0]);

            foreach (Store i in Ent.Stores)
            {
                var stores = i.transfares.Where(a => a.Item.Item_code == code).Select(a => a.Store).Distinct();
                foreach (Store x in stores)
                {
                    foreach(supplyPermition y in i.supplyPermitions)
                    {
                        if (dateTimePicker5.Value < y.date && dateTimePicker6.Value > y.date)
                        {

                            Transaction_store.Items.Add(x.name);
                        }

                    }
                    
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var sp = Ent.supplyPermitions.Where(a => a.Store.name == Report_timeInStore.Text.ToString());
            listBox10.Items.Clear();
            foreach (supplyPermition s in sp)
            {
                int day=0;int month=0; int year=0;
                if (textBox1.Text !="")
                {

                    day = DateTime.Now.Day - s.date.Day - int.Parse(textBox1.Text);
                }

                if (textBox2.Text != "")
                {

                    month =  DateTime.Now.Month - s.date.Month - int.Parse(textBox2.Text);
                   
                }

                if (textBox3.Text != "")
                {

                    year = DateTime.Now.Year -  s.date.Year - int.Parse(textBox3.Text);
                }

                if (month < 0)
                {
                    month = 12 - month;
                    year -= 1;
                }
                if(year < 0 )
                {
                    year = 0;
                }
                if(day < 0)
                {
                    day = 0;
                    month -=1;
                   

                }
                if(month > 12)
                {
                    year += 1;
                    month = month - 12;
                }
                foreach (item_supplyPermition x in s.item_supplyPermition)
                {
                    listBox10.Items.Add(x.item_id + "    " + x.Item.name + "    " + x.quantity + "    " + day+ " "+month+" " + year );
                }
            }
        }
    }
}
